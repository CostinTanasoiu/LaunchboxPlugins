using SharpDX.DirectInput;
using SharpDX.XInput;
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
        private System.Windows.Forms.Timer _progressTimer;

        public VideoPlayerForm()
        {
            InitializeComponent();

            // Removing the border to make the form look full-screen
            this.FormBorderStyle = FormBorderStyle.None;

            // This ensures the form will receive all key events before any control on the form receives them.
            this.KeyPreview = true;

            // Ensure progress label is on top and positioned correctly
            lblProgress.BringToFront();
            this.Resize += VideoPlayerForm_Resize;

            // Timer to update progress display
            _progressTimer = new System.Windows.Forms.Timer
            {
                Interval = 500
            };
            _progressTimer.Tick += ProgressTimer_Tick;
        }

        private void VideoPlayerForm_Resize(object? sender, EventArgs e)
        {
            // Position label in bottom-right, vertically centered with flowLayoutPanel1
            int labelX = this.ClientSize.Width - lblProgress.Width - 20;
            int labelY = flowLayoutPanel1.Top + (flowLayoutPanel1.Height - lblProgress.Height) / 2;
            lblProgress.Location = new Point(labelX, labelY);
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
            ResetProgress();

            mediaPlayer.Ctlcontrols.play();
            _progressTimer.Start();
        }

        public void PlayPause()
        {
            if (IsPlaying())
                mediaPlayer.Ctlcontrols.pause();
            else
                mediaPlayer.Ctlcontrols.play();
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
                    StopPlaying(); break;
                case GamepadButtonFlags.DPadLeft:
                    SkipBackward(); break;
                case GamepadButtonFlags.DPadRight:
                    SkipForward(); break;
            }
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
            _progressTimer.Stop();
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

        private void ResetProgress()
        {
            lblProgress.Text = "--:-- / --:--";
        }

        private void ProgressTimer_Tick(object? sender, EventArgs e)
        {
            var current = TimeSpan.FromSeconds(mediaPlayer.Ctlcontrols.currentPosition);
            var currentFormatted = TimespanFormat(current);

            // Duration may be 0 for certain formats or while media is still loading
            if (mediaPlayer.currentMedia != null && mediaPlayer.currentMedia.duration > 0)
            {
                var total = TimeSpan.FromSeconds(mediaPlayer.currentMedia.duration);
                lblProgress.Text = $"{currentFormatted} / {TimespanFormat(total)}";
            }
            else
            {
                // No duration available - show only current position
                lblProgress.Text = currentFormatted;
            }
        }

        private string TimespanFormat(TimeSpan t)
        {
            return t.Hours > 0 ? t.ToString("hh\\:mm\\:ss") : t.ToString("mm\\:ss");
        }
    }
}
