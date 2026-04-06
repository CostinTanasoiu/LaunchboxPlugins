using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins.Data;
using Unbroken.LaunchBox.Plugins;
using VideoPlayer.Utilities;
using VideoPlayer.Forms;
using VideoPlayer.Gamepad;

namespace VideoPlayer
{
    public class VideoSelectorGameMenuItem : IGameMenuItemPlugin
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
                new GameVideoUtility(),
                new VideoPlayerForm(),
                new GamepadXinputProvider()
                );
            form.ShowDialog();
        }

        public void OnSelected(IGame[] selectedGames)
        {
            return;
        }
    }
}
