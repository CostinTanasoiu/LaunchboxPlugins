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

namespace LaunchboxPluginsTests.OnlineVideoLinks
{
    public class VideoManagerFormTests
    {
        private Fixture _fixture = new Fixture();

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

            var form = new VideoManagerForm(gameMock);

            Assert.Equal(4, form.GameVideos.Count);
            Assert.Equal("Gameplay teaser", form.GameVideos[0].Title);
            Assert.Equal(122, form.GameVideos[2].StartTime);
            Assert.Equal(387, form.GameVideos[3].StopTime);
        }

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
    }
}
