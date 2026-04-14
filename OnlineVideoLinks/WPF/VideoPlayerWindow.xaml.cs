using OnlineVideoLinks.Models;
using OnlineVideoLinks.Utilities;
using SharpDX.XInput;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace OnlineVideoLinks.WPF
{
    /// <summary>
    /// Interaction logic for VideoPlayerWindow.xaml
    /// </summary>
    public partial class VideoPlayerWindow : Window, IVideoPlayer
    {
        const string TempVideoPath = "temp_video.mp4";
        const int MouseHideDelayMs = 3000;

        private GameVideo _gameVideo;
        private DispatcherTimer _progressTimer;
        private DispatcherTimer _mouseHideTimer;
        private bool _isPlaying;
        private bool _isClosing;
        private CancellationTokenSource _cancellation;
        private bool _isCursorHidden;
        private Point? _lastMousePosition;
        private int _startTime;
        private int _stopTime;

        public event EventHandler PlayerClosed;

        public bool IsVisible => Visibility == Visibility.Visible;

        public VideoPlayerWindow()
        {
            InitializeComponent();

            //_gameVideo = gameVideo;

            // Timer to update progress display
            _progressTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            _progressTimer.Tick += ProgressTimer_Tick;

            // Timer to auto-hide mouse cursor after inactivity
            _mouseHideTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(MouseHideDelayMs)
            };
            _mouseHideTimer.Tick += MouseHideTimer_Tick;

            // Track mouse movement to show/hide cursor
            this.MouseMove += VideoPlayerWindow_MouseMove;
        }

        /// <summary>
        /// Loads a game video and immediately starts playing it.
        /// </summary>
        /// <param name="video"></param>
        public async Task Play(GameVideo video)
        {
            _gameVideo = video;

            // Store start/stop times
            _startTime = video.StartTime;
            _stopTime = video.StopTime;

            this.Show();

            // Start the hide timer (cursor will be hidden in ContentRendered)
            _mouseHideTimer.Start();

            // Show loading indicator
            // TODO
            progressBar.Visibility = Visibility.Visible;

            // Create cancellation token for this load operation
            _cancellation = new CancellationTokenSource();

            try
            {
                if (VideoMetadataUtilities.IsYoutubeUrl(_gameVideo.VideoPath))
                    await LoadYoutubeVideo(_cancellation.Token);
                else
                    LoadRegularVideo();
            }
            catch (OperationCanceledException)
            {
                // Loading was cancelled, window is closing
                return;
            }

            // Check if window was closed during loading
            if (_isClosing)
                return;
        }

        /// <summary>
        /// Toggles the play/pause function.
        /// </summary>
        public void PlayPause()
        {
            if (_isPlaying)
            {
                mediaElement.Pause();
                _progressTimer.Stop();
                _isPlaying = false;
            }
            else
            {
                mediaElement.Play();
                _progressTimer.Start();
                _isPlaying = true;
            }
        }

        /// <summary>
        /// Skips 10 seconds backward.
        /// </summary>
        public void SkipBackward()
        {
            var newPosition = mediaElement.Position - TimeSpan.FromSeconds(10);
            mediaElement.Position = newPosition > TimeSpan.Zero ? newPosition : TimeSpan.Zero;
        }

        /// <summary>
        /// Skips 10 seconds forward.
        /// </summary>
        public void SkipForward()
        {
            if (mediaElement.NaturalDuration.HasTimeSpan)
            {
                var newPosition = mediaElement.Position + TimeSpan.FromSeconds(10);
                if (newPosition < mediaElement.NaturalDuration.TimeSpan)
                    mediaElement.Position = newPosition;
                else
                    mediaElement.Position = mediaElement.NaturalDuration.TimeSpan;
            }
        }

        public void StopPlaying()
        {
            if (_isClosing)
                return;

            _isClosing = true;

            // Cancel any ongoing download
            _cancellation?.Cancel();
            _cancellation?.Dispose();
            _cancellation = null;

            // Stop mouse hide timer and restore cursor
            _mouseHideTimer.Stop();
            ShowCursor();

            _progressTimer.Stop();
            mediaElement.Stop();
            mediaElement.Close();
            mediaElement.Source = null;
            _isPlaying = false;

            if (File.Exists(TempVideoPath))
                File.Delete(TempVideoPath);

            this.Close();
        }

        /// <summary>
        /// Stops playing and closes the window.
        /// </summary>
        //public void StopAndClose()
        //{
        //    Dispatcher.BeginInvoke(new Action(() =>
        //    {
        //        this.Close();
        //    }));
        //}

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            // Hide cursor once window is fully rendered
            // Initialize last position so first real mouse move will be detected
            _lastMousePosition = Mouse.GetPosition(this);
            HideCursor();
        }

        private void LoadRegularVideo()
        {
            Uri mediaUri;

            // Check if it's a network URL or a local file path
            if (Uri.TryCreate(_gameVideo.VideoPath, UriKind.Absolute, out var uri) &&
                (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            {
                mediaUri = uri;
            }
            else
            {
                // Local file path - resolve relative paths to absolute
                var filePath = Path.IsPathRooted(_gameVideo.VideoPath)
                    ? _gameVideo.VideoPath
                    : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _gameVideo.VideoPath);

                mediaUri = new Uri(filePath);
            }

            mediaElement.Source = mediaUri;
            mediaElement.Play();
            _isPlaying = true;
            _progressTimer.Start();
        }

        private async Task LoadYoutubeVideo(CancellationToken cancellationToken)
        {
            var videoPath = await YoutubeDownloader.GetPlayableVideoPath(
                _gameVideo.VideoPath, 
                TempVideoPath, 
                _gameVideo.StartTime, 
                _gameVideo.StopTime, 
                cancellationToken);

            mediaElement.Source = new Uri(videoPath);
            mediaElement.Play();
            _isPlaying = true;
            _progressTimer.Start();
        }

        /// <summary>
        /// Sends gamepad input.
        /// </summary>
        /// <param name="button"></param>
        public void SendGamepadInput(GamepadButtonFlags button)
        {
            switch (button)
            {
                case GamepadButtonFlags.A:
                    PlayPause(); break;
                case GamepadButtonFlags.B:
                    this.Close(); break;
                case GamepadButtonFlags.DPadLeft:
                    SkipBackward(); break;
                case GamepadButtonFlags.DPadRight:
                    SkipForward(); break;
            }
        }

        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            progressBar.Visibility = Visibility.Collapsed;

            // Seek to start time if specified
            if (_startTime > 0)
            {
                mediaElement.Position = TimeSpan.FromSeconds(_startTime);
            }
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            StopPlaying();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Ensure cleanup happens even if window is closed directly (e.g., via X button)
            if (!_isClosing)
            {
                _isClosing = true;

                // Cancel any ongoing download
                _cancellation?.Cancel();
                _cancellation?.Dispose();
                _cancellation = null;

                // Stop mouse hide timer and restore cursor
                _mouseHideTimer.Stop();
                ShowCursor();

                _progressTimer.Stop();
                mediaElement.Stop();
                mediaElement.Close();
                mediaElement.Source = null;
                _isPlaying = false;

                if (File.Exists(TempVideoPath))
                    File.Delete(TempVideoPath);
            }

            PlayerClosed?.Invoke(this, EventArgs.Empty);
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            PlayPause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            StopPlaying();
        }

        private void btnSkipBack_Click(object sender, RoutedEventArgs e)
        {
            SkipBackward();
        }

        private void btnSkipFwd_Click(object sender, RoutedEventArgs e)
        {
            SkipForward();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                case Key.Space:
                case Key.A:
                    PlayPause(); break;
                case Key.Escape:
                    StopPlaying(); break;
                case Key.B:
                    StopPlaying(); break;
                case Key.Left:
                    SkipBackward(); break;
                case Key.Right:
                    SkipForward(); break;
            }
        }

        private void ProgressTimer_Tick(object sender, EventArgs e)
        {
            var current = mediaElement.Position;

            // Check if we've reached the stop time
            if (_stopTime > 0 && current.TotalSeconds >= _stopTime)
            {
                StopPlaying();
                return;
            }

            if (mediaElement.NaturalDuration.HasTimeSpan)
            {
                // If stop time is set, show that as the total instead of full duration
                var total = _stopTime > 0 
                    ? TimeSpan.FromSeconds(_stopTime) 
                    : mediaElement.NaturalDuration.TimeSpan;
                txtProgress.Text = $"{TimespanFormat(current)} / {TimespanFormat(total)}";
            }
        }

        private string TimespanFormat(TimeSpan t)
        {
            return t.Hours > 0 ? t.ToString("hh\\:mm\\:ss") : t.ToString("mm\\:ss");
        }

        private void VideoPlayerWindow_MouseMove(object sender, MouseEventArgs e)
        {
            var currentPosition = e.GetPosition(this);

            // Only show cursor if mouse actually moved (not programmatic events)
            if (_lastMousePosition.HasValue && _lastMousePosition.Value == currentPosition)
                return;

            _lastMousePosition = currentPosition;
            ShowCursorTemporarily();
        }

        private void ShowCursorTemporarily()
        {
            ShowCursor();

            // Restart the hide timer
            _mouseHideTimer.Stop();
            _mouseHideTimer.Start();
        }

        private void ShowCursor()
        {
            if (_isCursorHidden)
            {
                Mouse.OverrideCursor = null;
                _isCursorHidden = false;
            }
        }

        private void HideCursor()
        {
            if (!_isCursorHidden)
            {
                Mouse.OverrideCursor = Cursors.None;
                _isCursorHidden = true;
            }
        }

        private void MouseHideTimer_Tick(object sender, EventArgs e)
        {
            _mouseHideTimer.Stop();
            HideCursor();
        }
    }
}
