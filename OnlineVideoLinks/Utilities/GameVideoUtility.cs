using log4net;
using OnlineVideoLinks.Database;
using OnlineVideoLinks.Models;
using OnlineVideoLinks.WPF;
using SharpDX.XInput;
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
        void PlayPause();
        void SkipBackward();
        void SkipForward();
        void StopPlaying();
        void SendGamepadInput(GamepadButtonFlags button);
    }

    public class GameVideoUtility : IGameVideoUtility
    {
        ILog _log = LogManager.GetLogger(nameof(GameVideoUtility));

        private VideoPlayerWindow _player;

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
            StopPlaying();

            _player = new VideoPlayerWindow(video);
            _player.Closed += _player_Closed;
            _player.ShowDialog();

            CurrentlyPlayingVideo = video;
        }

        /// <summary>
        /// Stops playing the video.
        /// </summary>
        public void StopPlaying()
        {
            if (_player != null)
            {
                _player.Close();
                _player = null;

                CurrentlyPlayingVideo = null;
            }
        }

        /// <summary>
        /// Checks whether this video is currently playing.
        /// </summary>
        public bool IsPlaying()
        {
            return _player != null;
        }

        /// <summary>
        /// Skips 10 seconds forward.
        /// </summary>
        public void SkipForward()
        {
            _player.SkipForward();
        }

        /// <summary>
        /// Skips 10 seconds backward.
        /// </summary>
        public void SkipBackward()
        {
            _player.SkipBackward();
        }

        /// <summary>
        /// Toggles the play/pause function.
        /// </summary>
        public void PlayPause()
        {
            _player.PlayPause();
        }

        public void SendGamepadInput(GamepadButtonFlags button)
        {
            if(_player != null)
                _player.SendGamepadInput(button);
        }

        #region Private Methods


        private void _player_Closed(object sender, EventArgs e)
        {
            _player = null;
            CurrentlyPlayingVideo = null;
        }

        #endregion
    }
}
