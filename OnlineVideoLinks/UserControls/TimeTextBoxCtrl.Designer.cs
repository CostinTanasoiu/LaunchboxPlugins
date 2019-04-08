namespace OnlineVideoLinks.UserControls
{
    partial class TimeTextBoxCtrl
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
            this.txtTimeBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtTimeBox
            // 
            this.txtTimeBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTimeBox.Location = new System.Drawing.Point(0, 0);
            this.txtTimeBox.Margin = new System.Windows.Forms.Padding(0);
            this.txtTimeBox.Name = "txtTimeBox";
            this.txtTimeBox.Size = new System.Drawing.Size(66, 22);
            this.txtTimeBox.TabIndex = 3;
            this.txtTimeBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimeBox_KeyPress);
            // 
            // TimeTextBoxCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtTimeBox);
            this.Name = "TimeTextBoxCtrl";
            this.Size = new System.Drawing.Size(66, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTimeBox;
    }
}
