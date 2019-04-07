/*
    Costin's LaunchBox Plugins
    https://github.com/SsjCosty/LaunchboxPlugins
    Copyright (C) 2019  Costin Tănăsoiu
    GNU General Public License v3.0

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

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
        #region Private members

        private static string[] _commonVlcArguments = new string[] { "-f", "--play-and-exit" };

        #endregion

        #region Public properties

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

        #endregion

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
            var cmdArgs = string.Join(" ", _commonVlcArguments);
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

        /// <summary>
        /// This method updates an existing Additional App with the current video details.
        /// </summary>
        /// <param name="additionalApplication"></param>
        public void UpdateExistingApp(IAdditionalApplication additionalApplication)
        {
            additionalApplication.Name = TitleWithPrefix;
            additionalApplication.ApplicationPath = Utilities.GetVlcExecutablePath();
            additionalApplication.CommandLine = GetVlcCmdArguments();
        }

        /// <summary>
        /// Verifies if an 'additional app' is properly set up with all the expected arguments.
        /// </summary>
        /// <param name="app">The Additional App entry.</param>
        public static bool IsAppCorrectlySetup(IAdditionalApplication app)
        {
            if (app.ApplicationPath != Utilities.GetVlcExecutablePath())
                return false;

            // Checking if the app's command line string is missing any of the VLC arguments expected by our plugin
            foreach(var expectedArg in _commonVlcArguments)
            {
                if (!app.CommandLine.Contains(expectedArg))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// This method checks if the given game has any video links.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static bool HasVideoLinks(IGame game)
        {
            return game.GetAllAdditionalApplications().Any(x => x.Name.StartsWith(GameVideo.TitlePrefix));
        }

        /// <summary>
        /// Extracts a list of video links from the given game object.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static List<GameVideo> ExtractVideoLinksFromGame(IGame game)
        {
            var results = new List<GameVideo>();

            var videoAppList = game.GetAllAdditionalApplications()
                .Where(x => x.Name.StartsWith(GameVideo.TitlePrefix))
                .ToList();

            foreach (var app in videoAppList)
            {
                var gameVideo = new GameVideo(app);
                results.Add(gameVideo);
            }

            return results;
        }
    }
}
