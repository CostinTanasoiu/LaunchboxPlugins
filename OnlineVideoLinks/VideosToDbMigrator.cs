using OnlineVideoLinks.Database;
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
            var games = PluginHelper.DataManager.GetAllGames();
            foreach (var game in games)
            {
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
                GameVideoDb.Instance.SetVideosForGame(game.Id, videoEntries);
            }
        }
    }
}
