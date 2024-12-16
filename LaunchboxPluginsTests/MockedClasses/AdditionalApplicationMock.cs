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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins.Data;

namespace LaunchboxPluginsTests.MockedClasses
{
    public class AdditionalApplicationMock : IAdditionalApplication
    {
        public string Id { get; set; }

        public string GameId { get; set; }

        public int PlayCount { get; set; }
        public string ApplicationPath { get; set; }
        public bool AutoRunAfter { get; set; }
        public bool AutoRunBefore { get; set; }
        public string CommandLine { get; set; }
        public string Name { get; set; }
        public bool UseDosBox { get; set; }
        public bool UseEmulator { get; set; }
        public bool WaitForExit { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string Region { get; set; }
        public string Version { get; set; }
        public string Status { get; set; }
        public DateTime? LastPlayed { get; set; }
        public int? Disc { get; set; }
        public string EmulatorId { get; set; }
        public bool SideA { get; set; }
        public bool SideB { get; set; }
        public int Priority { get; set; }
        public bool? Installed { get; set; }
        public int PlayTime { get; set; }

        public System.Drawing.Image GetIconImage(IGame game)
        {
            throw new NotImplementedException();
        }

        public string Launch(IGame game)
        {
            throw new NotImplementedException();
        }
    }
}
