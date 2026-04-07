using log4net;
using OnlineVideoLinks.Database;
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
            var videoEntries = GameVideoDb.Instance.GetVideosForGame(game.Id);
            return videoEntries.Any();
        }

        /// <summary>
        /// Retrieves a game's list of videos.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public GameVideo[] GetGameVideos(IGame game)
        {
            // Load videos from the database
            var videoEntries = GameVideoDb.Instance.GetVideosForGame(game.Id);
            var gameVideos = new List<GameVideo>();
            foreach (var entry in videoEntries)
            {
                gameVideos.Add(entry.ToGameVideo(game.Id));
            }
            return gameVideos.ToArray();
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
    }
}
