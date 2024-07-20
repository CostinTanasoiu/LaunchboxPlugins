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

using AutoFixture;
using LaunchboxPluginsTests.MockedClasses;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins.Data;
using Xunit;
using OnlineVideoLinks;
using OnlineVideoLinks.Models;
using OnlineVideoLinks.Utilities;

namespace LaunchboxPluginsTests.OnlineVideoLinks
{
    public class VideoManagerFormTests
    {
        private Fixture _fixture = new Fixture();
        IGameVideoUtility _gameVideoUtilitiesMock = Substitute.For<IGameVideoUtility>();

        /// <summary>
        /// This tests that the Video Manager form can load a game with existing videos and properly parse them.
        /// </summary>
        [Fact]
        public void ShouldLoadGameWithExistingVideos()
        {
            var dummyAdditionalApps = new AdditionalApplicationMock[]
            {
                new AdditionalApplicationMock{
                    ApplicationPath ="C:\\Program Files\\VideoLAN\\VLC\\vlc.exe",
                    Name = "Video: Gameplay teaser",
                    CommandLine = "-f https://www.youtube.com/watch?v=KpXkJ8rebDE"
                },
                new AdditionalApplicationMock{
                    ApplicationPath ="C:\\Program Files\\VideoLAN\\VLC\\vlc.exe",
                    Name = "Video: Main trailer",
                    CommandLine = "-f https://www.youtube.com/watch?v=LLlKtI11C-4"
                },
                new AdditionalApplicationMock{
                    ApplicationPath ="C:\\Program Files\\VideoLAN\\VLC\\vlc.exe",
                    Name = "Video: View split-screen demo",
                    CommandLine = "-f --start-time=122 https://youtu.be/_q4e7BwFFUU"
                }
                ,
                new AdditionalApplicationMock{
                    ApplicationPath = "C:\\Program Files\\VideoLAN\\VLC\\vlc.exe",
                    Name = "Video: Video presentation",
                    CommandLine = "-f --start-time=337 --stop-time=387 https://youtu.be/q_7KUC6CY6Q"
                }
            };

            var gameMock = Substitute.For<IGame>();
            gameMock.GetAllAdditionalApplications().Returns(dummyAdditionalApps);

            var form = new VideoManagerForm(gameMock, _gameVideoUtilitiesMock);

            Assert.Equal(4, form.GameVideos.Count);
            Assert.Equal("Gameplay teaser", form.GameVideos[0].Title);
            Assert.Equal(122, form.GameVideos[2].StartTime);
            Assert.Equal(387, form.GameVideos[3].StopTime);
        }
    }
}
