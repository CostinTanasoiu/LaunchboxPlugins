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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace OnlineVideoLinks
{
    public class VideoListMultiMenuItem : IGameMultiMenuItemPlugin
    {
        public IEnumerable<IGameMenuItem> GetMenuItems(params IGame[] selectedGames)
        {
            // We only want this for one selected game
            if(selectedGames.Length != 1)
                return Enumerable.Empty<IGameMenuItem>();

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

            if (menuItems.Count == 0)
                return Enumerable.Empty<IGameMenuItem>();

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
            if (PluginHelper.StateManager.IsBigBox && games.Length == 1)
            {
                // ActiveX controls (like Windows Media Player) require an STA thread.
                // BigBox may call this from a non-STA thread, so we create a dedicated STA thread.
                // Don't use Join() - let the thread run independently so the menu can close.
                var game = games[0];
                var staThread = new Thread(() =>
                {
                    var form = new VideoSelectorForm(
                        game,
                        PluginContext.Instance.VideoUtility,
                        () => new VideoPlayerForm(),
                        PluginContext.Instance.GamepadInput);
                    Application.Run(form);
                });
                staThread.SetApartmentState(ApartmentState.STA);
                staThread.Start();
            }
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

        public void OnSelect(params IGame[] games)
        {
            // ActiveX controls (like Windows Media Player) require an STA thread.
            // BigBox may call this from a non-STA thread, so we create a dedicated STA thread.
            // Don't use Join() - let the thread run independently so the menu can close.
            var video = _video;
            var staThread = new Thread(() =>
            {
                var videoPlayer = new VideoPlayerForm();
                videoPlayer.PlayerClosed += (s, e) => Application.ExitThread();
                _ = videoPlayer.Play(video);
                Application.Run();
            });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
        }
    }
}
