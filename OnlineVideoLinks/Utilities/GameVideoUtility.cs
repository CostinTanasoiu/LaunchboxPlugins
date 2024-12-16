using log4net;
using OnlineVideoLinks.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace OnlineVideoLinks.Utilities
{
    public interface IGameVideoUtility
    {
        GameVideo CurrentlyPlayingVideo { get; set; }

        GameVideo[] GetGameVideos(IGame game);
        bool IsPlaying();
        void Play(GameVideo video);
        void StopPlaying();
        void ValidateVideosForAllGames();
    }

    public class GameVideoUtility : IGameVideoUtility
    {
        ILog _log = LogManager.GetLogger(nameof(GameVideoUtility));

        private Process _playingProcess;

        public GameVideo CurrentlyPlayingVideo { get; set; }

        /// <summary>
        /// Checks whether a game has videos.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static bool DoesGameHaveVideos(IGame game)
        {
            return game.GetAllAdditionalApplications()
                        .Any(x => x.Name.StartsWith(GameVideo.TitlePrefix));
        }

        /// <summary>
        /// Retrieves a game's list of videos.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public GameVideo[] GetGameVideos(IGame game)
        {
            var videoLinkApps = game.GetAllAdditionalApplications()
                                    .Where(x => x.Name.StartsWith(GameVideo.TitlePrefix))
                                    .Select(x => new GameVideo(x))
                                    .ToArray();
            return videoLinkApps;
        }

        /// <summary>
        /// Validates the videos for all the games in LaunchBox.
        /// </summary>
        public void ValidateVideosForAllGames()
        {
            _log.Info("Starting video verification for all games...");
            var allGames = PluginHelper.DataManager.GetAllGames();
            _log.Info($"Retrieved all {allGames.Length} games.");

            var dateStart = DateTime.Now;
            foreach (var game in allGames)
                ValidateGameVideos(game);

            var timespan = DateTime.Now - dateStart;
            _log.Info($"Video verification done. Duration (ms): {timespan.TotalMilliseconds}");
        }

        /// <summary>
        /// Plays this video.
        /// </summary>
        public void Play(GameVideo video)
        {
            var vlcExecutable = VlcUtilities.GetVlcExecutablePath();
            var cmdArgs = video.GetVlcCmdArguments();
            _playingProcess = Process.Start(new ProcessStartInfo
            {
                FileName = vlcExecutable,
                Arguments = cmdArgs,
                UseShellExecute = false,
                RedirectStandardInput = true
            });

            CurrentlyPlayingVideo = video;

            //Thread.Sleep(5000);
            //_playingProcess.StandardInput.Write(" ");
        }

        /// <summary>
        /// Stops playing the video.
        /// </summary>
        public void StopPlaying()
        {
            if (_playingProcess != null)
            {
                //_playingProcess.Kill();
                _playingProcess.CloseMainWindow();
                _playingProcess.Close();
                _playingProcess = null;

                CurrentlyPlayingVideo = null;
            }
        }

        /// <summary>
        /// Checks whether this video is currently playing.
        /// </summary>
        public bool IsPlaying()
        {
            return _playingProcess != null && !_playingProcess.HasExited;
        }

        #region Private Methods

        /// <summary>
        /// This checks that the additional apps for launching our videos are set up correctly.
        /// This ensures that video link apps are always up to date with the latest VLC path and command-line arguments.
        /// </summary>
        /// <param name="game"></param>
        private void ValidateGameVideos(IGame game)
        {
            var videoLinkApps = game.GetAllAdditionalApplications()
                                    .Where(x => x.Name.StartsWith(GameVideo.TitlePrefix))
                                    .ToList();
            if (videoLinkApps.Count != 0)
            {
                foreach (var app in videoLinkApps)
                {
                    if (!GameVideo.IsAppCorrectlySetup(app))
                    {
                        var gameVideo = new GameVideo(app);
                        gameVideo.UpdateExistingApp(app);
                    }

                    // TEMP
                    if (app.Name == GameVideo.GamesDbVideoTitleWithPrefix)
                        game.TryRemoveAdditionalApplication(app);
                }
            }

            //HandleGamesDbVideoUrl(game, videoLinkApps);
        }

        /// <summary>
        /// This makes sure that the GamesDB video URL is added as a video app.
        /// </summary>
        private void HandleGamesDbVideoUrl(IGame game, List<IAdditionalApplication> videoLinkApps)
        {
            // Retrieving the additional app that represents the video linked from Games DB, if any.
            var app = videoLinkApps.FirstOrDefault(x => x.Name == GameVideo.GamesDbVideoTitleWithPrefix);

            if (!string.IsNullOrWhiteSpace(game.VideoUrl))
            {
                if (app != null)
                {
                    // If the additional app that we have uses a different URL we want 
                    // to update it to match the current video URL from Games DB.
                    var gameVideo = new GameVideo(app);
                    if (gameVideo.VideoPath != game.VideoUrl)
                    {
                        gameVideo.VideoPath = game.VideoUrl;
                        gameVideo.StartTime = 0;
                        gameVideo.StopTime = 0;
                        gameVideo.UpdateExistingApp(app);
                    }
                }
                else
                {
                    // Otherwise, we add a new additional app for this video.
                    var gameVideo = new GameVideo
                    {
                        VideoPath = game.VideoUrl,
                        Title = GameVideo.GamesDbVideoTitleNoPrefix
                    };
                    gameVideo.AddVideoToGame(game);
                }
            }
        }

        #endregion
    }
}
