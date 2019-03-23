using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace BulkGenreEditor
{
    public class Plugin : IGameMenuItemPlugin
    {
        public bool SupportsMultipleGames => true;

        public string Caption => "Bulk Add/Remove Genres...";

        public Image IconImage => null;

        public bool ShowInLaunchBox => true;

        public bool ShowInBigBox => false;

        public bool GetIsValidForGame(IGame selectedGame)
        {
            return true;
        }

        public bool GetIsValidForGames(IGame[] selectedGames)
        {
            return true;
        }

        public void OnSelected(IGame selectedGame)
        {
            HandleSelectedGames(new IGame[] { selectedGame });
        }

        public void OnSelected(IGame[] selectedGames)
        {
            HandleSelectedGames(selectedGames);
        }

        private void HandleSelectedGames(IGame[] selectedGames)
        {
            var form = new FormGenreEditor(selectedGames);
            form.ShowDialog();
            form.Dispose();
        }
    }
}
