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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVideoLinks
{
    public class Utilities
    {
        /// <summary>
        /// Retrieves the VLC folder path.
        /// </summary>
        /// <returns></returns>
        public static string GetVlcFolderPath()
        {
            var vlcEnvironment = Environment.Is64BitOperatingSystem ? "x64" : "x86";
            var path = $"ThirdParty\\VLC\\{vlcEnvironment}";

            if (File.Exists(path + "\\vlc.exe"))
                return path;

            // Otherwise try the old Launchbox location - root level.
            path = $"VLC\\{vlcEnvironment}\\vlc.exe";
            if (File.Exists(path + "\\vlc.exe"))
                return path;

            // Otherwise check whether VLC is installed in Program Files and return that.

            if (Environment.Is64BitOperatingSystem)
                return $"C:\\Program Files\\VideoLAN\\VLC";
            else
                return $"C:\\Program Files (x86)\\VideoLAN\\VLC";
        }

        /// <summary>
        /// Retrieves the VLC executable path.
        /// </summary>
        /// <returns></returns>
        public static string GetVlcExecutablePath()
        {
            return GetVlcFolderPath() + "\\vlc.exe";
        }

        /// <summary>
        /// Retrieves the VLC Addons folder path.
        /// </summary>
        /// <returns></returns>
        public static string GetVlcAddonsFolderPath()
        {
            return GetVlcFolderPath() + "\\lua\\playlist";
        }
    }
}
