using OnlineVideoLinks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode;
using System.IO;
using System.IO.Packaging;

namespace OnlineVideoLinks.Utilities
{
    public class YoutubeDownloader
    {
        public static async Task DownloadVideoMP4(string videoUrl, string outputFilePath)
        {
            //get youtube stream using youtube explode
            var youtube = new YoutubeClient();
            var video = await youtube.Videos.GetAsync(videoUrl);
            var title = video.Title;
            var author = video.Author.ChannelTitle;
            var duration = video.Duration;

            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);

            // Select best audio stream (highest bitrate)
            var audioStreamInfo = streamManifest
                .GetAudioStreams()
                .Where(s => s.Container == Container.Mp4)
                .GetWithHighestBitrate();

            // Select best video stream
            var videoStreamInfo = streamManifest
                .GetVideoStreams()
                .Where(s => s.Container == Container.Mp4)
                .GetWithHighestVideoQuality();

            // Download and mux streams into a single file
            var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo };

            var ffmpegPath = Path.Combine(Environment.CurrentDirectory, @"ffmpeg\ffmpeg.exe");
            var conversionReqBuilder = new ConversionRequestBuilder("temp_video.mp4")
                .SetFFmpegPath(ffmpegPath)
                .Build();
            await youtube.Videos.DownloadAsync(streamInfos, conversionReqBuilder);

        }

        public static async Task DownloadTimestampedVideoMP4(string videoUrl, string outputFilePath, int startTimeS = 0, int endTimeS = 0)
        {
            //get youtube stream using youtube explode
            var youtube = new YoutubeClient();
            var video = await youtube.Videos.GetAsync(videoUrl);
            var title = video.Title;
            var author = video.Author.ChannelTitle;
            var duration = video.Duration;

            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);

            // Select best audio stream (highest bitrate)
            var audioStreamInfo = streamManifest
                .GetAudioStreams()
                .Where(s => s.Container == Container.Mp4)
                .GetWithHighestBitrate();

            // Select best video stream
            var videoStreamInfo = streamManifest
                .GetVideoStreams()
                .Where(s => s.Container == Container.Mp4)
                .GetWithHighestVideoQuality();

            // Download and mux streams into a single file
            var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo };

            var ffmpegPath = Path.Combine(Environment.CurrentDirectory, @"ffmpeg\ffmpeg.exe");
            var conversionReqBuilder = new ConversionRequestBuilder("temp_video.mp4")
                .SetFFmpegPath(ffmpegPath)
                .Build();
            await youtube.Videos.DownloadAsync(streamInfos, conversionReqBuilder);

            //var args = new List<string>();
            //if (startTimeS != 0)
            //{
            //    var startTime = TimeSpan.FromSeconds(startTimeS);
            //    args.Add("-ss " + startTime.ToString());
            //}
            //if (endTimeS != 0)
            //{
            //    var spanDuration = TimeSpan.FromSeconds(endTimeS - startTimeS);
            //    args.Add("-t " + duration.ToString());
            //}
            //var argsString = string.Join(" ", args);

        }
    }
}
