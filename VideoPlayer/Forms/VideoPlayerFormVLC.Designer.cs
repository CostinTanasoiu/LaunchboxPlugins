namespace VideoPlayer.Forms
{
    partial class VideoPlayerFormVLC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoPlayerFormVLC));
            btnSkipFwd = new UserControls.PlayerButtonWithBadge();
            btnStop = new UserControls.PlayerButtonWithBadge();
            btnPlay = new UserControls.PlayerButtonWithBadge();
            pictureBox1 = new PictureBox();
            btnSkipBack = new UserControls.PlayerButtonWithBadge();
            flowLayoutPanel1 = new FlowLayoutPanel();
            vlcView = new LibVLCSharp.WinForms.VideoView();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)vlcView).BeginInit();
            SuspendLayout();
            // 
            // btnSkipFwd
            // 
            btnSkipFwd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSkipFwd.BadgeIcon = (Image)resources.GetObject("btnSkipFwd.BadgeIcon");
            btnSkipFwd.Location = new Point(377, 5);
            btnSkipFwd.MainIcon = (Image)resources.GetObject("btnSkipFwd.MainIcon");
            btnSkipFwd.Margin = new Padding(5, 5, 15, 5);
            btnSkipFwd.Name = "btnSkipFwd";
            btnSkipFwd.Size = new Size(76, 76);
            btnSkipFwd.TabIndex = 9;
            // 
            // btnStop
            // 
            btnStop.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnStop.BadgeIcon = (Image)resources.GetObject("btnStop.BadgeIcon");
            btnStop.Location = new Point(281, 5);
            btnStop.MainIcon = (Image)resources.GetObject("btnStop.MainIcon");
            btnStop.Margin = new Padding(5, 5, 15, 5);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(76, 76);
            btnStop.TabIndex = 8;
            // 
            // btnPlay
            // 
            btnPlay.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnPlay.BadgeIcon = (Image)resources.GetObject("btnPlay.BadgeIcon");
            btnPlay.Location = new Point(185, 5);
            btnPlay.MainIcon = (Image)resources.GetObject("btnPlay.MainIcon");
            btnPlay.Margin = new Padding(5, 5, 15, 5);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(76, 76);
            btnPlay.TabIndex = 7;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(5, 17);
            pictureBox1.Margin = new Padding(5, 5, 15, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(64, 64);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // btnSkipBack
            // 
            btnSkipBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSkipBack.BadgeIcon = (Image)resources.GetObject("btnSkipBack.BadgeIcon");
            btnSkipBack.Location = new Point(89, 5);
            btnSkipBack.MainIcon = (Image)resources.GetObject("btnSkipBack.MainIcon");
            btnSkipBack.Margin = new Padding(5, 5, 15, 5);
            btnSkipBack.Name = "btnSkipBack";
            btnSkipBack.Size = new Size(76, 76);
            btnSkipBack.TabIndex = 5;
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
            flowLayoutPanel1.Location = new Point(0, 364);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(800, 86);
            flowLayoutPanel1.TabIndex = 11;
            // 
            // vlcView
            // 
            vlcView.BackColor = Color.Black;
            vlcView.Dock = DockStyle.Fill;
            vlcView.Location = new Point(0, 0);
            vlcView.MediaPlayer = null;
            vlcView.Name = "vlcView";
            vlcView.Size = new Size(800, 364);
            vlcView.TabIndex = 12;
            // 
            // VideoPlayerFormVLC
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(800, 450);
            Controls.Add(vlcView);
            Controls.Add(flowLayoutPanel1);
            Name = "VideoPlayerFormVLC";
            Text = "VideoPlayerForm";
            WindowState = FormWindowState.Maximized;
            Load += VideoPlayerFormVLC_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)vlcView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private UserControls.PlayerButtonWithBadge btnSkipFwd;
        private UserControls.PlayerButtonWithBadge btnStop;
        private UserControls.PlayerButtonWithBadge btnPlay;
        private PictureBox pictureBox1;
        private UserControls.PlayerButtonWithBadge btnSkipBack;
        private FlowLayoutPanel flowLayoutPanel1;
        private LibVLCSharp.WinForms.VideoView vlcView;
    }
}