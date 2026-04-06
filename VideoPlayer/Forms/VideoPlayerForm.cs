using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoPlayer.Models;

namespace VideoPlayer.Forms
{
    public partial class VideoPlayerForm : Form, IVideoPlayerPanel
    {
        const string TempVideoPath = "temp_video.mp4";
        const int SkipFwdSeconds = 15;
        const int SkipBwdSeconds = 15;

        GameVideo? _gameVideo;

        public VideoPlayerForm()
        {
            InitializeComponent();

            // Removing the border to make the form look full-screen
            this.FormBorderStyle = FormBorderStyle.None;

            // This ensures the form will receive all key events before any control on the form receives them.
            this.KeyPreview = true;
        }

        public bool IsPlaying()
        {
            return mediaPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying;
        }

        public async Task Play(GameVideo video)
        {
            if (IsPlaying())
                StopPlaying();

            this.Show();

            // Prepare mediaPlayer before loading a new video
            mediaPlayer.close();
            mediaPlayer.URL = null;

            mediaPlayer.URL = video.VideoPath;
            //mediaPlayer.URL = @"C:\Work_Personal\LaunchboxPlugins\Resources\BigBuckBunny_640x360.mp4";

            mediaPlayer.settings.volume = 50; // Set volume to 50%
            mediaPlayer.settings.mute = false;
            mediaPlayer.stretchToFit = true;

            mediaPlayer.Ctlcontrols.play();
        }

        public void PlayPause()
        {
            if (IsPlaying())
                mediaPlayer.Ctlcontrols.pause();
            else
                mediaPlayer.Ctlcontrols.play();
        }

        public void SkipBackward()
        {
            if (mediaPlayer.Ctlcontrols.currentPosition > SkipBwdSeconds)
                mediaPlayer.Ctlcontrols.currentPosition -= SkipBwdSeconds;
            else
                mediaPlayer.Ctlcontrols.currentPosition = 0;
        }

        public void SkipForward()
        {
            if (mediaPlayer.Ctlcontrols.currentPosition + SkipFwdSeconds < mediaPlayer.currentMedia.duration)
                mediaPlayer.Ctlcontrols.currentPosition += SkipFwdSeconds;
            else
                mediaPlayer.Ctlcontrols.currentPosition = mediaPlayer.currentMedia.duration;
        }

        public void StopPlaying()
        {
            mediaPlayer.Ctlcontrols.stop();
            mediaPlayer.close();
            _gameVideo = null;

            if (File.Exists(TempVideoPath))
                File.Delete(TempVideoPath);

            this.Hide();
        }

        private void VideoPlayerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopPlaying();

            mediaPlayer.Dispose();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            PlayPause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopPlaying();
        }

        private void btnSkipBack_Click(object sender, EventArgs e)
        {
            SkipBackward();
        }

        private void btnSkipFwd_Click(object sender, EventArgs e)
        {
            SkipForward();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                case Keys.Space:
                case Keys.A:
                    PlayPause(); break;
                case Keys.Escape:
                case Keys.B:
                    StopPlaying(); break;
                case Keys.Left:
                    SkipBackward(); break;
                case Keys.Right:
                    SkipForward(); break;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
            return true;
        }
    }
}
