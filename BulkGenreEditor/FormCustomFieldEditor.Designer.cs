namespace BulkGenreEditor
{
    partial class FormCustomFieldEditor
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
            this.picLoader = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridCustomFields = new System.Windows.Forms.DataGridView();
            this.FieldSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FieldValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bgWorkerSave = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.picLoader)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomFields)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInstructions
            // 
            this.lblInstructions.Location = new System.Drawing.Point(12, 9);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(532, 38);
            this.lblInstructions.TabIndex = 0;
            this.lblInstructions.Text = "Choose the custom fields that you want to add/update or remove for {0} selected g" +
    "ame(s). You can also add new ones.\r\n";
            // 
            // btnRemoveGenres
            // 
            this.btnRemoveGenres.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveGenres.Location = new System.Drawing.Point(367, 438);
            this.btnRemoveGenres.Name = "btnRemoveGenres";
            this.btnRemoveGenres.Size = new System.Drawing.Size(177, 32);
            this.btnRemoveGenres.TabIndex = 2;
            this.btnRemoveGenres.Text = "Remove field values";
            this.btnRemoveGenres.UseVisualStyleBackColor = true;
            this.btnRemoveGenres.Click += new System.EventHandler(this.btnRemoveGenres_Click);
            // 
            // btnAddGenres
            // 
            this.btnAddGenres.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddGenres.Location = new System.Drawing.Point(12, 438);
            this.btnAddGenres.Name = "btnAddGenres";
            this.btnAddGenres.Size = new System.Drawing.Size(183, 32);
            this.btnAddGenres.TabIndex = 3;
            this.btnAddGenres.Text = "Add/Update field values";
            this.btnAddGenres.UseVisualStyleBackColor = true;
            this.btnAddGenres.Click += new System.EventHandler(this.btnAddGenres_Click);
            // 
            // picLoader
            // 
            this.picLoader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.picLoader.Image = global::BulkGenreEditor.Properties.Resources.green_loading_bar;
            this.picLoader.Location = new System.Drawing.Point(201, 438);
            this.picLoader.Name = "picLoader";
            this.picLoader.Size = new System.Drawing.Size(160, 32);
            this.picLoader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLoader.TabIndex = 9;
            this.picLoader.TabStop = false;
            this.picLoader.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(519, 44);
            this.label1.TabIndex = 0;
            this.label1.Text = "You can add new custom fields at the end of the list. Fields with empty values wi" +
    "ll be ignored on Add/Update.";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(11, 358);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(532, 74);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Note";
            // 
            // gridCustomFields
            // 
            this.gridCustomFields.AllowUserToDeleteRows = false;
            this.gridCustomFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCustomFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FieldSelected,
            this.FieldName,
            this.FieldValue});
            this.gridCustomFields.Location = new System.Drawing.Point(15, 50);
            this.gridCustomFields.Name = "gridCustomFields";
            this.gridCustomFields.RowTemplate.Height = 24;
            this.gridCustomFields.Size = new System.Drawing.Size(528, 289);
            this.gridCustomFields.TabIndex = 13;
            this.gridCustomFields.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gridCustomFields_CellBeginEdit);
            this.gridCustomFields.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.gridCustomFields_CellValidating);
            // 
            // FieldSelected
            // 
            this.FieldSelected.DataPropertyName = "IsSelected";
            this.FieldSelected.HeaderText = "";
            this.FieldSelected.Name = "FieldSelected";
            this.FieldSelected.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FieldSelected.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.FieldSelected.Width = 50;
            // 
            // FieldName
            // 
            this.FieldName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FieldName.DataPropertyName = "FieldName";
            this.FieldName.HeaderText = "Name";
            this.FieldName.Name = "FieldName";
            // 
            // FieldValue
            // 
            this.FieldValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FieldValue.DataPropertyName = "FieldValue";
            this.FieldValue.HeaderText = "Value (only available for adding)";
            this.FieldValue.Name = "FieldValue";
            // 
            // bgWorkerSave
            // 
            this.bgWorkerSave.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerSave_DoWork);
            this.bgWorkerSave.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorkerSave_ProgressChanged);
            this.bgWorkerSave.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerSave_RunWorkerCompleted);
            // 
            // FormCustomFieldEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 482);
            this.Controls.Add(this.gridCustomFields);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.picLoader);
            this.Controls.Add(this.btnAddGenres);
            this.Controls.Add(this.btnRemoveGenres);
            this.Controls.Add(this.lblInstructions);
            this.MinimumSize = new System.Drawing.Size(574, 529);
            this.Name = "FormCustomFieldEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bulk Add/Remove Custom Fields";
            this.Load += new System.EventHandler(this.FormCustomFieldEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLoader)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomFields)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Button btnRemoveGenres;
        private System.Windows.Forms.Button btnAddGenres;
        private System.Windows.Forms.PictureBox picLoader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView gridCustomFields;
        private System.Windows.Forms.DataGridViewCheckBoxColumn FieldSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn FieldName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FieldValue;
        private System.ComponentModel.BackgroundWorker bgWorkerSave;
    }
}