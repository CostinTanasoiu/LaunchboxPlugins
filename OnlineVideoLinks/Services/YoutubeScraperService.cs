using Google.Apis.Services;
using Google.Apis.YouTube.v3;
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

        private const string ApiKey = "AIzaSyBx_lkLUixCJtexzSbs4KSC6cu4jrsxuQM";
        private const string ApplicationName = "Costin's LaunchBox Plugins - Online Video Links";

        private YouTubeService _youTubeService;

        public YoutubeScraperService()
        {
            _youTubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = ApiKey,
                ApplicationName = ApplicationName
            });
        }

        public string GetChannelId(string channelName)
        {
            try
            {
                var searchListRequest = _youTubeService.Search.List("snippet");
                searchListRequest.Q = channelName;
                searchListRequest.MaxResults = 1;
                searchListRequest.Type = "channel";

                var searchListResponse = searchListRequest.Execute(); // TODO: async
                if (searchListResponse.Items.Any())
                    return searchListResponse.Items[0].Id.ChannelId;
            }
            catch
            {
                // TODO
            }

            return string.Empty;
        }

        public string GetVideoDescription(string videoId)
        {
            var request = _youTubeService.Videos.List("snippet");
            request.Id = videoId;

            var response = request.Execute();
            if (response.Items.Any())
                return response.Items[0].Snippet.Description;

            return string.Empty;
        }

        /// <summary>
        /// Retrieves a list of YouTube videos for the desired game.
        /// </summary>
        /// <param name="channelName">The ID of the YouTube channel.</param>
        /// <param name="extraQuery">Any extra query to add to the search.</param>
        /// <param name="gameName">The name of the game.</param>
        /// <param name="strictSearch">If true, the method will return only videos which contain the game name in the title.</param>
        /// <param name="maxNrOfItemsPerGame">The maximum number of results per game.</param>
        /// <returns>List of video information objects.</returns>
        public List<YoutubeVideoInfo> GetVideos(string channelId, string extraQuery, string gameName, bool strictSearch, int maxNrOfItemsPerGame)
        {
            var results = new List<YoutubeVideoInfo>();
            var processedGameName = gameName;

            if (processedGameName.EndsWith(", The", StringComparison.InvariantCultureIgnoreCase))
                processedGameName = processedGameName.Substring(0, processedGameName.Length - ", The".Length);

            if (processedGameName.StartsWith("The "))
                processedGameName = processedGameName.Substring("The ".Length);

            var processedGameNameLow = processedGameName.ToLowerInvariant();


            var searchListRequest = _youTubeService.Search.List("snippet");
            searchListRequest.Q = processedGameName + " " + extraQuery;
            searchListRequest.MaxResults = maxNrOfItemsPerGame;
            searchListRequest.Type = "video";
            if(!string.IsNullOrEmpty(channelId))
                searchListRequest.ChannelId = channelId;

            try
            {
                var searchListResponse = searchListRequest.Execute(); // TODO: async
                foreach (var item in searchListResponse.Items)
                {
                    // If strict search is enabled, we want only the videos which contain the game name in their title
                    if (!strictSearch || (strictSearch && item.Snippet.Title.ToLowerInvariant().Contains(processedGameNameLow)))
                    {
                        results.Add(new YoutubeVideoInfo
                        {
                            YoutubeVideoId = item.Id.VideoId,
                            GameSearched = gameName,
                            Title = item.Snippet.Title,
                            ChannelName = item.Snippet.ChannelTitle,
                            Author = "N/A",
                            Description = item.Snippet.Description,
                            Duration = "N/A",
                            PostedWhen = item.Snippet.PublishedAt.HasValue ? item.Snippet.PublishedAt.Value.ToString("dd MM yyyy") : "N/A",
                            Thumbnail = item.Snippet.Thumbnails.Default__.Url,
                            Url = YtWatchUrl + item.Id.VideoId,
                            ViewCount = "N/A"
                        });
                    }
                }
            }
            catch(Exception ex)
            {
                // TODO
            }

            return results.Take(maxNrOfItemsPerGame).ToList();
        }

        /// <summary>
        /// Given a list of game names, this retrieves a list of YouTube videos for each game.
        /// </summary>
        /// <param name="channelName">The name of the YouTube channel.</param>
        /// <param name="extraQuery">Any extra query to add to the search.</param>
        /// <param name="gameNames">The list of game names.</param>
        /// <param name="strictSearch">If true, the method will return only videos which contain the game name in the title.</param>
        /// <param name="maxNrOfItemsPerGame">The maximum number of results per game.</param>
        /// <returns>A dictionary where the key is the game name, and the value is the list of video information objects.</returns>
        public Dictionary<string, List<YoutubeVideoInfo>> GetVideos(string channelName, string extraQuery, string[] gameNames, bool strictSearch, int maxNrOfItemsPerGame)
        {
            var result = new Dictionary<string, List<YoutubeVideoInfo>>();

            Thread.Sleep(2000);
            return result;

            var channelId = GetChannelId(channelName);

            foreach (var gameName in gameNames.Distinct())
            {
                result[gameName] = GetVideos(channelId, extraQuery, gameName, strictSearch, maxNrOfItemsPerGame);
            }

            return result;
        }
    }
}
