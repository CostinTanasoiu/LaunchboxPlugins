using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineVideoLinks.Models;
using SteamWebAPI2.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using YoutubeExplode;

namespace OnlineVideoLinks.Utilities
{
    public class VideoMetadataUtilities
    {
        ILog _log = LogManager.GetLogger(nameof(VideoMetadataUtilities));
        private static string[] YouTubeHosts = { "www.youtube.com", "youtube.com", "youtu.be", "www.youtu.be" };
        private const string YoutubeWatchUrl = "https://www.youtube.com/watch?v=";

        public async Task<VideoMetadata[]> GetVideoMetadata(string videoUrl)
        {
            try
            {
                if (IsYoutubeUrl(videoUrl))
                {
                    var meta = await GetYoutubeMeta(videoUrl);
                    if (meta != null)
                        return new VideoMetadata[] { meta };
                }
                else if (IsSteamUrl(videoUrl))
                    return await GetSteamMeta(videoUrl);
                else if (IsIgnUrl(videoUrl))
                {
                    var meta = await GetIgnMeta(videoUrl);
                    if (meta != null)
                        return new VideoMetadata[] { meta };
                }
                else if (IsGogUrl(videoUrl))
                    return await GetGogMeta(videoUrl);
            }
            catch(Exception ex)
            {
                _log.Error("Could not download metadata", ex);
            }

            return new VideoMetadata[] { };
        }

        public static bool IsYoutubeUrl(string url)
        {
            var uri = new Uri(url);
            return YouTubeHosts.Contains(uri.Host, StringComparer.InvariantCultureIgnoreCase);
        }

        public static bool IsSteamUrl(string url)
        {
            return url.Contains("store.steampowered.com", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsIgnUrl(string url)
        {
            return url.Contains("ign.com", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsGogUrl(string url)
        {
            var uri = new Uri(url);
            return uri.Host.Equals("www.gog.com", StringComparison.InvariantCultureIgnoreCase);
        }

        #region Private Methods
        private async Task<VideoMetadata> GetYoutubeMeta(string url)
        {
            var youtube = new YoutubeClient();
            var meta = await youtube.Videos.GetAsync(url);
            if (meta != null)
            {
                var uri = new Uri(url);
                var queryStrings = HttpUtility.ParseQueryString(uri.Query);

                return new VideoMetadata
                {
                    Title = meta.Title,
                    Url = meta.Url,
                    Duration = meta.Duration.GetValueOrDefault(),
                    Thumbnail = meta.Thumbnails.FirstOrDefault().Url,
                    StartTime = queryStrings["t"] != null && int.TryParse(queryStrings["t"], out var time)
                                ? time 
                                : 0
                };
            }
            return null;
        }

        private async Task<VideoMetadata[]> GetSteamMeta(string url)
        {
            if (!string.IsNullOrEmpty(LaunchboxSettingsReader.SteamApiKey))
                return await GetSteamMetaViaApi(url);
            else
                return await GetSteamMetaViaScraping(url);
        }

        private async Task<VideoMetadata[]> GetSteamMetaViaApi(string url)
        {
            // factory to be used to generate various web interfaces
            var webInterfaceFactory = new SteamWebInterfaceFactory(LaunchboxSettingsReader.SteamApiKey);

            var uri = new Uri(url);
            var urlPart = uri.AbsolutePath.Replace("/app/", "");
            var nextSlashIndex = urlPart.IndexOf("/");
            var gameIdStr = urlPart.Substring(0, nextSlashIndex);

            if (uint.TryParse(gameIdStr, out var gameId))
            {
                var store = webInterfaceFactory.CreateSteamStoreInterface(new HttpClient());
                var details = await store.GetStoreAppDetailsAsync(gameId);
                if (details != null && details.Movies != null)
                {
                    var result = details.Movies
                        .Select(x => new VideoMetadata
                        {
                            Title = x.Name,
                            Url = x.Webm.Max,
                            Thumbnail = x.Thumbnail
                        })
                        .ToArray();

                    return result;
                }
            }
            return new VideoMetadata[] { };
        }

        private async Task<VideoMetadata[]> GetSteamMetaViaScraping(string url)
        {
            var webclient = new WebClient();
            string htmlPage = webclient.DownloadString(url);

            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(htmlPage);

            var videoNodes = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class,'highlight_movie')]");
            if (videoNodes != null && videoNodes.Count > 0)
            {
                var result = videoNodes
                    .Where(x => x.Attributes["data-mp4-hd-source"] != null)
                    .Select(x => new VideoMetadata
                    {
                        Url = x.Attributes["data-mp4-hd-source"].Value,
                        Thumbnail = x.Attributes["data-poster"].Value
                    })
                    .ToArray();

                return result;
            }
            return new VideoMetadata[] { };
        }

        private async Task<VideoMetadata> GetIgnMeta(string url)
        {
            var webclient = new WebClient();
            webclient.Headers.Add("user-agent", " Mozilla/5.0 (Windows NT 6.1; WOW64; rv:25.0) Gecko/20100101 Firefox/25.0");
            string htmlPage = webclient.DownloadString(url);

            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(htmlPage);

            var jsonNode = htmlDoc.GetElementbyId("__NEXT_DATA__");
            if (jsonNode?.Attributes["type"]?.Value == "application/json")
            {
                dynamic metadata = JObject.Parse(jsonNode.InnerHtml);
                dynamic videoMeta = metadata?.props?.pageProps?.video;
                if (videoMeta != null)
                {
                    dynamic asset = ((IEnumerable)videoMeta.assets)
                                .Cast<dynamic>()
                                .Where(x => x.width <= 1920)
                                .LastOrDefault();
                    int? durationSeconds = videoMeta?.duration;

                    var result = new VideoMetadata
                    {
                        Title = videoMeta.title,
                        Url = asset?.url,
                        Thumbnail = videoMeta.thumbnailUrl,
                        Duration = durationSeconds.HasValue
                                    ? TimeSpan.FromSeconds(durationSeconds.Value)
                                    : TimeSpan.Zero
                    };
                    return result;
                }
            }

            return null;
        }

        private async Task<VideoMetadata[]> GetGogMeta(string url)
        {
            var webclient = new WebClient();
            string htmlPage = webclient.DownloadString(url);

            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(htmlPage);

            var iframes = htmlDoc.DocumentNode.SelectNodes("//iframe");
            var youtubeUrls = iframes
                .Where(x => x.Attributes["src"] != null)
                .Where(x => x.Attributes["src"].Value.Contains("youtube.com", StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Attributes["src"].Value)
                .ToList();

            var metas = new List<VideoMetadata>();
            foreach(var yUrl in youtubeUrls)
            {
                var meta = await GetYoutubeMeta(yUrl);
                if (meta != null)
                    metas.Add(meta);
            }
            return metas.ToArray();
        }
        #endregion
    }
}
