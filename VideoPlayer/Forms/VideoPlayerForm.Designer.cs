namespace VideoPlayer.Forms
{
    partial class VideoPlayerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoPlayerForm));
            btnSkipFwd = new VideoPlayer.Forms.UserControls.PlayerButtonWithBadge();
            btnStop = new VideoPlayer.Forms.UserControls.PlayerButtonWithBadge();
            btnPlay = new VideoPlayer.Forms.UserControls.PlayerButtonWithBadge();
            pictureBox1 = new PictureBox();
            btnSkipBack = new VideoPlayer.Forms.UserControls.PlayerButtonWithBadge();
            mediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            flowLayoutPanel1 = new FlowLayoutPanel();
            lblProgress = new Label();
            lottieLoading = new VideoPlayer.Forms.UserControls.LottieAnimationControl();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mediaPlayer).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnSkipFwd
            // 
            btnSkipFwd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSkipFwd.BadgeIcon = Resources.d_pad_right;
            btnSkipFwd.Location = new Point(471, 6);
            btnSkipFwd.MainIcon = Resources.next;
            btnSkipFwd.Margin = new Padding(6, 6, 19, 6);
            btnSkipFwd.Name = "btnSkipFwd";
            btnSkipFwd.Size = new Size(95, 95);
            btnSkipFwd.TabIndex = 9;
            btnSkipFwd.Click += btnSkipFwd_Click;
            // 
            // btnStop
            // 
            btnStop.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnStop.BadgeIcon = Resources.ButtonB;
            btnStop.Location = new Point(351, 6);
            btnStop.MainIcon = Resources.stop;
            btnStop.Margin = new Padding(6, 6, 19, 6);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(95, 95);
            btnStop.TabIndex = 8;
            btnStop.Click += btnStop_Click;
            // 
            // btnPlay
            // 
            btnPlay.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnPlay.BadgeIcon = Resources.ButtonA;
            btnPlay.Location = new Point(231, 6);
            btnPlay.MainIcon = Resources.play_pause;
            btnPlay.Margin = new Padding(6, 6, 19, 6);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(95, 95);
            btnPlay.TabIndex = 7;
            btnPlay.Click += btnPlay_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pictureBox1.Image = Resources.game_controller;
            pictureBox1.Location = new Point(6, 21);
            pictureBox1.Margin = new Padding(6, 6, 19, 6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(80, 80);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // btnSkipBack
            // 
            btnSkipBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSkipBack.BadgeIcon = Resources.d_pad_left;
            btnSkipBack.Location = new Point(111, 6);
            btnSkipBack.MainIcon = Resources.previous;
            btnSkipBack.Margin = new Padding(6, 6, 19, 6);
            btnSkipBack.Name = "btnSkipBack";
            btnSkipBack.Size = new Size(95, 95);
            btnSkipBack.TabIndex = 5;
            btnSkipBack.Click += btnSkipBack_Click;
            // 
            // mediaPlayer
            // 
            mediaPlayer.Dock = DockStyle.Fill;
            mediaPlayer.Enabled = true;
            mediaPlayer.Location = new Point(0, 0);
            mediaPlayer.Margin = new Padding(0);
            mediaPlayer.Name = "mediaPlayer";
            mediaPlayer.OcxState = (AxHost.State)resources.GetObject("mediaPlayer.OcxState");
            mediaPlayer.Size = new Size(1000, 562);
            mediaPlayer.TabIndex = 10;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(pictureBox1);
            flowLayoutPanel1.Controls.Add(btnSkipBack);
            flowLayoutPanel1.Controls.Add(btnPlay);
            flowLayoutPanel1.Controls.Add(btnStop);
            flowLayoutPanel1.Controls.Add(btnSkipFwd);
            flowLayoutPanel1.Dock = DockStyle.Bottom;
            flowLayoutPanel1.Location = new Point(0, 455);
            flowLayoutPanel1.Margin = new Padding(4, 4, 4, 4);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1000, 107);
            flowLayoutPanel1.TabIndex = 11;
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = false;
            lblProgress.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            lblProgress.ForeColor = Color.White;
            lblProgress.Location = new Point(800, 496);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(300, 40);
            lblProgress.TabIndex = 12;
            lblProgress.Text = "--:-- / --:--";
            lblProgress.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lottieLoading
            // 
            lottieLoading.Location = new Point(400, 200);
            lottieLoading.Name = "lottieLoading";
            lottieLoading.Size = new Size(200, 200);
            lottieLoading.TabIndex = 13;
            lottieLoading.Visible = false;
            // 
            // VideoPlayerForm
            //
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1000, 562);
            Controls.Add(lottieLoading);
            Controls.Add(lblProgress);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(mediaPlayer);
            Margin = new Padding(4, 4, 4, 4);
            Name = "VideoPlayerForm";
            Text = "VideoPlayerForm";
            WindowState = FormWindowState.Maximized;
            FormClosing += VideoPlayerForm_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)mediaPlayer).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private UserControls.PlayerButtonWithBadge btnSkipFwd;
        private UserControls.PlayerButtonWithBadge btnStop;
        private UserControls.PlayerButtonWithBadge btnPlay;
        private PictureBox pictureBox1;
        private UserControls.PlayerButtonWithBadge btnSkipBack;
        private AxWMPLib.AxWindowsMediaPlayer mediaPlayer;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label lblProgress;
        private UserControls.LottieAnimationControl lottieLoading;
    }
}