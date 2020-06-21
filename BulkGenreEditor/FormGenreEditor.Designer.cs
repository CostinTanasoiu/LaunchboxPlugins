namespace BulkGenreEditor
{
    partial class FormGenreEditor
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
            this.lblInstructions = new System.Windows.Forms.Label();
            this.btnRemoveGenres = new System.Windows.Forms.Button();
            this.btnAddGenres = new System.Windows.Forms.Button();
            this.checklistGenres = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.multiSelectionBox = new BulkGenreEditor.UserControls.MultiSelectionBox();
            this.lblSelectionCount = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Location = new System.Drawing.Point(11, 70);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(488, 17);
            this.lblInstructions.TabIndex = 0;
            this.lblInstructions.Text = "Choose the genres that you want to add or remove for {0} selected game(s):";
            // 
            // btnRemoveGenres
            // 
            this.btnRemoveGenres.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveGenres.Location = new System.Drawing.Point(415, 9);
            this.btnRemoveGenres.Name = "btnRemoveGenres";
            this.btnRemoveGenres.Size = new System.Drawing.Size(202, 32);
            this.btnRemoveGenres.TabIndex = 5;
            this.btnRemoveGenres.Text = "Remove selected genres";
            this.btnRemoveGenres.UseVisualStyleBackColor = true;
            this.btnRemoveGenres.Click += new System.EventHandler(this.btnRemoveGenres_Click);
            // 
            // btnAddGenres
            // 
            this.btnAddGenres.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddGenres.Location = new System.Drawing.Point(3, 9);
            this.btnAddGenres.Name = "btnAddGenres";
            this.btnAddGenres.Size = new System.Drawing.Size(200, 32);
            this.btnAddGenres.TabIndex = 4;
            this.btnAddGenres.Text = "Add selected genres";
            this.btnAddGenres.UseVisualStyleBackColor = true;
            this.btnAddGenres.Click += new System.EventHandler(this.btnAddGenres_Click);
            // 
            // checklistGenres
            // 
            this.checklistGenres.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checklistGenres.CheckOnClick = true;
            this.checklistGenres.FormattingEnabled = true;
            this.checklistGenres.Location = new System.Drawing.Point(12, 90);
            this.checklistGenres.Name = "checklistGenres";
            this.checklistGenres.Size = new System.Drawing.Size(620, 242);
            this.checklistGenres.Sorted = true;
            this.checklistGenres.TabIndex = 3;
            this.checklistGenres.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checklistGenres_ItemCheck);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(605, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Search and add new or existing genres to the selection list. Press \'Enter\' to app" +
    "ly the selection:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(607, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Adding genres will not overwrite your selected games\' existing genres, but will o" +
    "nly append the new ones to each selected game. However, be careful with the remo" +
    "ve operation.";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(11, 365);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(620, 67);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Note";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(209, 14);
            this.progressBar.MarqueeAnimationSpeed = 20;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(200, 23);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 11;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.btnRemoveGenres, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAddGenres, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.progressBar, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(11, 438);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(620, 51);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // multiSelectionBox
            // 
            this.multiSelectionBox.AutocompleteItems = null;
            this.multiSelectionBox.Location = new System.Drawing.Point(14, 29);
            this.multiSelectionBox.Name = "multiSelectionBox";
            this.multiSelectionBox.Size = new System.Drawing.Size(618, 25);
            this.multiSelectionBox.TabIndex = 13;
            this.multiSelectionBox.SelectionCompleted += new BulkGenreEditor.UserControls.MultiSelectionBox.SelectionCompletedHandler(this.multiSelectionBox_SelectionCompleted);
            // 
            // lblSelectionCount
            // 
            this.lblSelectionCount.AutoSize = true;
            this.lblSelectionCount.Location = new System.Drawing.Point(12, 335);
            this.lblSelectionCount.Name = "lblSelectionCount";
            this.lblSelectionCount.Size = new System.Drawing.Size(127, 17);
            this.lblSelectionCount.TabIndex = 14;
            this.lblSelectionCount.Text = "Selected genres: 0";
            // 
            // FormGenreEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 498);
            this.Controls.Add(this.lblSelectionCount);
            this.Controls.Add(this.multiSelectionBox);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checklistGenres);
            this.Controls.Add(this.lblInstructions);
            this.MinimumSize = new System.Drawing.Size(574, 529);
            this.Name = "FormGenreEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bulk Add/Remove Genres";
            this.Load += new System.EventHandler(this.FormGenreEditor_Load);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Button btnRemoveGenres;
        private System.Windows.Forms.Button btnAddGenres;
        private System.Windows.Forms.CheckedListBox checklistGenres;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private UserControls.MultiSelectionBox multiSelectionBox;
        private System.Windows.Forms.Label lblSelectionCount;
    }
}