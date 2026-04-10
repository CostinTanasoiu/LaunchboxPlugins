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
        GameVideo[] GetGameVideos(IGame game);
    }

    public class GameVideoUtility : IGameVideoUtility
    {
        ILog _log = LogManager.GetLogger(nameof(GameVideoUtility));

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
    }
}
