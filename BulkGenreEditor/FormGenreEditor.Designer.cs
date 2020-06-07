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
            this.btnCustomGenre = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboGenres = new System.Windows.Forms.ComboBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
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
            this.btnRemoveGenres.Location = new System.Drawing.Point(397, 9);
            this.btnRemoveGenres.Name = "btnRemoveGenres";
            this.btnRemoveGenres.Size = new System.Drawing.Size(192, 32);
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
            this.btnAddGenres.Size = new System.Drawing.Size(191, 32);
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
            this.checklistGenres.Size = new System.Drawing.Size(592, 242);
            this.checklistGenres.Sorted = true;
            this.checklistGenres.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(379, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Search and add new or existing genres to the selection list:";
            // 
            // btnCustomGenre
            // 
            this.btnCustomGenre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCustomGenre.Location = new System.Drawing.Point(562, 29);
            this.btnCustomGenre.Name = "btnCustomGenre";
            this.btnCustomGenre.Size = new System.Drawing.Size(40, 24);
            this.btnCustomGenre.TabIndex = 2;
            this.btnCustomGenre.Text = "➤";
            this.btnCustomGenre.UseVisualStyleBackColor = true;
            this.btnCustomGenre.Click += new System.EventHandler(this.btnCustomGenre_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(579, 54);
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
            this.groupBox1.Location = new System.Drawing.Point(11, 344);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(592, 88);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Note";
            // 
            // comboGenres
            // 
            this.comboGenres.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboGenres.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboGenres.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboGenres.FormattingEnabled = true;
            this.comboGenres.Location = new System.Drawing.Point(11, 29);
            this.comboGenres.Name = "comboGenres";
            this.comboGenres.Size = new System.Drawing.Size(545, 24);
            this.comboGenres.TabIndex = 1;
            this.comboGenres.KeyUp += new System.Windows.Forms.KeyEventHandler(this.comboGenres_KeyUp);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(200, 14);
            this.progressBar.MarqueeAnimationSpeed = 20;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(191, 23);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(592, 51);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // FormGenreEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 498);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.comboGenres);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCustomGenre);
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
        private System.Windows.Forms.Button btnCustomGenre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboGenres;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}