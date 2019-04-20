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
using System.Threading;
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
            progressBar.Value = 0;
            var channel = txtChannel.Text.Trim();
            var extraQuery = txtExtraQuery.Text.Trim();

            panelVideoResults.Controls.Clear();

            var gameNames = _selectedGames
                .Where(x => !checkSkipGamesWithVideos.Checked || !GameVideo.HasVideoLinks(x))
                .Select(x => x.Title)
                .Distinct()
                .ToArray();

            progressBar.Maximum = gameNames.Length;
            progressBar.Value = 0;
            btnSearch.Enabled = false;

            bgWorkerSearch.RunWorkerAsync(new SearchModel
            {
                ChannelName = channel,
                ExtraQuery = extraQuery,
                GameNames = gameNames,
                StrictSearch = checkStrictSearch.Checked,
                MaxNrOfItemsPerGame = (int)numericMaxItemsPerGame.Value
            });
            //var results = GetVideos(channel, extraQuery, gameNames, checkStrictSearch.Checked, (int)numericMaxItemsPerGame.Value);
            
        }

        /// <summary>
        /// Displays search results in the form.
        /// </summary>
        /// <param name="searchResults">A dictionary where the key is the game name, and the value is the list of video information objects.</param>
        private void DisplaySearchResults(Dictionary<string, List<YoutubeVideoInfo>> searchResults)
        {
            var drawingFirstElement = true;

            foreach (var game in _selectedGames.OrderBy(x => x.SortTitleOrTitle))
            {
                // If the game wasn't skipped
                if (searchResults.ContainsKey(game.Title))
                {
                    var lblGameTitle = new Label
                    {
                        Text = game.Title,
                        Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold),
                        AutoSize = true,
                        Margin = new Padding(3, drawingFirstElement ? 3 : 20, 3, 3)
                    };
                    panelVideoResults.Controls.Add(lblGameTitle);
                    drawingFirstElement = false;

                    if (searchResults[game.Title].Count > 0)
                    {
                        foreach (var item in searchResults[game.Title])
                        {
                            var ctrl = new UserControls.YoutubeSearchResultCtrl(item);
                            ctrl.DeleteClicked += YoutubeSearchResultCtrl_DeleteClicked;
                            ctrl.Margin = new Padding(20, 3, 3, 3);
                            panelVideoResults.Controls.Add(ctrl);
                        }
                    }
                    else
                    {
                        // This means the game title wasn't skipped but we found no videos for it
                        panelVideoResults.Controls.Add(new Label
                        {
                            Text = "- No Results",
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
            var selectedVideoInfos = new List<GameVideo>();
            foreach(var ctrl in panelVideoResults.Controls)
            {
                if(ctrl is YoutubeSearchResultCtrl)
                {
                    var youtubeCtrl = ctrl as YoutubeSearchResultCtrl;
                    if (youtubeCtrl.IsSelected)
                        selectedVideoInfos.Add(youtubeCtrl.ExtractGameVideo());
                }
            }

            // Dictionary of GameVideo lists, where the game title is the key.
            var gameVideoDictionary = selectedVideoInfos
                .GroupBy(x => x.GameTitle)
                .ToDictionary(
                    g => g.Key, 
                    g => g.ToList()
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

        private void btnSelectAllResults_Click(object sender, EventArgs e)
        {
            foreach (var ctrl in panelVideoResults.Controls)
            {
                if (ctrl is YoutubeSearchResultCtrl)
                {
                    var youtubeCtrl = ctrl as YoutubeSearchResultCtrl;
                    youtubeCtrl.IsSelected = true;
                }
            }
        }

        private void bgWorkerSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            var searchModel = e.Argument as SearchModel;
            var result = new Dictionary<string, List<YoutubeVideoInfo>>();
            var channelId = youtubeScraperService.GetChannelId(searchModel.ChannelName);

            for (var i = 0; i < searchModel.GameNames.Length; i++)
            {
                var gameName = searchModel.GameNames[i];
                result[gameName] = youtubeScraperService.GetVideos(channelId, searchModel.ExtraQuery, gameName, 
                    searchModel.StrictSearch, searchModel.MaxNrOfItemsPerGame);
                bgWorkerSearch.ReportProgress(i + 1);

            }

            e.Result = result;
        }

        private void bgWorkerSearch_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void bgWorkerSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DisplaySearchResults(e.Result as Dictionary<string, List<YoutubeVideoInfo>>);
            btnSearch.Enabled = true;
        }
    }
}
