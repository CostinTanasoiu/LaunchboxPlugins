using OnlineVideoLinks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineVideoLinks.Services
{
    public class YoutubeScraperService
    {
        private const string YtQueryUrl = "https://www.youtube.com/results?search_query=";
        private const string YtThumbnailUrl = "https://i.ytimg.com/vi/{0}/mqdefault.jpg";
        private const string YtWatchUrl = "http://www.youtube.com/watch?v=";

        /// <summary>
        /// Retrieves a list of YouTube videos for the desired game.
        /// </summary>
        /// <param name="channelName">The name of the YouTube channel.</param>
        /// <param name="gameName">The name of the game.</param>
        /// <param name="strictSearch">If true, the method will return only videos which contain the game name in the title.</param>
        /// <param name="maxNrOfItemsPerGame">The maximum number of results per game.</param>
        /// <returns>List of video information objects.</returns>
        public List<YoutubeVideoInfo> GetVideos(string channelName, string gameName, bool strictSearch, int maxNrOfItemsPerGame)
        {
            var results = new List<YoutubeVideoInfo>();
            var processedGameName = gameName;

            if (processedGameName.EndsWith(", The", StringComparison.InvariantCultureIgnoreCase))
                processedGameName = processedGameName.Substring(0, processedGameName.Length - ", The".Length);

            if (processedGameName.StartsWith("The "))
                processedGameName = processedGameName.Substring("The ".Length);

            var processedGameNameLow = processedGameName.ToLowerInvariant();

            var scrapedResults = ScrapeYoutubeSearchPage(channelName + " " + gameName);

            foreach (var item in scrapedResults)
            {
                // Filtering the given channel name
                if (item.ChannelName.Equals(channelName, StringComparison.InvariantCultureIgnoreCase)
                    || item.Author.Equals(channelName, StringComparison.InvariantCultureIgnoreCase))
                {
                    // If strict search is enabled, we want only the videos which contain the game name in their title
                    if (!strictSearch || (strictSearch && item.Title.ToLowerInvariant().Contains(processedGameNameLow)))
                    {
                        item.GameSearched = gameName;
                        results.Add(item);
                    }
                }
            }

            return results.Take(maxNrOfItemsPerGame).ToList();
        }

        /// <summary>
        /// Given a list of game names, this retrieves a list of YouTube videos for each game.
        /// </summary>
        /// <param name="channelName">The name of the YouTube channel.</param>
        /// <param name="gameNames">The list of game names.</param>
        /// <param name="strictSearch">If true, the method will return only videos which contain the game name in the title.</param>
        /// <param name="maxNrOfItemsPerGame">The maximum number of results per game.</param>
        /// <returns>A dictionary where the key is the game name, and the value is the list of video information objects.</returns>
        public Dictionary<string, List<YoutubeVideoInfo>> GetVideos(string channelName, string[] gameNames, bool strictSearch, int maxNrOfItemsPerGame)
        {
            var result = new Dictionary<string, List<YoutubeVideoInfo>>();
            foreach(var gameName in gameNames.Distinct())
            {
                result[gameName] = GetVideos(channelName, gameName, strictSearch, maxNrOfItemsPerGame);
            }

            return result;
        }

        private List<YoutubeVideoInfo> ScrapeYoutubeSearchPage(string searchQuery)
        {
            var results = new List<YoutubeVideoInfo>();

            var webclient = new WebClient();
            string htmlPage = webclient.DownloadString(YtQueryUrl + searchQuery + "&page=1");

            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(htmlPage);

            var videoNodes = htmlDoc.DocumentNode.SelectNodes(".//div[contains(@class, 'yt-lockup-content')]");

            foreach (var videoNode in videoNodes)
            {
                // Look for elements that might tell us that this is not a video. If so, skip it.
                if (// Channel:
                    videoNode.InnerHtml.Contains("yt-uix-button-subscription-container")
                    || videoNode.InnerHtml.Contains("yt-uix-subscription-button")
                    || videoNode.InnerHtml.Contains("instream")
                    // Playlist:
                    || videoNode.InnerHtml.Contains("/playlist?list=")
                    // Movie:
                    || videoNode.OuterHtml.Contains("yt-lockup-movie")
                    )
                    continue;

                var item = new YoutubeVideoInfo();

                var link = videoNode.SelectSingleNode(".//a[contains(@class, 'yt-uix-tile-link')]");
                // This is not a video
                if (link.Attributes["href"].Value.Contains("list=") || link.Attributes["href"].Value.Contains("start_radio"))
                    continue;

                var itemId = link.Attributes["href"]?.Value.Replace("/watch?v=", "");
                item.Title = System.Web.HttpUtility.HtmlDecode(link.InnerText.Trim());
                item.Url = YtWatchUrl + itemId;
                item.Thumbnail = string.Format(YtThumbnailUrl, itemId);

                var authorLink = videoNode
                    .SelectSingleNode(".//div[contains(@class, 'yt-lockup-byline')]")
                    ?.SelectSingleNode(".//a[contains(@class, 'yt-uix-sessionlink')]");
                item.Author = authorLink.Attributes["href"]?.Value.Replace("/user/", "").Trim();
                item.ChannelName = authorLink.InnerText.Trim();

                item.Duration = videoNode.SelectSingleNode(".//span[contains(@class, 'accessible-description')]")
                    ?.InnerText.Replace("- Duration:", "").Replace(".", "").Trim();

                var extraInfo = videoNode.SelectSingleNode(".//ul[contains(@class, 'yt-lockup-meta-info')]");
                item.PostedWhen = extraInfo?.FirstChild?.InnerText.Trim();
                item.ViewCount = extraInfo?.LastChild?.InnerText.Replace("views", "").Trim();

                item.Description = videoNode.SelectSingleNode(".//div[contains(@class, 'yt-lockup-description')]")?.InnerHtml;

                results.Add(item);
            }

            return results;
        }
    }
}
