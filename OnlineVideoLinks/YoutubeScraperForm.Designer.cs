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
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblResults = new System.Windows.Forms.Label();
            this.checkStrictSearch = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(248, 506);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 29);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSaveAll
            // 
            this.btnSaveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveAll.Location = new System.Drawing.Point(12, 506);
            this.btnSaveAll.Name = "btnSaveAll";
            this.btnSaveAll.Size = new System.Drawing.Size(230, 29);
            this.btnSaveAll.TabIndex = 4;
            this.btnSaveAll.Text = "Add Selected Videos To Game";
            this.btnSaveAll.UseVisualStyleBackColor = true;
            // 
            // panelVideoResults
            // 
            this.panelVideoResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelVideoResults.AutoScroll = true;
            this.panelVideoResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelVideoResults.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelVideoResults.Location = new System.Drawing.Point(13, 91);
            this.panelVideoResults.Name = "panelVideoResults";
            this.panelVideoResults.Size = new System.Drawing.Size(1011, 409);
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
            this.txtChannel.Size = new System.Drawing.Size(226, 22);
            this.txtChannel.TabIndex = 0;
            // 
            // txtQuery
            // 
            this.txtQuery.Location = new System.Drawing.Point(248, 34);
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(226, 22);
            this.txtQuery.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(245, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 17);
            this.label2.TabIndex = 106;
            this.label2.Text = "Game Name:";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(641, 30);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 30);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResults.Location = new System.Drawing.Point(13, 71);
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
            this.checkStrictSearch.Location = new System.Drawing.Point(480, 36);
            this.checkStrictSearch.Name = "checkStrictSearch";
            this.checkStrictSearch.Size = new System.Drawing.Size(155, 21);
            this.checkStrictSearch.TabIndex = 2;
            this.checkStrictSearch.Text = "Enable strict search";
            this.checkStrictSearch.UseVisualStyleBackColor = true;
            // 
            // YoutubeScraperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 547);
            this.Controls.Add(this.checkStrictSearch);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtQuery);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtChannel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelVideoResults);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveAll);
            this.Name = "YoutubeScraperForm";
            this.Text = "YoutubeScraperForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSaveAll;
        private System.Windows.Forms.FlowLayoutPanel panelVideoResults;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtChannel;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.CheckBox checkStrictSearch;
    }
}