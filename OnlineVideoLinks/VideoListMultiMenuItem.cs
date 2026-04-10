using OnlineVideoLinks.Database;
using OnlineVideoLinks.Forms;
using OnlineVideoLinks.Models;
using OnlineVideoLinks.Utilities;
using OnlineVideoLinks.WPF;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace OnlineVideoLinks
{
    public class VideoListMultiMenuItem : IGameMultiMenuItemPlugin
    {
        public IEnumerable<IGameMenuItem> GetMenuItems(params IGame[] selectedGames)
        {
            var menuItems = new List<IGameMenuItem>();
            foreach (var game in selectedGames)
            {
                // Load videos from the database
                var videoEntries = GameVideoDb.Instance.GetVideosForGame(game.Id);
                foreach (var entry in videoEntries)
                {
                    var gameVideo = entry.ToGameVideo(game.Id);
                    var menuItem = new VideoMenuItem(gameVideo);
                    menuItems.Add(menuItem);
                }
            }

            var parentMenuItem = new ParentMenuItem
            {
                Caption = "Videos",
                Children = menuItems,
                Enabled = menuItems.Any(),
                Icon = Resources.Video
            };

            return new List<IGameMenuItem> { parentMenuItem };
        }
    }

    public class ParentMenuItem : IGameMenuItem
    {
        public string Caption { get; set; }
        public IEnumerable<IGameMenuItem> Children { get; set; }
        public bool Enabled { get; set; }
        public Image Icon { get; set; }

        public void OnSelect(params IGame[] games)
        {
        }
    }

    public class VideoMenuItem : IGameMenuItem
    {
        private GameVideo _video;
        public string Caption { get; }

        public IEnumerable<IGameMenuItem> Children => null;

        public bool Enabled => true;

        public Image Icon => Resources.Video;

        public VideoMenuItem(GameVideo video)
        {
            Caption = video.Title;
            _video = video;
        }

        public async void OnSelect(params IGame[] games)
        {
            var videoPlayer = new VideoPlayerForm();
            await videoPlayer.Play(_video);
        }
    }
}
