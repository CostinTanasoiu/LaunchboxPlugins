using LibVLCSharp.Shared;
using OnlineVideoLinks.Models;
using OnlineVideoLinks.Utilities;
using SharpDX.XInput;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OnlineVideoLinks.WPF
{
    /// <summary>
    /// Interaction logic for VideoPlayerWindow.xaml
    /// </summary>
    public partial class VideoPlayerWindow : Window
    {
        //[DllImport("libvlccore.dll")]
        //private static extern void Initialize(string? libvlcDirectoryPath = null);

        const string TempVideoPath = "temp_video.mp4";

        LibVLC _libVLC;
        MediaPlayer _mediaPlayer;
        GameVideo _gameVideo;

        public VideoPlayerWindow(GameVideo gameVideo)
        {
            InitializeComponent();

            LibVLCSharp.Shared.Core.Initialize();
            _libVLC = new LibVLC(enableDebugLogs: true);
            _libVLC.SetLogFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs\\libvlc.log"));

            _gameVideo = gameVideo;

            _mediaPlayer = new MediaPlayer(_libVLC);
            _mediaPlayer.TimeChanged += _mediaPlayer_TimeChanged;
            _mediaPlayer.EndReached += _mediaPlayer_EndReached;

            vlcView.MediaPlayer = _mediaPlayer;
        }

        /// <summary>
        /// Loads a game video and immediately starts playing it.
        /// </summary>
        /// <param name="video"></param>
        public void LoadAndPlay(GameVideo video)
        {
            _gameVideo = video;
            this.ShowDialog();
        }

        /// <summary>
        /// Stops playing and closes the window.
        /// </summary>
        public void StopAndClose()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                this.Close();
            }));
        }

        /// <summary>
        /// Checks whether the loaded video is currently playing.
        /// </summary>
        public bool IsPlaying()
        {
            return _mediaPlayer.IsPlaying;
        }

        /// <summary>
        /// Skips 10 seconds forward.
        /// </summary>
        public void SkipForward()
        {
            _mediaPlayer.Time += 10000;
        }

        /// <summary>
        /// Skips 10 seconds backward.
        /// </summary>
        public void SkipBackward()
        {
            if (_mediaPlayer.Time > 10000)
                _mediaPlayer.Time -= 10000;
            else _mediaPlayer.Time = 0;
        }

        /// <summary>
        /// Toggles the play/pause function.
        /// </summary>
        public void PlayPause()
        {
            _mediaPlayer.Pause();
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

        private async void vlcView_Loaded(object sender, RoutedEventArgs e)
        {
            if (_gameVideo != null)
            {
                progressBar.Visibility = Visibility.Visible;

                if (VideoMetadataUtilities.IsYoutubeUrl(_gameVideo.VideoPath))
                    await LoadYoutubeVideo();
                else
                    await LoadRegularVideo();
            }
        }

        private async Task LoadRegularVideo()
        {
            var media = new Media(_libVLC, new Uri(_gameVideo.VideoPath));

            await media.Parse(MediaParseOptions.ParseNetwork);
            var subitem = media.SubItems.FirstOrDefault();

            progressBar.Visibility = Visibility.Collapsed;

            if (subitem != null)
                vlcView.MediaPlayer.Play(subitem);
            else
                vlcView.MediaPlayer.Play(media);
        }

        private async Task LoadYoutubeVideo()
        {
            await YoutubeDownloader.DownloadTimestampedVideoMP4(_gameVideo.VideoPath, TempVideoPath, _gameVideo.StartTime, _gameVideo.StopTime);

            var media = new Media(_libVLC, TempVideoPath, FromType.FromPath);

            progressBar.Visibility = Visibility.Collapsed;
            vlcView.MediaPlayer.Play(media);
        }

        private async void Window_ContentRendered(object sender, EventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _mediaPlayer.Stop();

            _mediaPlayer.Media.Dispose();
            _mediaPlayer.Dispose();

            _libVLC.CloseLogFile();
            _libVLC.Dispose();

            File.Delete(TempVideoPath);
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            PlayPause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            StopAndClose();
        }

        private void btnSkipBack_Click(object sender, RoutedEventArgs e)
        {
            SkipBackward();
        }

        private void btnSkipFwd_Click(object sender, RoutedEventArgs e)
        {
            SkipForward();
        }

        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                case Key.Space:
                case Key.A:
                    PlayPause(); break;
                case Key.Escape:
                case Key.B:
                    StopAndClose(); break;
                case Key.Left:
                    SkipBackward(); break;
                case Key.Right:
                    SkipForward(); break;
            }
        }

        private void _mediaPlayer_TimeChanged(object sender, MediaPlayerTimeChangedEventArgs e)
        {
            var current = TimeSpan.FromMilliseconds(_mediaPlayer.Time);
            var total = TimeSpan.FromMilliseconds(_mediaPlayer.Length);

            Dispatcher.BeginInvoke(new Action(() =>
            {
                txtProgress.Text = $"{TimespanFormat(current)} / {TimespanFormat(total)}";
            }));
        }

        private void _mediaPlayer_EndReached(object sender, EventArgs e)
        {
            StopAndClose();
        }

        private string TimespanFormat(TimeSpan t)
        {
            return t.Hours > 0 ? t.ToString("hh\\:mm\\:ss") : t.ToString("mm\\:ss");
        }
    }
}
