using log4net;
using System;
using System.IO;
using System.Linq;
using System.Threading;
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
        /// FFmpeg is at: LaunchBox\ThirdParty\FFMPEG\ffmpeg.exe
        /// </summary>
        private static string GetLaunchBoxFFmpegPath()
        {
            var ffmpegPath = Path.Combine(PluginContext.Instance.LaunchBoxRootDirectory, "ThirdParty", "FFMPEG", "ffmpeg.exe");
            return File.Exists(ffmpegPath) ? ffmpegPath : null;
        }

        /// <summary>
        /// Gets the direct URL for a muxed YouTube stream (up to 720p, no download required).
        /// </summary>
        private static async Task<string> GetMuxedStreamUrl(string videoUrl, CancellationToken cancellationToken = default)
        {
            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl, cancellationToken);

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
        private static async Task DownloadBestQualityMP4(string videoUrl, string outputFilePath, CancellationToken cancellationToken = default)
        {
            var ffmpegPath = GetLaunchBoxFFmpegPath();
            if (ffmpegPath == null)
                throw new FileNotFoundException("FFmpeg not found in LaunchBox ThirdParty folder");

            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl, cancellationToken);

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
            await youtube.Videos.DownloadAsync(streamInfos, conversionRequest, cancellationToken: cancellationToken);
            _log.Info($"Downloaded and muxed video to: {outputFilePath}");
        }

        /// <summary>
        /// Gets a playable video path or URL for a YouTube video.
        /// If start/stop timestamps are specified, returns muxed stream URL for instant playback (720p max).
        /// Otherwise downloads best quality (up to 1080p) for full video playback.
        /// </summary>
        /// <param name="videoUrl">YouTube video URL</param>
        /// <param name="outputFilePath">Path to save downloaded video (only used when no timestamps)</param>
        /// <param name="startTime">Start time in seconds (0 = no start time)</param>
        /// <param name="stopTime">Stop time in seconds (0 = no stop time)</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public static async Task<string> GetPlayableVideoPath(
            string videoUrl, 
            string outputFilePath, 
            int startTime = 0, 
            int stopTime = 0, 
            CancellationToken cancellationToken = default)
        {
            // If timestamps are specified, use muxed stream URL for instant playback
            // (player will handle seeking to start time and stopping at stop time)
            if (startTime > 0 || stopTime > 0)
            {
                _log.Info($"Video has timestamps (start={startTime}, stop={stopTime}), using muxed stream URL for instant playback");
                return await GetMuxedStreamUrl(videoUrl, cancellationToken);
            }

            // No timestamps - download full video for best quality
            try
            {
                await DownloadBestQualityMP4(videoUrl, outputFilePath, cancellationToken);
                return Path.GetFullPath(outputFilePath);
            }
            catch (OperationCanceledException)
            {
                throw; // Re-throw cancellation exceptions
            }
            catch (Exception ex)
            {
                _log.Warn($"Failed to download and mux video '{videoUrl}' with FFmpeg. Falling back to muxed stream URL. Error: {ex.Message}");

                // Fall back to muxed stream URL (720p max, no download)
                return await GetMuxedStreamUrl(videoUrl, cancellationToken);
            }
        }
    }
}
