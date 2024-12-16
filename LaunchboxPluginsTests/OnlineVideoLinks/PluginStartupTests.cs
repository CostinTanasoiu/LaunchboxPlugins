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
using log4net.Appender;
using OnlineVideoLinks.Utilities;
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
        public void Should_Validate_Log4Net_LogFileLocation()
        {
            var expectedFile =
                Path.Combine(GeneralUtilities.PluginDirectory, "Logs\\OnlineVideoLinks.Plugin.log");

            var pluginStartup = new PluginStartup();

            var fileAppender = LogManager.GetRepository()
                                .GetAppenders()
                                .FirstOrDefault(appender => appender is RollingFileAppender) 
                                as RollingFileAppender;

            Assert.NotNull(fileAppender);
            Assert.Equal(expectedFile, fileAppender.File);
        }
    }
}
