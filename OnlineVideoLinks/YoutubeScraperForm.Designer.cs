namespace OnlineVideoLinks
{
    partial class YoutubeScraperForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSaveAll = new System.Windows.Forms.Button();
            this.panelVideoResults = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtChannel = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblResults = new System.Windows.Forms.Label();
            this.checkStrictSearch = new System.Windows.Forms.CheckBox();
            this.checkSkipGamesWithVideos = new System.Windows.Forms.CheckBox();
            this.numericMaxItemsPerGame = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxItemsPerGame)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(309, 506);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 29);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSaveAll
            // 
            this.btnSaveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveAll.Location = new System.Drawing.Point(12, 506);
            this.btnSaveAll.Name = "btnSaveAll";
            this.btnSaveAll.Size = new System.Drawing.Size(291, 29);
            this.btnSaveAll.TabIndex = 5;
            this.btnSaveAll.Text = "Add selected videos to selected game(s)";
            this.btnSaveAll.UseVisualStyleBackColor = true;
            this.btnSaveAll.Click += new System.EventHandler(this.btnSaveAll_Click);
            // 
            // panelVideoResults
            // 
            this.panelVideoResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelVideoResults.AutoScroll = true;
            this.panelVideoResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelVideoResults.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelVideoResults.Location = new System.Drawing.Point(13, 133);
            this.panelVideoResults.Name = "panelVideoResults";
            this.panelVideoResults.Size = new System.Drawing.Size(1011, 367);
            this.panelVideoResults.TabIndex = 103;
            this.panelVideoResults.WrapContents = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 17);
            this.label1.TabIndex = 104;
            this.label1.Text = "YouTube Channel:";
            // 
            // txtChannel
            // 
            this.txtChannel.Location = new System.Drawing.Point(16, 34);
            this.txtChannel.Name = "txtChannel";
            this.txtChannel.Size = new System.Drawing.Size(287, 22);
            this.txtChannel.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(16, 62);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 30);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResults.Location = new System.Drawing.Point(13, 113);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(67, 17);
            this.lblResults.TabIndex = 109;
            this.lblResults.Text = "Results:";
            // 
            // checkStrictSearch
            // 
            this.checkStrictSearch.AutoSize = true;
            this.checkStrictSearch.Checked = true;
            this.checkStrictSearch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkStrictSearch.Location = new System.Drawing.Point(309, 61);
            this.checkStrictSearch.Name = "checkStrictSearch";
            this.checkStrictSearch.Size = new System.Drawing.Size(427, 21);
            this.checkStrictSearch.TabIndex = 2;
            this.checkStrictSearch.Text = "Show only video results which have the game name in their title";
            this.checkStrictSearch.UseVisualStyleBackColor = true;
            // 
            // checkSkipGamesWithVideos
            // 
            this.checkSkipGamesWithVideos.AutoSize = true;
            this.checkSkipGamesWithVideos.Checked = true;
            this.checkSkipGamesWithVideos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkSkipGamesWithVideos.Location = new System.Drawing.Point(309, 34);
            this.checkSkipGamesWithVideos.Name = "checkSkipGamesWithVideos";
            this.checkSkipGamesWithVideos.Size = new System.Drawing.Size(287, 21);
            this.checkSkipGamesWithVideos.TabIndex = 1;
            this.checkSkipGamesWithVideos.Text = "Skip games that already have video links";
            this.checkSkipGamesWithVideos.UseVisualStyleBackColor = true;
            // 
            // numericMaxItemsPerGame
            // 
            this.numericMaxItemsPerGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericMaxItemsPerGame.Location = new System.Drawing.Point(957, 33);
            this.numericMaxItemsPerGame.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericMaxItemsPerGame.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMaxItemsPerGame.Name = "numericMaxItemsPerGame";
            this.numericMaxItemsPerGame.Size = new System.Drawing.Size(67, 22);
            this.numericMaxItemsPerGame.TabIndex = 3;
            this.numericMaxItemsPerGame.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(774, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 17);
            this.label2.TabIndex = 111;
            this.label2.Text = "Max. No. results per game:";
            // 
            // YoutubeScraperForm
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1036, 547);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericMaxItemsPerGame);
            this.Controls.Add(this.checkSkipGamesWithVideos);
            this.Controls.Add(this.checkStrictSearch);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtChannel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelVideoResults);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveAll);
            this.Name = "YoutubeScraperForm";
            this.Text = "YoutubeScraperForm";
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxItemsPerGame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSaveAll;
        private System.Windows.Forms.FlowLayoutPanel panelVideoResults;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtChannel;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.CheckBox checkStrictSearch;
        private System.Windows.Forms.CheckBox checkSkipGamesWithVideos;
        private System.Windows.Forms.NumericUpDown numericMaxItemsPerGame;
        private System.Windows.Forms.Label label2;
    }
}