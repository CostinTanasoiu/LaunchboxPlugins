using OnlineVideoLinks.Utilities;
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
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using OnlineVideoLinks.Models;
using System.IO;

namespace OnlineVideoLinks.Forms
{
    public partial class VideoPlayerForm : Form, IVideoPlayer
    {
        const string TempVideoPath = "temp_video.mp4";
        const int SkipFwdSeconds = 15;
        const int SkipBwdSeconds = 15;
        const string LoadingAnimationResource = "OnlineVideoLinks.Resources.loading-animation.gif";

        private Timer _progressTimer;
        private bool _isClosing;

        public event EventHandler PlayerClosed;

        public bool IsPlaying => mediaPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying;

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
            this.Shown += (s, e) => VideoPlayerForm_Resize(s, e); // Recalculate when form is shown

            // Timer to update progress display
            _progressTimer = new Timer
            {
                Interval = 500
            };
            _progressTimer.Tick += ProgressTimer_Tick;

            // Load the loading animation from embedded resource
            loadingAnimation.LoadFromEmbeddedResource(LoadingAnimationResource);
        }

        private void VideoPlayerForm_Resize(object? sender, EventArgs e)
        {
            // Position label in bottom-right, vertically centered with flowLayoutPanel1
            int labelX = this.ClientSize.Width - lblProgress.Width - 20;
            int labelY = flowLayoutPanel1.Top + (flowLayoutPanel1.Height - lblProgress.Height) / 2;
            lblProgress.Location = new Point(labelX, labelY);

            // Size loading animation up to 600x600, constrained by available space
            const int MaxAnimationSize = 600;
            const int Padding = 40; // Minimum padding from edges
            int availableWidth = this.ClientSize.Width - (Padding * 2);
            int availableHeight = this.ClientSize.Height - flowLayoutPanel1.Height - (Padding * 2);
            int animationSize = Math.Min(MaxAnimationSize, Math.Min(availableWidth, availableHeight));
            animationSize = Math.Max(animationSize, 100); // Minimum size of 100

            System.Diagnostics.Debug.WriteLine($"Form ClientSize: {this.ClientSize.Width}x{this.ClientSize.Height}, " +
                $"FlowPanel Height: {flowLayoutPanel1.Height}, " +
                $"Available: {availableWidth}x{availableHeight}, " +
                $"Animation Size: {animationSize}");

            loadingAnimation.Size = new Size(animationSize, animationSize);

            // Center loading animation in the form (above the control panel)
            loadingAnimation.Location = new Point(
                (this.ClientSize.Width - loadingAnimation.Width) / 2,
                (this.ClientSize.Height - flowLayoutPanel1.Height - loadingAnimation.Height) / 2);
        }

        public async Task Play(GameVideo video)
        {
            if (IsPlaying)
                StopPlaying();

            ResetProgress();

            this.Show();

            // Show loading indicator
            loadingAnimation.Visible = true;
            loadingAnimation.BringToFront();
            loadingAnimation.StartAnimation();

            // Prepare mediaPlayer before loading a new video
            mediaPlayer.close();
            mediaPlayer.URL = null;

            // Subscribe to state change to hide progress bar when ready
            mediaPlayer.OpenStateChange += MediaPlayer_OpenStateChange;
            mediaPlayer.PlayStateChange += MediaPlayer_PlayStateChange;

            //mediaPlayer.URL = video.VideoPath;
            if (VideoMetadataUtilities.IsYoutubeUrl(video.VideoPath))
                await LoadYoutubeVideo(video.VideoPath);
            else
                LoadRegularVideo(video.VideoPath);

            mediaPlayer.settings.volume = 50; // Set volume to 50%
            mediaPlayer.settings.mute = false;
            mediaPlayer.stretchToFit = true;

            mediaPlayer.Ctlcontrols.play();
            _progressTimer.Start();
        }

        private void LoadRegularVideo(string videoPath)
        {
            Uri mediaUri;

            // Check if it's a network URL or a local file path
            if (Uri.TryCreate(videoPath, UriKind.Absolute, out var uri) &&
                (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            {
                mediaUri = uri;
            }
            else
            {
                // Local file path - resolve relative paths to absolute
                var filePath = Path.IsPathRooted(videoPath)
                    ? videoPath
                    : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, videoPath);

                mediaUri = new Uri(filePath);
            }

            mediaPlayer.URL = mediaUri.ToString();
        }

        private async Task LoadYoutubeVideo(string videoPath)
        {
            var playablePath = await YoutubeDownloader.GetPlayableVideoPath(videoPath, TempVideoPath);
            mediaPlayer.URL = playablePath;
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

        public void PlayPause()
        {
            if (IsPlaying)
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
            if (_isClosing)
                return;

            _isClosing = true;
            _progressTimer.Stop();
            loadingAnimation.StopAnimation();
            loadingAnimation.Visible = false;
            mediaPlayer.OpenStateChange -= MediaPlayer_OpenStateChange;
            mediaPlayer.PlayStateChange -= MediaPlayer_PlayStateChange;
            mediaPlayer.Ctlcontrols.stop();
            mediaPlayer.close();

            if (File.Exists(TempVideoPath))
                File.Delete(TempVideoPath);

            this.Close();
        }

        private void MediaPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            // wmppsMediaEnded = 8 means media playback has ended
            if (e.newState == (int)WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                StopPlaying();
            }
        }

        private void MediaPlayer_OpenStateChange(object sender, AxWMPLib._WMPOCXEvents_OpenStateChangeEvent e)
        {
            // wmposMediaOpen = 13 means media is fully open and ready to play
            if (e.newState == (int)WMPLib.WMPOpenState.wmposMediaOpen)
            {
                loadingAnimation.StopAnimation();
                loadingAnimation.Visible = false;
                mediaPlayer.OpenStateChange -= MediaPlayer_OpenStateChange;
            }
        }

        private void VideoPlayerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Ensure cleanup happens even if form is closed directly (e.g., via X button)
            if (!_isClosing)
            {
                _isClosing = true;
                _progressTimer.Stop();
                loadingAnimation.StopAnimation();
                mediaPlayer.OpenStateChange -= MediaPlayer_OpenStateChange;
                mediaPlayer.PlayStateChange -= MediaPlayer_PlayStateChange;
                mediaPlayer.Ctlcontrols.stop();
                mediaPlayer.close();

                if (File.Exists(TempVideoPath))
                    File.Delete(TempVideoPath);
            }

            mediaPlayer.Dispose();
            PlayerClosed?.Invoke(this, EventArgs.Empty);
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
                    StopPlaying(); break;
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
