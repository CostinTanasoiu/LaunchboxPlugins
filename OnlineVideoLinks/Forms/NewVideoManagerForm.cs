using OnlineVideoLinks.Utilities;
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
    public partial class NewVideoManagerForm : Form
    {
        VideoMetadataUtilities _videoTitleScraper = new VideoMetadataUtilities();

        public NewVideoManagerForm()
        {
            InitializeComponent();
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            panelNewVideo.Visible = true;
            panelNewVideo.Location = new Point(panelNewVideo.Location.X, btnPlus.Location.Y);
            btnPlus.Visible = false;
        }

        private async void btnOkVideoUrl_Click(object sender, EventArgs e)
        {
            var metadata = await _videoTitleScraper.GetVideoMetadata(txtVideoPath.Text);
            if (metadata.Length > 0)
                MessageBox.Show(string.Join("\n", metadata.Select(x => x.Title)));
            else
                MessageBox.Show("Not found");
        }
    }
}
