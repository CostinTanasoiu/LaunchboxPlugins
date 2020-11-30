using LibVLCSharp.Shared;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

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

            vlcView.KeyUp += VlcView_KeyUp;
        }

        private void VlcView_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            MessageBox.Show("Pressed key");
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

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayer.Pause();
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
    }
}
