using OnlineVideoLinks.Models;
using OnlineVideoLinks.Services;
using OnlineVideoLinks.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace OnlineVideoLinks
{
    public partial class YoutubeScraperForm : Form
    {
        private YoutubeScraperService youtubeScraperService = new YoutubeScraperService();
        private IGame[] _selectedGames;

        public YoutubeScraperForm()
        {
            InitializeComponent();
        }

        public YoutubeScraperForm(IGame[] selectedGames)
        {
            InitializeComponent();

            _selectedGames = selectedGames;
            this.Text = "Find YouTube videos for: " + selectedGames.Length + " game(s)";
        }

        private void UpdateResultsCountLabel(int count)
        {
            lblResults.Text = count > 0 ? $"{count} Results:" : "No Results";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var channel = txtChannel.Text.Trim();

            panelVideoResults.Controls.Clear();

            var gameNames = _selectedGames
                .Where(x => !checkSkipGamesWithVideos.Checked || !GameVideo.HasVideoLinks(x))
                .Select(x => x.Title).ToArray();
            
            if (!string.IsNullOrEmpty(channel))
            {
                var results = youtubeScraperService.GetVideos(channel, gameNames, checkStrictSearch.Checked, (int)numericMaxItemsPerGame.Value);
                //UpdateResultsCountLabel(results.Count);

                var drawingFirstElement = true;

                foreach (var game in _selectedGames.OrderBy(x=>x.SortTitleOrTitle))
                {
                    var lblGameTitle = new Label {
                        Text = game.Title,
                        Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold),
                        AutoSize = true,
                        Margin = new Padding(3, drawingFirstElement ? 3 : 20, 3, 3)
                    };
                    panelVideoResults.Controls.Add(lblGameTitle);
                    drawingFirstElement = false;

                    if (results.ContainsKey(game.Title) && results[game.Title].Count > 0)
                    {
                        foreach (var item in results[game.Title])
                        {
                            var ctrl = new UserControls.YoutubeSearchResultCtrl(item);
                            ctrl.DeleteClicked += YoutubeSearchResultCtrl_DeleteClicked;
                            ctrl.Margin = new Padding(20, 3, 3, 3);
                            //ctrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                            panelVideoResults.Controls.Add(ctrl);
                        }
                    }
                    else
                    {
                        var reason = "- No Results";
                        
                        if (!results.ContainsKey(game.Title))
                            reason = "- Skipped";

                        panelVideoResults.Controls.Add(new Label
                        {
                            Text = reason,
                            AutoSize = true,
                            Margin = new Padding(20, 3, 3, 3)
                        });
                    }
                }
            }
        }

        private void YoutubeSearchResultCtrl_DeleteClicked(object sender, EventArgs e)
        {
            var ctrl = sender as UserControls.YoutubeSearchResultCtrl;
            panelVideoResults.Controls.Remove(ctrl);
            UpdateResultsCountLabel(panelVideoResults.Controls.Count);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            // Retrieving all the selected video titles
            var selectedVideoInfos = new List<YoutubeVideoInfo>();
            foreach(var ctrl in panelVideoResults.Controls)
            {
                if(ctrl is YoutubeSearchResultCtrl)
                {
                    var youtubeCtrl = ctrl as YoutubeSearchResultCtrl;
                    if (youtubeCtrl.IsSelected)
                        selectedVideoInfos.Add(youtubeCtrl.VideoInfo);
                }
            }

            // Dictionary of GameVideo lists, where the game title is the key.
            var gameVideoDictionary = selectedVideoInfos
                .GroupBy(x => x.GameSearched)
                .ToDictionary(
                    g => g.Key, 
                    g => g.Select(x=>new GameVideo { Title = x.Title, VideoPath = x.Url }).ToList()
                    );

            foreach(var game in _selectedGames)
            {
                if (gameVideoDictionary.ContainsKey(game.Title))
                {
                    foreach(var gameVideoToAdd in gameVideoDictionary[game.Title])
                    {
                        // first, check that this game doesn't already have this video
                        var existingVideos = GameVideo.ExtractVideoLinksFromGame(game);
                        if(!existingVideos.Any(x=>x.VideoPath.Equals(gameVideoToAdd.VideoPath)))
                            gameVideoToAdd.AddVideoToGame(game);
                    }
                }
            }

            // Save
            PluginHelper.DataManager.Save(true);
            this.Close();
        }
    }
}
