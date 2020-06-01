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
            this.picLoader = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboGenres = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLoader)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.btnRemoveGenres.Location = new System.Drawing.Point(415, 438);
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
            this.btnAddGenres.Location = new System.Drawing.Point(12, 438);
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
            this.label2.Size = new System.Drawing.Size(136, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Create a new genre:";
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
            this.picLoader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.picLoader.Image = global::BulkGenreEditor.Properties.Resources.green_loading_bar;
            this.picLoader.Location = new System.Drawing.Point(201, 441);
            this.picLoader.Name = "picLoader";
            this.picLoader.Size = new System.Drawing.Size(160, 24);
            this.picLoader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picLoader.TabIndex = 9;
            this.picLoader.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(519, 54);
            this.label1.TabIndex = 0;
            this.label1.Text = "Adding genres will not overwrite your selected games\' existing genres, but will o" +
    "nly append the new ones to each selected game. However, be careful with the remo" +
    "ve operation.";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(11, 344);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(532, 88);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Note";
            // 
            // comboGenres
            // 
            this.comboGenres.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboGenres.FormattingEnabled = true;
            this.comboGenres.Location = new System.Drawing.Point(12, 314);
            this.comboGenres.Name = "comboGenres";
            this.comboGenres.Size = new System.Drawing.Size(485, 24);
            this.comboGenres.TabIndex = 11;
            // 
            // FormGenreEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 482);
            this.Controls.Add(this.comboGenres);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.picLoader);
            this.Controls.Add(this.btnCustomGenre);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checklistGenres);
            this.Controls.Add(this.btnAddGenres);
            this.Controls.Add(this.btnRemoveGenres);
            this.Controls.Add(this.lblInstructions);
            this.MinimumSize = new System.Drawing.Size(574, 529);
            this.Name = "FormGenreEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bulk Add/Remove Genres";
            this.Load += new System.EventHandler(this.FormGenreEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLoader)).EndInit();
            this.groupBox1.ResumeLayout(false);
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
        private System.Windows.Forms.PictureBox picLoader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboGenres;
    }
}