using OnlineVideoLinks.Models;
using OnlineVideoLinks.Utilities;
using SharpDX.XInput;
using System;
using System.IO;
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

        private GameVideo _gameVideo;
        private DispatcherTimer _progressTimer;
        private bool _isPlaying;
        private bool _isClosing;

        public event EventHandler PlayerClosed;

        public bool IsPlaying => _isPlaying;

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
        }

        /// <summary>
        /// Loads a game video and immediately starts playing it.
        /// </summary>
        /// <param name="video"></param>
        public async Task Play(GameVideo video)
        {
            _gameVideo = video;

            this.Show();

            // Show loading indicator
            // TODO
            progressBar.Visibility = Visibility.Visible;

            if (VideoMetadataUtilities.IsYoutubeUrl(_gameVideo.VideoPath))
                await LoadYoutubeVideo();
            else
                LoadRegularVideo();
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

        private async void Window_ContentRendered(object sender, EventArgs e)
        {
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

        private async Task LoadYoutubeVideo()
        {
            var videoPath = await YoutubeDownloader.GetPlayableVideoPath(_gameVideo.VideoPath, TempVideoPath);

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
            if (mediaElement.NaturalDuration.HasTimeSpan)
            {
                var current = mediaElement.Position;
                var total = mediaElement.NaturalDuration.TimeSpan;
                txtProgress.Text = $"{TimespanFormat(current)} / {TimespanFormat(total)}";
            }
        }

        private string TimespanFormat(TimeSpan t)
        {
            return t.Hours > 0 ? t.ToString("hh\\:mm\\:ss") : t.ToString("mm\\:ss");
        }
    }
}
