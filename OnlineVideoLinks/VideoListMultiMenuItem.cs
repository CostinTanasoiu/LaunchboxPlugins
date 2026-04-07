using OnlineVideoLinks.Database;
using OnlineVideoLinks.Models;
using OnlineVideoLinks.Utilities;
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
            var gameVideoUtility = new GameVideoUtility();
            var menuItems = new List<IGameMenuItem>();
            foreach (var game in selectedGames)
            {
                // Load videos from the database
                var videoEntries = GameVideoDb.Instance.GetVideosForGame(game.Id);
                foreach (var entry in videoEntries)
                {
                    var gameVideo = entry.ToGameVideo(game.Id);
                    var menuItem = new VideoMenuItem(gameVideoUtility, gameVideo);
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
        private IGameVideoUtility _gameVideoUtility;
        private GameVideo _video;
        public string Caption { get; }

        public IEnumerable<IGameMenuItem> Children => null;

        public bool Enabled => true;

        public Image Icon => Resources.Video;

        public VideoMenuItem(IGameVideoUtility gameVideoUtility, GameVideo video)
        {
            Caption = video.Title;
            _gameVideoUtility = gameVideoUtility;
            _video = video;
        }

        public void OnSelect(params IGame[] games)
        {
            _gameVideoUtility.Play(_video);
        }
    }
}
