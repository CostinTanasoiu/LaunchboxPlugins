using LaunchboxPluginsTests.MockedClasses;
using OnlineVideoLinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LaunchboxPluginsTests.OnlineVideoLinks
{
    public class YoutubeScraperFormTests
    {
        [Fact]
        public void CanLoadOneGame()
        {
            var dummyGames = new GameMock[]
            {
                new GameMock
                {
                    Title = "Death and Return of Superman"
                }
            };

            var form = new YoutubeScraperForm(dummyGames);

            Assert.True(true);
        }
    }
}
