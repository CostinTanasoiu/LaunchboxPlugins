namespace OnlineVideoLinks.Forms
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
            listBoxVideos = new System.Windows.Forms.ListBox();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            pictureBox3 = new System.Windows.Forms.PictureBox();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            pictureBox4 = new System.Windows.Forms.PictureBox();
            panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // listBoxVideos
            // 
            listBoxVideos.BackColor = System.Drawing.Color.FromArgb(37, 37, 38);
            listBoxVideos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            listBoxVideos.Font = new System.Drawing.Font("Segoe UI", 20F);
            listBoxVideos.ForeColor = System.Drawing.SystemColors.Info;
            listBoxVideos.FormattingEnabled = true;
            listBoxVideos.Items.AddRange(new object[] { "Game Trailer", "IGN Review", "Playthrough", "Lorem Ipsum", "Dolor Sit Amet", "Hello World", "Fred Flintstone", "Barney Rubble" });
            listBoxVideos.Location = new System.Drawing.Point(15, 15);
            listBoxVideos.Margin = new System.Windows.Forms.Padding(15, 15, 15, 15);
            listBoxVideos.Name = "listBoxVideos";
            listBoxVideos.Size = new System.Drawing.Size(967, 218);
            listBoxVideos.TabIndex = 0;
            listBoxVideos.KeyUp += listBoxVideos_KeyUp;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Resources.XboxOne_A;
            pictureBox1.Location = new System.Drawing.Point(116, 261);
            pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 15);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(60, 60);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Resources.XboxOne_B;
            pictureBox2.Location = new System.Drawing.Point(828, 261);
            pictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 15);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(60, 60);
            pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            label1.ForeColor = System.Drawing.SystemColors.Info;
            label1.Location = new System.Drawing.Point(184, 272);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(135, 32);
            label1.TabIndex = 2;
            label1.Text = "Play Video";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            label2.ForeColor = System.Drawing.SystemColors.Info;
            label2.Location = new System.Drawing.Point(895, 272);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(89, 32);
            label2.TabIndex = 2;
            label2.Text = "Cancel";
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Resources.Enter_Alt_Key_Light;
            pictureBox3.Location = new System.Drawing.Point(15, 261);
            pictureBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 15);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new System.Drawing.Size(60, 60);
            pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 1;
            pictureBox3.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            label3.ForeColor = System.Drawing.SystemColors.Info;
            label3.Location = new System.Drawing.Point(82, 272);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(25, 32);
            label3.TabIndex = 2;
            label3.Text = "/";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            label4.ForeColor = System.Drawing.SystemColors.Info;
            label4.Location = new System.Drawing.Point(794, 272);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(25, 32);
            label4.TabIndex = 2;
            label4.Text = "/";
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Resources.Esc_Key_Light;
            pictureBox4.Location = new System.Drawing.Point(726, 261);
            pictureBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 15);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new System.Drawing.Size(60, 60);
            pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 1;
            pictureBox4.TabStop = false;
            // 
            // panel1
            // 
            panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel1.Controls.Add(listBoxVideos);
            panel1.Controls.Add(pictureBox4);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(label2);
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Margin = new System.Windows.Forms.Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1000, 343);
            panel1.TabIndex = 3;
            // 
            // VideoSelectorForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            ClientSize = new System.Drawing.Size(1045, 400);
            Controls.Add(panel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "VideoSelectorForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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