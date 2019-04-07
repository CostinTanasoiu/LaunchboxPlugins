using OnlineVideoLinks.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineVideoLinks
{
    public partial class YoutubeScraperForm : Form
    {
        private YoutubeScraperService youtubeScraperService = new YoutubeScraperService();

        public YoutubeScraperForm()
        {
            InitializeComponent();
        }

        private void UpdateResultsCountLabel(int count)
        {
            lblResults.Text = count > 0 ? $"{count} Results:" : "No Results";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var channel = txtChannel.Text.Trim();
            var query = txtQuery.Text.Trim();

            if(!string.IsNullOrEmpty(channel) && !string.IsNullOrEmpty(query))
            {
                var results = youtubeScraperService.GetVideos(channel, query, checkStrictSearch.Checked);
                UpdateResultsCountLabel(results.Count);
                panelVideoResults.Controls.Clear();
                
                foreach(var item in results)
                {
                    var ctrl = new UserControls.YoutubeSearchResult(item);
                    ctrl.DeleteClicked += YoutubeSearchResultCtrl_DeleteClicked;
                    //ctrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    panelVideoResults.Controls.Add(ctrl);
                }
            }
        }

        private void YoutubeSearchResultCtrl_DeleteClicked(object sender, EventArgs e)
        {
            var ctrl = sender as UserControls.YoutubeSearchResult;
            panelVideoResults.Controls.Remove(ctrl);
            UpdateResultsCountLabel(panelVideoResults.Controls.Count);
        }
    }
}
