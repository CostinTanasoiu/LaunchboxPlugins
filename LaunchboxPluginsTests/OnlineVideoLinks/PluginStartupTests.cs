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
using Unbroken.LaunchBox.Plugins.Data;
using Xunit;
using YoutubeGameVideos;

namespace LaunchboxPluginsTests.OnlineVideoLinks
{
    public class PluginStartupTests
    {
        [Fact]
        public void Can_Download_VLC_Addon_At_Startup()
        {
            var vlcEnvironment = Environment.Is64BitOperatingSystem ? "x64" : "x86";

            // Create the VLC root folder and dummy executable in our test project working folder
            var path = Path.Combine(Environment.CurrentDirectory, $"ThirdParty\\VLC\\{vlcEnvironment}");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (!File.Exists(Path.Combine(path, "vlc.exe")))
                File.Create(Path.Combine(path, "vlc.exe"));

            // Check that VLC plugin doesn't already exist in the working folder
            var addonFilePath = $"ThirdParty\\VLC\\{vlcEnvironment}\\lua\\playlist\\youtube.luac";
            if(File.Exists(addonFilePath))
                File.Delete(addonFilePath);

            var pluginStartup = new PluginStartup();
            pluginStartup.OnEventRaised(SystemEventTypes.LaunchBoxStartupCompleted);

            Assert.True(File.Exists(addonFilePath));
        }
    }
}
