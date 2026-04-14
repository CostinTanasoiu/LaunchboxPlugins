using OnlineVideoLinks.Gamepad;
using OnlineVideoLinks.Models;
using OnlineVideoLinks.Utilities;
using OnlineVideoLinks.WPF;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
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
            // ActiveX controls (like Windows Media Player) require an STA thread.
            // BigBox may call this from a non-STA thread, so we create a dedicated STA thread.
            var staThread = new Thread(() =>
            {
                var form = new VideoSelectorForm(
                    selectedGame,
                    PluginContext.Instance.VideoUtility,
                    () => new VideoPlayerForm(),
                    PluginContext.Instance.GamepadInput);
                Application.Run(form);
            });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join(); // Wait for the form to close before returning
        }

        public void OnSelected(IGame[] selectedGames)
        {
            return;
        }
    }
}
