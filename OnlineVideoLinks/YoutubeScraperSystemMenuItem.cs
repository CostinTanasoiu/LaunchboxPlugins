using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;

namespace OnlineVideoLinks
{
    public class YoutubeScraperSystemMenuItem : ISystemMenuItemPlugin
    {
        public string Caption => "Find YouTube videos for selected games...";

        public Image IconImage => null;

        public bool ShowInLaunchBox => true;

        public bool ShowInBigBox => false;

        public bool AllowInBigBoxWhenLocked => false;

        public void OnSelected()
        {
            var selectedGames = PluginHelper.StateManager.GetAllSelectedGames();
            if (selectedGames.Length == 0)
                MessageBox.Show("No games were selected.");
            else
            {
                var form = new YoutubeScraperForm(selectedGames);
                form.ShowDialog();
                form.Dispose();
            }
        }
    }
}
