using log4net;
using VideoPlayer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace VideoPlayer.Utilities
{
    public interface IGameVideoUtility
    {
        OnlineVideoLinks.Models.GameVideo[] GetGameVideos(IGame game);
    }

    public class GameVideoUtility : IGameVideoUtility
    {
        ILog _log = LogManager.GetLogger(nameof(GameVideoUtility));

        //private VideoPlayerWindow _player;

        /// <summary>
        /// Checks whether a game has videos.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static bool DoesGameHaveVideos(IGame game)
        {
            var videoEntries = OnlineVideoLinks.Database.GameVideoDb.Instance.GetVideosForGame(game.Id);
            return videoEntries.Any();
        }

        /// <summary>
        /// Retrieves a game's list of videos.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public OnlineVideoLinks.Models.GameVideo[] GetGameVideos(IGame game)
        {
            // Load videos from the database
            var videoEntries = OnlineVideoLinks.Database.GameVideoDb.Instance.GetVideosForGame(game.Id);
            var gameVideos = new List<OnlineVideoLinks.Models.GameVideo>();
            foreach (var entry in videoEntries)
            {
                gameVideos.Add(new OnlineVideoLinks.Models.GameVideo
                {
                    GameId = game.Id,
                    Title = entry.Title,
                    VideoPath = entry.VideoPath,
                    StartTime = entry.StartTime,
                    StopTime = entry.StopTime
                });
            }
            return gameVideos.ToArray();
        }
    }
}
