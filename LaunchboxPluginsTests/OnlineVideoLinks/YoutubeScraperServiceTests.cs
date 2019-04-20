using OnlineVideoLinks.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LaunchboxPluginsTests.OnlineVideoLinks
{
    public class YoutubeScraperServiceTests
    {
        [Fact]
        public void CanGetVideos()
        {
            var scraper = new YoutubeScraperService();
            var results = scraper.GetVideos("SNES drunk", "", "Superman", true, 3);

            Assert.NotEmpty(results);
        }
    }
}
