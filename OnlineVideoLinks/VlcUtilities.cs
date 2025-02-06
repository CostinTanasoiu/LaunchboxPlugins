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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineVideoLinks
{
    public class VlcUtilities
    {
        // I learned about the YouTube VLC addon here:
        // https://www.latecnosfera.com/2016/10/vlc-unable-to-open-mrl.html

        /// <summary>
        /// This is a link to the youtube.luac file from the development branch of VLC.
        /// </summary>
        private const string YoutubeVlcAddonUrl = "https://raw.githubusercontent.com/videolan/vlc/master/share/lua/playlist/youtube.lua";

        /// <summary>
        /// Retrieves the VLC folder path.
        /// </summary>
        /// <returns></returns>
        public static string GetVlcFolderPath()
        {
            var vlcEnvironment = Environment.Is64BitOperatingSystem ? "x64" : "x86";
            var relativePath = $"ThirdParty\\VLC\\{vlcEnvironment}";
            var fullPath = Path.Combine(Environment.CurrentDirectory, relativePath);

            if (File.Exists(fullPath + "\\vlc.exe"))
                return relativePath;

            // Otherwise try the old Launchbox location - root level.
            relativePath = $"VLC\\{vlcEnvironment}";
            fullPath = Path.Combine(Environment.CurrentDirectory, relativePath);
            if (File.Exists(fullPath + "\\vlc.exe"))
                return relativePath;

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

        /// <summary>
        /// Installs the YouTube VLC addon if not already installed.
        /// </summary>
        public static void VerifyYoutubeAddon()
        {
            // Look for Launchbox's VLC distro
            var vlcEnvironment = Environment.Is64BitOperatingSystem ? "x64" : "x86";
            var vlcAddonsFolder = VlcUtilities.GetVlcAddonsFolderPath();

            var youtubeAddonPath = "";

            // Check if the YouTube VLC addon is installed
            if (Directory.Exists(vlcAddonsFolder))
            {
                var files = Directory.GetFiles(vlcAddonsFolder, "youtube.luac", SearchOption.AllDirectories);
                // I'll assume it's the first one in the array
                if (files.Length != 0)
                    youtubeAddonPath = files[0];
            }
            else Directory.CreateDirectory(vlcAddonsFolder);

            // If we don't have the YouTube addon OR the file is more than a month old, then let's download it.
            // This is because YouTube transmission protocols change over time, and VLC developers are quite
            // keep up by updating their YouTube add-on file.
            if (youtubeAddonPath == ""
                || (DateTime.Now - File.GetLastWriteTime(youtubeAddonPath)).TotalDays > 1)
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(YoutubeVlcAddonUrl, vlcAddonsFolder + "\\youtube.luac");
                }
            }

            // All done. Now VLC should be able to play YouTube videos!
        }
    }
}
