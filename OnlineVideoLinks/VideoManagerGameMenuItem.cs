/*
    Costin's LaunchBox Plugins
    https://github.com/SsjCosty/LaunchboxPlugins
    Copyright (C) 2019  Costin Tănăsoiu
    GNU General Public License v3.0

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using OnlineVideoLinks.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace OnlineVideoLinks
{
    /// <summary>
    /// This controls the game's context menu item for managing game videos.
    /// </summary>
    public class VideoManagerGameMenuItem : IGameMenuItemPlugin
    {
        public bool SupportsMultipleGames => false;

        public string Caption => "Manage Online Video Links...";

        public System.Drawing.Image IconImage => Resources.Video;

        public bool ShowInLaunchBox => true;

        public bool ShowInBigBox => false;

        public bool GetIsValidForGame(IGame selectedGame)
        {
            return true;
        }

        public bool GetIsValidForGames(IGame[] selectedGames)
        {
            return false;
        }

        public void OnSelected(IGame selectedGame)
        {
            var form = new VideoManagerForm(selectedGame, new GameVideoUtility());
            form.ShowDialog();
            form.Dispose();
        }

        public void OnSelected(IGame[] selectedGames)
        {
            return;
        }
    }
}
