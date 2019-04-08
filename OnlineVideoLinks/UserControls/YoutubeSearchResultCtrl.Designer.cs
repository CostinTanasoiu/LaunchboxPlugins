namespace OnlineVideoLinks.UserControls
{
    partial class YoutubeSearchResultCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.LinkLabel();
            this.txtStopTime = new OnlineVideoLinks.UserControls.TimeTextBoxCtrl();
            this.txtStartTime = new OnlineVideoLinks.UserControls.TimeTextBoxCtrl();
            this.txtAltTitle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(27, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 84);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Location = new System.Drawing.Point(185, 20);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(200, 17);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "User * 90k views * 3 years ago";
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(188, 120);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 29);
            this.btnPlay.TabIndex = 3;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(269, 120);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 29);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(5, 43);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(18, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(185, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Start Time (mm:ss):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(396, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Stop Time (mm:ss):";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(185, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(40, 17);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.TabStop = true;
            this.lblTitle.Text = "Title";
            this.lblTitle.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblTitle_LinkClicked);
            // 
            // txtStopTime
            // 
            this.txtStopTime.Location = new System.Drawing.Point(527, 73);
            this.txtStopTime.Name = "txtStopTime";
            this.txtStopTime.Size = new System.Drawing.Size(66, 22);
            this.txtStopTime.TabIndex = 2;
            // 
            // txtStartTime
            // 
            this.txtStartTime.Location = new System.Drawing.Point(316, 73);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(66, 22);
            this.txtStartTime.TabIndex = 1;
            // 
            // txtAltTitle
            // 
            this.txtAltTitle.Location = new System.Drawing.Point(316, 45);
            this.txtAltTitle.Name = "txtAltTitle";
            this.txtAltTitle.Size = new System.Drawing.Size(277, 22);
            this.txtAltTitle.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Alt. Title (optional):";
            // 
            // YoutubeSearchResultCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAltTitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtStopTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtStartTime);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.pictureBox1);
            this.Name = "YoutubeSearchResultCtrl";
            this.Size = new System.Drawing.Size(722, 152);
            this.Load += new System.EventHandler(this.YoutubeSearchResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.CheckBox checkBox1;
        private TimeTextBoxCtrl txtStartTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private TimeTextBoxCtrl txtStopTime;
        private System.Windows.Forms.LinkLabel lblTitle;
        private System.Windows.Forms.TextBox txtAltTitle;
        private System.Windows.Forms.Label label3;
    }
}
