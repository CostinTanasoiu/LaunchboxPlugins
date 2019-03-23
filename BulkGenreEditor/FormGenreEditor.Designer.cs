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
            this.txtCustomGenre = new System.Windows.Forms.TextBox();
            this.btnCustomGenre = new System.Windows.Forms.Button();
            this.picLoader = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLoader)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Location = new System.Drawing.Point(12, 9);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(488, 17);
            this.lblInstructions.TabIndex = 0;
            this.lblInstructions.Text = "Choose the genres that you want to add or remove for {0} selected game(s):";
            // 
            // btnRemoveGenres
            // 
            this.btnRemoveGenres.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveGenres.Location = new System.Drawing.Point(415, 363);
            this.btnRemoveGenres.Name = "btnRemoveGenres";
            this.btnRemoveGenres.Size = new System.Drawing.Size(129, 32);
            this.btnRemoveGenres.TabIndex = 2;
            this.btnRemoveGenres.Text = "Remove genres";
            this.btnRemoveGenres.UseVisualStyleBackColor = true;
            this.btnRemoveGenres.Click += new System.EventHandler(this.btnRemoveGenres_Click);
            // 
            // btnAddGenres
            // 
            this.btnAddGenres.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddGenres.Location = new System.Drawing.Point(12, 363);
            this.btnAddGenres.Name = "btnAddGenres";
            this.btnAddGenres.Size = new System.Drawing.Size(129, 32);
            this.btnAddGenres.TabIndex = 3;
            this.btnAddGenres.Text = "Add genres";
            this.btnAddGenres.UseVisualStyleBackColor = true;
            this.btnAddGenres.Click += new System.EventHandler(this.btnAddGenres_Click);
            // 
            // checklistGenres
            // 
            this.checklistGenres.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checklistGenres.CheckOnClick = true;
            this.checklistGenres.FormattingEnabled = true;
            this.checklistGenres.Location = new System.Drawing.Point(12, 39);
            this.checklistGenres.Name = "checklistGenres";
            this.checklistGenres.Size = new System.Drawing.Size(532, 242);
            this.checklistGenres.Sorted = true;
            this.checklistGenres.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 296);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Add a custom genre:";
            // 
            // txtCustomGenre
            // 
            this.txtCustomGenre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomGenre.Location = new System.Drawing.Point(12, 316);
            this.txtCustomGenre.Name = "txtCustomGenre";
            this.txtCustomGenre.Size = new System.Drawing.Size(485, 22);
            this.txtCustomGenre.TabIndex = 7;
            this.txtCustomGenre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomGenre_KeyDown);
            // 
            // btnCustomGenre
            // 
            this.btnCustomGenre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCustomGenre.Location = new System.Drawing.Point(503, 314);
            this.btnCustomGenre.Name = "btnCustomGenre";
            this.btnCustomGenre.Size = new System.Drawing.Size(40, 24);
            this.btnCustomGenre.TabIndex = 8;
            this.btnCustomGenre.Text = "➤";
            this.btnCustomGenre.UseVisualStyleBackColor = true;
            this.btnCustomGenre.Click += new System.EventHandler(this.btnCustomGenre_Click);
            // 
            // picLoader
            // 
            this.picLoader.Image = global::BulkGenreEditor.Properties.Resources.green_loading_bar;
            this.picLoader.Location = new System.Drawing.Point(201, 366);
            this.picLoader.Name = "picLoader";
            this.picLoader.Size = new System.Drawing.Size(160, 24);
            this.picLoader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picLoader.TabIndex = 9;
            this.picLoader.TabStop = false;
            // 
            // FormGenreEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 407);
            this.Controls.Add(this.picLoader);
            this.Controls.Add(this.btnCustomGenre);
            this.Controls.Add(this.txtCustomGenre);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checklistGenres);
            this.Controls.Add(this.btnAddGenres);
            this.Controls.Add(this.btnRemoveGenres);
            this.Controls.Add(this.lblInstructions);
            this.Name = "FormGenreEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bulk Add/Remove Genres";
            this.Load += new System.EventHandler(this.FormGenreEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLoader)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Button btnRemoveGenres;
        private System.Windows.Forms.Button btnAddGenres;
        private System.Windows.Forms.CheckedListBox checklistGenres;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCustomGenre;
        private System.Windows.Forms.Button btnCustomGenre;
        private System.Windows.Forms.PictureBox picLoader;
    }
}