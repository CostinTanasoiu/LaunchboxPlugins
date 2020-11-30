using LibVLCSharp.Shared;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace OnlineVideoLinks.WPF
{
    /// <summary>
    /// Interaction logic for VideoPlayerWindow.xaml
    /// </summary>
    public partial class VideoPlayerWindow : Window
    {
        LibVLC _libVLC;
        MediaPlayer _mediaPlayer;

        public VideoPlayerWindow()
        {
            InitializeComponent();

            LibVLCSharp.Shared.Core.Initialize();
            _libVLC = new LibVLC(enableDebugLogs: true);
            _mediaPlayer = new MediaPlayer(_libVLC);
            vlcView.MediaPlayer = _mediaPlayer;

            _mediaPlayer.TimeChanged += _mediaPlayer_TimeChanged;
            _mediaPlayer.EndReached += _mediaPlayer_EndReached;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            using (var media = new Media(_libVLC, new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4")))
            {
                vlcView.MediaPlayer.Play(media);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _mediaPlayer.Stop();
            _mediaPlayer.Dispose();
            _libVLC.Dispose();
        }

        public void SkipForward()
        {
            _mediaPlayer.Time += 10000;
        }

        public void SkipBackward()
        {
            if (_mediaPlayer.Time > 10000)
                _mediaPlayer.Time -= 10000;
            else _mediaPlayer.Time = 0;
        }

        public void PlayPause()
        {
            _mediaPlayer.Pause();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            PlayPause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                    this.Close(); break;
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
            this.Close();
        }

        private string TimespanFormat(TimeSpan t)
        {
            return t.Hours > 0 ? t.ToString("hh\\:mm\\:ss") : t.ToString("mm\\:ss");
        }
    }
}
