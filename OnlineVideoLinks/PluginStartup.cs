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

using OnlineVideoLinks.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace YoutubeGameVideos
{
    public class PluginStartup : ISystemEventsPlugin
    {
        // I learned about the YouTube VLC addon here:
        // https://www.latecnosfera.com/2016/10/vlc-unable-to-open-mrl.html

        /// <summary>
        /// This is a link to the youtube.luac file from the development branch of VLC.
        /// </summary>
        private const string YoutubeVlcAddonUrl = "http://git.videolan.org/?p=vlc.git;a=blob_plain;f=share/lua/playlist/youtube.lua;hb=HEAD";

        public void OnEventRaised(string eventType)
        {
            // On startup, we want to check if Launchbox's VLC distribution has the latest YouTube addon.
            if(eventType == SystemEventTypes.LaunchBoxStartupCompleted)
            {
                // Look for Launchbox's VLC distro
                var vlcEnvironment = Environment.Is64BitOperatingSystem ? "x64" : "x86";
                var vlcAddonsFolder = $"VLC\\{vlcEnvironment}\\lua\\playlist\\";

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
                if(youtubeAddonPath == "" 
                    || (DateTime.Now - File.GetLastWriteTime(youtubeAddonPath)).TotalDays > 30)
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(YoutubeVlcAddonUrl, vlcAddonsFolder + "youtube.luac");
                    }
                }

                // All done. Now VLC should be able to play YouTube videos!
            }
            else if(eventType == SystemEventTypes.SelectionChanged)
            {
                // When a game is selected, I want to check that the additional apps 
                // for launching our videos are set up correctly.
                // This ensures that video link apps are always up to date with 
                // the latest VLC path and command-line arguments.
                var selectedGames = PluginHelper.StateManager.GetAllSelectedGames();
                if(selectedGames.Length == 1)
                {
                    var game = selectedGames[0];
                    var videoLinkApps = game.GetAllAdditionalApplications()
                                        .Where(x => x.Name.StartsWith(GameVideo.TitlePrefix))
                                        .ToList();

                    if (videoLinkApps.Count != 0)
                    {
                        foreach(var app in videoLinkApps)
                        {
                            if (!GameVideo.IsAppCorrectlySetup(app))
                            {
                                var gameVideo = new GameVideo(app);
                                gameVideo.UpdateExistingApp(app);
                            }
                        }
                    }
                }
            }
        }
    }
}
