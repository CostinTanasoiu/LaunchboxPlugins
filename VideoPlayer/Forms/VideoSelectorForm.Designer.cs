namespace VideoPlayer.Forms
{
    partial class VideoSelectorForm
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
            listBoxVideos = new ListBox();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            pictureBox3 = new PictureBox();
            label3 = new Label();
            label4 = new Label();
            pictureBox4 = new PictureBox();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // listBoxVideos
            // 
            listBoxVideos.BackColor = Color.FromArgb(37, 37, 38);
            listBoxVideos.BorderStyle = BorderStyle.FixedSingle;
            listBoxVideos.Font = new Font("Segoe UI", 20F);
            listBoxVideos.ForeColor = SystemColors.Info;
            listBoxVideos.FormattingEnabled = true;
            listBoxVideos.Items.AddRange(new object[] { "Game Trailer", "IGN Review", "Playthrough", "Lorem Ipsum", "Dolor Sit Amet", "Hello World", "Fred Flintstone", "Barney Rubble" });
            listBoxVideos.Location = new Point(12, 12);
            listBoxVideos.Margin = new Padding(12);
            listBoxVideos.Name = "listBoxVideos";
            listBoxVideos.Size = new Size(774, 182);
            listBoxVideos.TabIndex = 0;
            listBoxVideos.KeyUp += listBoxVideos_KeyUp;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Resources.XboxOne_A_100x100;
            pictureBox1.Location = new Point(93, 209);
            pictureBox1.Margin = new Padding(3, 3, 3, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(48, 48);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Resources.XboxOne_B_100x100;
            pictureBox2.Location = new Point(662, 209);
            pictureBox2.Margin = new Padding(3, 3, 3, 12);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(48, 48);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.ForeColor = SystemColors.Info;
            label1.Location = new Point(147, 218);
            label1.Name = "label1";
            label1.Size = new Size(112, 28);
            label1.TabIndex = 2;
            label1.Text = "Play Video";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label2.ForeColor = SystemColors.Info;
            label2.Location = new Point(716, 218);
            label2.Name = "label2";
            label2.Size = new Size(74, 28);
            label2.TabIndex = 2;
            label2.Text = "Cancel";
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Resources.Enter_Alt_Key_Light;
            pictureBox3.Location = new Point(12, 209);
            pictureBox3.Margin = new Padding(3, 3, 3, 12);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(48, 48);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 1;
            pictureBox3.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label3.ForeColor = SystemColors.Info;
            label3.Location = new Point(66, 218);
            label3.Name = "label3";
            label3.Size = new Size(21, 28);
            label3.TabIndex = 2;
            label3.Text = "/";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label4.ForeColor = SystemColors.Info;
            label4.Location = new Point(635, 218);
            label4.Name = "label4";
            label4.Size = new Size(21, 28);
            label4.TabIndex = 2;
            label4.Text = "/";
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Resources.Esc_Key_Light;
            pictureBox4.Location = new Point(581, 209);
            pictureBox4.Margin = new Padding(3, 3, 3, 12);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(48, 48);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 1;
            pictureBox4.TabStop = false;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(listBoxVideos);
            panel1.Controls.Add(pictureBox4);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 275);
            panel1.TabIndex = 3;
            // 
            // VideoSelectorForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(836, 320);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "VideoSelectorForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Select video to play";
            FormClosing += VideoSelectorForm_FormClosing;
            Load += VideoSelectorForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListBox listBoxVideos;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Panel panel1;
    }
}