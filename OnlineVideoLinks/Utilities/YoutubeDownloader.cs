using log4net;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;

namespace OnlineVideoLinks.Utilities
{
    public class YoutubeDownloader
    {
        private const int VIDEO_QUALITY_DESIRED = 1080;

        private static readonly ILog _log = LogManager.GetLogger(nameof(YoutubeDownloader));
        /// <summary>
        /// Gets the path to FFmpeg from LaunchBox's ThirdParty folder.
        /// Plugin is at: LaunchBox\Plugins\Costin.OnlineVideoLinks
        /// FFmpeg is at: LaunchBox\ThirdParty\FFMPEG\ffmpeg.exe
        /// </summary>
        private static string GetLaunchBoxFFmpegPath()
        {
            var ffmpegPath = Path.Combine(Environment.CurrentDirectory, "ThirdParty", "FFMPEG", "ffmpeg.exe");
            return File.Exists(ffmpegPath) ? ffmpegPath : null;
        }

        /// <summary>
        /// Gets the direct URL for a muxed YouTube stream (up to 720p, no download required).
        /// </summary>
        public static async Task<string> GetMuxedStreamUrl(string videoUrl)
        {
            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);

            // Get best muxed stream (contains both audio and video, max 720p)
            var muxedStream = streamManifest
                .GetMuxedStreams()
                .GetWithHighestVideoQuality();

            _log.Info($"Muxed stream: {muxedStream?.Container} | {muxedStream?.VideoQuality} | {muxedStream?.Size}");

            return muxedStream?.Url;
        }

        /// <summary>
        /// Downloads and muxes the best quality audio and video streams into a single MP4 file.
        /// Uses LaunchBox's FFmpeg installation.
        /// </summary>
        public static async Task DownloadBestQualityMP4(string videoUrl, string outputFilePath)
        {
            var ffmpegPath = GetLaunchBoxFFmpegPath();
            if (ffmpegPath == null)
                throw new FileNotFoundException("FFmpeg not found in LaunchBox ThirdParty folder");

            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);

            // Select best audio stream (highest bitrate)
            var audioStreamInfo = streamManifest
                .GetAudioStreams()
                .Where(s => s.Container == Container.Mp4)
                .GetWithHighestBitrate();

            // Select best video stream (highest quality up to VIDEO_QUALITY_DESIRED)
            var videoStreamInfo = streamManifest
                .GetVideoStreams()
                .Where(s => s.Container == Container.Mp4)
                .Where(s => s.VideoQuality.MaxHeight <= VIDEO_QUALITY_DESIRED)
                .GetWithHighestVideoQuality();

            // Download and mux streams into a single file using LaunchBox's FFmpeg
            var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo };
            _log.Info($"Found streams for video '{videoUrl}'");
            _log.Info($"Audio stream: {audioStreamInfo?.Container} | {audioStreamInfo?.Bitrate} | {audioStreamInfo?.Size}");
            _log.Info($"Video stream: {videoStreamInfo?.Container} | {videoStreamInfo?.VideoQuality} | {videoStreamInfo?.Size}");

            var conversionRequest = new ConversionRequestBuilder(outputFilePath)
                .SetFFmpegPath(ffmpegPath)
                .Build();
            await youtube.Videos.DownloadAsync(streamInfos, conversionRequest);
            _log.Info($"Downloaded and muxed video to: {outputFilePath}");
        }

        /// <summary>
        /// Gets a playable video path or URL for a YouTube video.
        /// Tries to download best quality first, falls back to muxed stream URL (720p max).
        /// </summary>
        public static async Task<string> GetPlayableVideoPath(string videoUrl, string outputFilePath)
        {
            try
            {
                // Try to download best quality with FFmpeg muxing
                await DownloadBestQualityMP4(videoUrl, outputFilePath);
                return Path.GetFullPath(outputFilePath);
            }
            catch
            {
                // Fall back to muxed stream URL (720p max, no download)
                return await GetMuxedStreamUrl(videoUrl);
            }
        }
    }
}
