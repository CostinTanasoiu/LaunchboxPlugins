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

using LaunchboxPluginsTests.MockedClasses;
using NSubstitute;
using OnlineVideoLinks;
using OnlineVideoLinks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins.Data;
using Xunit;

namespace LaunchboxPluginsTests.OnlineVideoLinks
{
    public class GameVideoTests
    {
        [Fact]
        public void CanImportAndExportAppDetails()
        {
            var additionalAppDummy = new AdditionalApplicationMock
            {
                ApplicationPath = Utilities.GetVlcExecutablePath(),
                Name = "Video: Presentation",
                CommandLine = "-f --start-time=337 --stop-time=387 https://youtu.be/q_7KUC6CY6Q"
            };

            var gameMock = Substitute.For<IGame>();
            gameMock.AddNewAdditionalApplication().Returns(new AdditionalApplicationMock());

            var video = new GameVideo(additionalAppDummy);

            var exportedApp = video.AddVideoToGame(gameMock);

            Assert.Equal(additionalAppDummy.ApplicationPath, exportedApp.ApplicationPath);
            Assert.Equal(additionalAppDummy.Name, exportedApp.Name);
            Assert.Equal("-f --play-and-exit --start-time=337 --stop-time=387 https://youtu.be/q_7KUC6CY6Q",
                exportedApp.CommandLine);
        }

        [Theory]
        [InlineData("VLC\\x64\\vlc.exe", "-f --play-and-exit http://vidurl.test", true)]
        [InlineData("VLC\\x64\\vlc.exe", "-f --play-and-exit --start-time=337 --stop-time=387 http://vidurl.test", true)]
        [InlineData("C:\\Program Files\\VLC\\x64\\vlc.exe", "-f --play-and-exit http://vidurl.test", false)]
        [InlineData("VLC\\x64\\vlc.exe", "--play-and-exit http://vidurl.test", false)]
        [InlineData("VLC\\x64\\vlc.exe", "-f http://vidurl.test", false)]
        public void CanValidateApps(string appPath, string appCmd, bool expectedResult)
        {
            var app = new AdditionalApplicationMock { ApplicationPath = appPath, CommandLine = appCmd };

            var isValid = GameVideo.IsAppCorrectlySetup(app);

            Assert.Equal(expectedResult, isValid);
        }
    }
}
