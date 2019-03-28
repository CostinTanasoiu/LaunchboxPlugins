﻿using LaunchboxPluginsTests.MockedClasses;
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

namespace LaunchboxPluginsTests.ExtraGameVideos
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
            Assert.Equal("-f --play-and-exit --qt-start-minimized --start-time=337 --stop-time=387 https://youtu.be/q_7KUC6CY6Q",
                exportedApp.CommandLine);
        }

        [Theory]
        [InlineData("VLC\\x64\\vlc.exe", "-f --play-and-exit --qt-start-minimized http://vidurl.test", true)]
        [InlineData("VLC\\x64\\vlc.exe", "-f --play-and-exit --qt-start-minimized --start-time=337 --stop-time=387 http://vidurl.test", true)]
        [InlineData("C:\\Program Files\\VLC\\x64\\vlc.exe", "-f --play-and-exit --qt-start-minimized http://vidurl.test", false)]
        [InlineData("VLC\\x64\\vlc.exe", "--play-and-exit --qt-start-minimized http://vidurl.test", false)]
        [InlineData("VLC\\x64\\vlc.exe", "-f --qt-start-minimized http://vidurl.test", false)]
        [InlineData("VLC\\x64\\vlc.exe", "-f --play-and-exit http://vidurl.test", false)]
        public void CanValidateApps(string appPath, string appCmd, bool expectedResult)
        {
            var app = new AdditionalApplicationMock { ApplicationPath = appPath, CommandLine = appCmd };

            var isValid = GameVideo.IsAppCorrectlySetup(app);

            Assert.Equal(expectedResult, isValid);
        }
    }
}