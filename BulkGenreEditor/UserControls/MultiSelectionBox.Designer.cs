namespace BulkGenreEditor.UserControls
{
    partial class MultiSelectionBox
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
            this.txtSearchBox = new System.Windows.Forms.TextBox();
            this.listBoxAutocomplete = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // txtSearchBox
            // 
            this.txtSearchBox.Location = new System.Drawing.Point(3, 3);
            this.txtSearchBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.txtSearchBox.Name = "txtSearchBox";
            this.txtSearchBox.Size = new System.Drawing.Size(614, 22);
            this.txtSearchBox.TabIndex = 0;
            this.txtSearchBox.TextChanged += new System.EventHandler(this.txtSearchBox_TextChanged);
            this.txtSearchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchBox_KeyDown);
            this.txtSearchBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearchBox_KeyUp);
            // 
            // listBoxAutocomplete
            // 
            this.listBoxAutocomplete.BackColor = System.Drawing.Color.Snow;
            this.listBoxAutocomplete.FormattingEnabled = true;
            this.listBoxAutocomplete.ItemHeight = 16;
            this.listBoxAutocomplete.Location = new System.Drawing.Point(3, 26);
            this.listBoxAutocomplete.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.listBoxAutocomplete.Name = "listBoxAutocomplete";
            this.listBoxAutocomplete.Size = new System.Drawing.Size(614, 164);
            this.listBoxAutocomplete.TabIndex = 1;
            this.listBoxAutocomplete.VisibleChanged += new System.EventHandler(this.listBoxAutocomplete_VisibleChanged);
            // 
            // MultiSelectionBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBoxAutocomplete);
            this.Controls.Add(this.txtSearchBox);
            this.Name = "MultiSelectionBox";
            this.Size = new System.Drawing.Size(620, 193);
            this.Load += new System.EventHandler(this.MultiSelectionBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearchBox;
        private System.Windows.Forms.ListBox listBoxAutocomplete;
    }
}
