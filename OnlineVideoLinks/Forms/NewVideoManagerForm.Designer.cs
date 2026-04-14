namespace OnlineVideoLinks.Forms
{
    partial class NewVideoManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewVideoManagerForm));
            this.btnPlus = new System.Windows.Forms.PictureBox();
            this.txtVideoPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.gridVideos = new System.Windows.Forms.DataGridView();
            this.panelNewVideo = new System.Windows.Forms.Panel();
            this.btnOkVideoUrl = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.btnPlus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVideos)).BeginInit();
            this.panelNewVideo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPlus
            // 
            this.btnPlus.Image = ((System.Drawing.Image)(resources.GetObject("btnPlus.Image")));
            this.btnPlus.Location = new System.Drawing.Point(352, 179);
            this.btnPlus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(96, 96);
            this.btnPlus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPlus.TabIndex = 0;
            this.btnPlus.TabStop = false;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // txtVideoPath
            // 
            this.txtVideoPath.Location = new System.Drawing.Point(0, 24);
            this.txtVideoPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtVideoPath.Name = "txtVideoPath";
            this.txtVideoPath.Size = new System.Drawing.Size(674, 27);
            this.txtVideoPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Video URL / Path";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Title";
            this.dataGridViewTextBoxColumn1.HeaderText = "Title";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "VideoPath";
            this.dataGridViewTextBoxColumn2.HeaderText = "Video file path / URL";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "StartTime";
            this.dataGridViewTextBoxColumn3.HeaderText = "Start (s)";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 125;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "StopTime";
            this.dataGridViewTextBoxColumn4.HeaderText = "Stop (s)";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 125;
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.HeaderText = "";
            this.dataGridViewButtonColumn1.MinimumWidth = 6;
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.Text = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.UseColumnTextForButtonValue = true;
            this.dataGridViewButtonColumn1.Width = 50;
            // 
            // dataGridViewButtonColumn2
            // 
            this.dataGridViewButtonColumn2.HeaderText = "";
            this.dataGridViewButtonColumn2.MinimumWidth = 6;
            this.dataGridViewButtonColumn2.Name = "dataGridViewButtonColumn2";
            this.dataGridViewButtonColumn2.Text = "dataGridViewButtonColumn2";
            this.dataGridViewButtonColumn2.UseColumnTextForButtonValue = true;
            this.dataGridViewButtonColumn2.Width = 50;
            // 
            // gridVideos
            // 
            this.gridVideos.AllowUserToAddRows = false;
            this.gridVideos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridVideos.ColumnHeadersHeight = 29;
            this.gridVideos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridVideos.Location = new System.Drawing.Point(14, 16);
            this.gridVideos.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridVideos.Name = "gridVideos";
            this.gridVideos.RowHeadersWidth = 51;
            this.gridVideos.Size = new System.Drawing.Size(774, 155);
            this.gridVideos.TabIndex = 100;
            // 
            // panelNewVideo
            // 
            this.panelNewVideo.Controls.Add(this.btnOkVideoUrl);
            this.panelNewVideo.Controls.Add(this.txtVideoPath);
            this.panelNewVideo.Controls.Add(this.label1);
            this.panelNewVideo.Location = new System.Drawing.Point(14, 283);
            this.panelNewVideo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelNewVideo.Name = "panelNewVideo";
            this.panelNewVideo.Size = new System.Drawing.Size(774, 175);
            this.panelNewVideo.TabIndex = 101;
            this.panelNewVideo.Visible = false;
            // 
            // btnOkVideoUrl
            // 
            this.btnOkVideoUrl.Location = new System.Drawing.Point(680, 24);
            this.btnOkVideoUrl.Name = "btnOkVideoUrl";
            this.btnOkVideoUrl.Size = new System.Drawing.Size(94, 29);
            this.btnOkVideoUrl.TabIndex = 3;
            this.btnOkVideoUrl.Text = "OK";
            this.btnOkVideoUrl.UseVisualStyleBackColor = true;
            this.btnOkVideoUrl.Click += new System.EventHandler(this.btnOkVideoUrl_Click);
            // 
            // NewVideoManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 620);
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.panelNewVideo);
            this.Controls.Add(this.gridVideos);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "NewVideoManagerForm";
            this.Text = "NewVideoManagerForm";
            ((System.ComponentModel.ISupportInitialize)(this.btnPlus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVideos)).EndInit();
            this.panelNewVideo.ResumeLayout(false);
            this.panelNewVideo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox btnPlus;
        private System.Windows.Forms.TextBox txtVideoPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn2;
        private System.Windows.Forms.DataGridView gridVideos;
        private System.Windows.Forms.Panel panelNewVideo;
        private System.Windows.Forms.Button btnOkVideoUrl;
    }
}