using OnlineVideoLinks.Models;
using OnlineVideoLinks.Utilities;
using OnlineVideoLinks.WPF;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;
using YoutubeGameVideos;

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
            //var form = new VideoSelectorForm(selectedGame, 
            //    new GameVideoUtility(), new GamepadXinputProvider());
            //form.ShowDialog();

            var window = new VideoSelectorWindow(selectedGame,
                new GameVideoUtility(),  new GamepadXinputProvider());
            window.ShowDialog();
        }

        public void OnSelected(IGame[] selectedGames)
        {
            return;
        }
    }
}
