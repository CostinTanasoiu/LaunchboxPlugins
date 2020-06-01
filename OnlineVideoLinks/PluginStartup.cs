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

using OnlineVideoLinks;
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
        public void OnEventRaised(string eventType)
        {
            // On startup, we want to check if Launchbox's VLC distribution has the latest YouTube addon.
            if (eventType == SystemEventTypes.LaunchBoxStartupCompleted
                || eventType == SystemEventTypes.BigBoxStartupCompleted)
            {
                VlcUtilities.VerifyYoutubeAddon();
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
