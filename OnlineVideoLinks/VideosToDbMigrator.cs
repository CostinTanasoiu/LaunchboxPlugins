using OnlineVideoLinks.Database;
using OnlineVideoLinks.Forms;
using OnlineVideoLinks.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins;

namespace OnlineVideoLinks
{
    public class VideosToDbMigrator : ISystemMenuItemPlugin
    {
        public string Caption => "Move game videos to DB";

        public Image IconImage => Resources.Video;

        public bool ShowInLaunchBox => true;

        public bool ShowInBigBox => false;

        public bool AllowInBigBoxWhenLocked => false;

        public void OnSelected()
        {
            // Show progress form on a background task
            var progressForm = new ProgressForm("Migrating Videos", "Starting migration...");
            progressForm.Show();

            var games = PluginHelper.DataManager.GetAllGames();
            progressForm.MaxValue = games.Length;
            for (var i = 0; i < games.Length; i++)
            {
                progressForm.UpdateProgress(i, $"Processing item {i+1}/{games.Length}...");

                var game = games[i];
                var gameVideos = new List<GameVideo>();
                var videoAppList = game.GetAllAdditionalApplications()
                                        .Where(x => x.Name.StartsWith(GameVideo.TitlePrefix))
                                        .ToList();

                foreach (var app in videoAppList)
                {
                    var gameVideo = new GameVideo(app);
                    gameVideos.Add(gameVideo);
                }

                var videoEntries = gameVideos.Select(x => GameVideoEntry.FromGameVideo(x)).ToList();
                GameVideoDb.Instance.SetVideosForGame(game.Id, game.Title, game.Platform, videoEntries);

                // Deleting the old video apps
                foreach (var app in videoAppList)
                {
                    game.TryRemoveAdditionalApplication(app);
                }
            }

            progressForm.UpdateProgress(games.Length, "Migration complete!");
            progressForm.Close();
        }
    }
}
