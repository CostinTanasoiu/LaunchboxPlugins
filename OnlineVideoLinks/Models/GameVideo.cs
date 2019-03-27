using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins.Data;

namespace OnlineVideoLinks.Models
{
    public class GameVideo
    {
        public const string TitlePrefix = "Video: ";

        public string Title { get; set; }

        /// <summary>
        /// The video path or URL. For example:
        /// https://www.youtube.com/watch?v=KpXkJ8rebDE
        /// https://youtu.be/_q4e7BwFFUU
        /// https://steamcdn-a.akamaihd.net/steam/apps/256742445/movie_max.webm?t=1553550374
        /// </summary>
        public string VideoPath { get; set; }

        /// <summary>
        /// Start time, in seconds. 0 value ignored.
        /// </summary>
        public int StartTime { get; set; }

        /// <summary>
        /// Stop time, in seconds. 0 value ignored.
        /// </summary>
        public int StopTime { get; set; }

        /// <summary>
        /// This is the name of the corresponding "additional application" for this video.
        /// </summary>
        public string TitleWithPrefix => TitlePrefix + Title;

        public GameVideo() { }

        /// <summary>
        /// Parses a LaunchBox Game additional application as a Game Video.
        /// </summary>
        /// <param name="app"></param>
        public GameVideo(IAdditionalApplication app)
        {
            Title = app.Name.Replace(TitlePrefix, "");

            var cmdArgsList = app.CommandLine.Split(' ');
            VideoPath = cmdArgsList.Last();

            var startTimeArg = cmdArgsList.Where(x => x.StartsWith("--start-time=")).SingleOrDefault();
            if (!string.IsNullOrEmpty(startTimeArg))
            {
                StartTime = int.Parse(startTimeArg.Replace("--start-time=", ""));
            }

            var stopTimeArg = cmdArgsList.Where(x => x.StartsWith("--stop-time=")).SingleOrDefault();
            if (!string.IsNullOrEmpty(stopTimeArg))
                StopTime = int.Parse(stopTimeArg.Replace("--stop-time=", ""));
        }

        /// <summary>
        /// Retrieves a string with all the command-line arguments required to play this video with VLC.
        /// </summary>
        public string GetVlcCmdArguments()
        {
            var cmdArgs = $"-f --play-and-exit";
            if (StartTime > 0)
                cmdArgs += " --start-time=" + StartTime.ToString();
            if (StopTime > 0)
                cmdArgs += " --stop-time=" + StopTime.ToString();

            cmdArgs += " " + VideoPath;
            return cmdArgs;
        }

        /// <summary>
        /// Adds the video details as a new 'Additional App' to the given game.
        /// </summary>
        /// <param name="game">The game entry.</param>
        /// <returns>The Additional App entry.</returns>
        public IAdditionalApplication AddVideoToGame(IGame game)
        {
            var app = game.AddNewAdditionalApplication();
            app.Name = TitleWithPrefix;
            app.ApplicationPath = Utilities.GetVlcExecutablePath();
            app.CommandLine = GetVlcCmdArguments();
            return app;
        }
    }
}
