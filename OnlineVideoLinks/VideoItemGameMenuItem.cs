using OnlineVideoLinks.Gamepad;
using OnlineVideoLinks.Models;
using OnlineVideoLinks.Utilities;
using OnlineVideoLinks.WPF;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;
using OnlineVideoLinks;
using OnlineVideoLinks.Forms;

namespace OnlineVideoLinks
{
    public class VideoItemGameMenuItem : IGameMenuItemPlugin
    {
        public bool SupportsMultipleGames => false;

        public string Caption => "VIDEOS";

        public Image IconImage => Resources.Video;

        public bool ShowInLaunchBox => true;

        public bool ShowInBigBox => true;

        public bool GetIsValidForGame(IGame selectedGame)
        {
            return GameVideoUtility.DoesGameHaveVideos(selectedGame) && !PluginStartup.StartupFailed;
        }

        public bool GetIsValidForGames(IGame[] selectedGames)
        {
            return !PluginStartup.StartupFailed;
        }

        public void OnSelected(IGame selectedGame)
        {
            var form = new VideoSelectorForm(
                selectedGame,
                PluginContext.Instance.VideoUtility,
                () => new VideoPlayerForm(),
                PluginContext.Instance.GamepadInput);
            form.ShowDialog();
            form.Dispose();

            //var window = new VideoSelectorWindow(
            //    selectedGame,
            //    new GameVideoUtility(),
            //    () => new VideoPlayerWindow(),
            //    new GamepadXinputProvider());
            //window.ShowDialog();
        }

        public void OnSelected(IGame[] selectedGames)
        {
            return;
        }
    }
}
