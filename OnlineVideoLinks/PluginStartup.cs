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

using log4net;
using OnlineVideoLinks.Utilities;
using System;
using System.IO;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace YoutubeGameVideos
{
    public class PluginStartup : ISystemEventsPlugin
    {
        ILog _log;

        public PluginStartup()
        {
            var logFilePath = Path.Combine(Environment.CurrentDirectory, @"Plugins\OnlineVideoLinks\Log4Net.config");
            var file = new FileInfo(logFilePath);
            log4net.Config.XmlConfigurator.Configure(file);

            _log = LogManager.GetLogger(nameof(PluginStartup));
        }

        public void OnEventRaised(string eventType)
        {
            // On startup, we want to check if Launchbox's VLC distribution has the latest YouTube addon.
            if (eventType == SystemEventTypes.LaunchBoxStartupCompleted
                || eventType == SystemEventTypes.BigBoxStartupCompleted)
            {
                _log.Info("Event raised: " + eventType.ToString());

                VlcUtilities.VerifyYoutubeAddon();
                _log.Info("Verified the Youtube addon for VLC.");

                var gameVideoUtility = new GameVideoUtilities();
                gameVideoUtility.ValidateVideosForAllGames();
            }
        }
    }
}
