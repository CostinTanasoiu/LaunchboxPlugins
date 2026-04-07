using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineVideoLinks.Forms
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
        }

        public ProgressForm(string title, string initialStatus = "Please wait...") : this()
        {
            Text = title;
            lblStatus.Text = initialStatus;
        }

        /// <summary>
        /// Gets or sets the maximum progress value.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int MaxValue
        {
            get => progressBar.Maximum;
            set
            {
                if (InvokeRequired)
                {
                    Invoke(() => progressBar.Maximum = value);
                }
                else
                {
                    progressBar.Maximum = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the current progress value (0 to MaxValue).
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int Progress
        {
            get => progressBar.Value;
            set
            {
                if (InvokeRequired)
                {
                    Invoke(() => progressBar.Value = Math.Clamp(value, 0, progressBar.Maximum));
                }
                else
                {
                    progressBar.Value = Math.Clamp(value, 0, progressBar.Maximum);
                }
            }
        }

        /// <summary>
        /// Gets or sets the status text displayed above the progress bar.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new string Status
        {
            get => lblStatus.Text;
            set
            {
                if (InvokeRequired)
                {
                    Invoke(() => lblStatus.Text = value);
                }
                else
                {
                    lblStatus.Text = value;
                }
            }
        }

        /// <summary>
        /// Updates both the progress value and status text.
        /// </summary>
        /// <param name="progress">Progress value (0 to MaxValue).</param>
        /// <param name="status">Status text to display.</param>
        public void UpdateProgress(int progress, string status)
        {
            Progress = progress;
            Status = status;
        }

        /// <summary>
        /// Sets the progress bar to indeterminate mode (marquee style).
        /// </summary>
        public void SetIndeterminate(bool indeterminate)
        {
            if (InvokeRequired)
            {
                Invoke(() => progressBar.Style = indeterminate ? ProgressBarStyle.Marquee : ProgressBarStyle.Blocks);
            }
            else
            {
                progressBar.Style = indeterminate ? ProgressBarStyle.Marquee : ProgressBarStyle.Blocks;
            }
        }
    }
}
