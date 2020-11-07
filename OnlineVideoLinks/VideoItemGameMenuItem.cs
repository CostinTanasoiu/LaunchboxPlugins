using OnlineVideoLinks.Models;
using OnlineVideoLinks.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace OnlineVideoLinks
{
    public class VideoItemGameMenuItem : IGameMenuItemPlugin
    {
        public bool SupportsMultipleGames => false;

        public string Caption => "VIDEOS";

        public Image IconImage => Resources.Video;

        public bool ShowInLaunchBox => false;

        public bool ShowInBigBox => true;

        public bool GetIsValidForGame(IGame selectedGame)
        {
            return GameVideoUtilities.DoesGameHaveVideos(selectedGame);
        }

        public bool GetIsValidForGames(IGame[] selectedGames)
        {
            return false;
        }

        public void OnSelected(IGame selectedGame)
        {
            var form = new VideoSelectorForm(selectedGame);
            form.ShowDialog();
        }

        public void OnSelected(IGame[] selectedGames)
        {
            return;
        }
    }
}
