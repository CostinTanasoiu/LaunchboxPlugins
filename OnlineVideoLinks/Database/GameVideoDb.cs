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

using OnlineVideoLinks.Models;
using OnlineVideoLinks.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineVideoLinks.Database
{
    /// <summary>
    /// Handles CRUD operations for game video data stored in a JSON file.
    /// </summary>
    public class GameVideoDb
    {
        private const string FileName = "GameVideos.json";
        private readonly string _filePath;
        private Dictionary<string, GameDataEntry> _database;

        private static readonly Lazy<GameVideoDb> _instance = new(() => new GameVideoDb());

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = null,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        /// <summary>
        /// Gets the singleton instance of the <see cref="GameVideoDb"/> class.
        /// </summary>
        public static GameVideoDb Instance => _instance.Value;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameVideoDb"/> class.
        /// </summary>
        private GameVideoDb()
        {
            _filePath = Path.Combine(GeneralUtilities.PluginDirectory, FileName);
            _database = Load();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameVideoDb"/> class with a custom folder path.
        /// This constructor is intended for testing purposes.
        /// </summary>
        /// <param name="pluginFolderPath">The path to the folder where the JSON file will be stored.</param>
        internal GameVideoDb(string pluginFolderPath)
        {
            _filePath = Path.Combine(pluginFolderPath, FileName);
            _database = Load();
        }

        /// <summary>
        /// Gets the game data for a specific game.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <returns>The game data entry, or null if it doesn't exist.</returns>
        public GameDataEntry GetGameData(string gameId)
        {
            return _database.TryGetValue(gameId, out var gameData) ? gameData : null;
        }

        /// <summary>
        /// Gets all videos for a specific game.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <returns>A list of video entries for the game, or an empty list if none exist.</returns>
        public List<GameVideoEntry> GetVideosForGame(string gameId)
        {
            if (_database.TryGetValue(gameId, out var gameData))
            {
                return gameData.Videos;
            }
            return [];
        }

        /// <summary>
        /// Gets all game IDs that have videos stored.
        /// </summary>
        /// <returns>A collection of game IDs.</returns>
        public IEnumerable<string> GetAllGameIds()
        {
            return _database.Keys;
        }

        /// <summary>
        /// Adds a video entry for a specific game.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="video">The video entry to add.</param>
        /// <param name="gameTitle">The game title (used when creating a new entry).</param>
        /// <param name="platform">The platform (used when creating a new entry).</param>
        public void AddVideo(string gameId, GameVideoEntry video, string gameTitle = null, string platform = null)
        {
            if (!_database.ContainsKey(gameId))
            {
                _database[gameId] = new GameDataEntry
                {
                    GameTitle = gameTitle ?? string.Empty,
                    Platform = platform ?? string.Empty,
                    Videos = []
                };
            }
            _database[gameId].Videos.Add(video);
            Save();
        }

        /// <summary>
        /// Adds multiple video entries for a specific game.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="videos">The video entries to add.</param>
        /// <param name="gameTitle">The game title (used when creating a new entry).</param>
        /// <param name="platform">The platform (used when creating a new entry).</param>
        public void AddVideos(string gameId, string gameTitle, string platform, IEnumerable<GameVideoEntry> videos)
        {
            if (!_database.ContainsKey(gameId))
            {
                _database[gameId] = new GameDataEntry
                {
                    GameTitle = gameTitle ?? string.Empty,
                    Platform = platform ?? string.Empty,
                    Videos = []
                };
            }
            _database[gameId].Videos.AddRange(videos);
            Save();
        }

        /// <summary>
        /// Updates a video entry at a specific index for a game.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="index">The index of the video to update.</param>
        /// <param name="video">The updated video entry.</param>
        /// <returns>True if the update was successful, false if the game or index doesn't exist.</returns>
        public bool UpdateVideo(string gameId, int index, GameVideoEntry video)
        {
            if (_database.TryGetValue(gameId, out var gameData) && index >= 0 && index < gameData.Videos.Count)
            {
                gameData.Videos[index] = video;
                Save();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Replaces all videos for a specific game.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="videos">The new list of video entries.</param>
        /// <param name="gameTitle">The game title (used when creating a new entry).</param>
        /// <param name="platform">The platform (used when creating a new entry).</param>
        public void SetVideosForGame(string gameId, string gameTitle, string platform, List<GameVideoEntry> videos)
        {
            if (videos == null || videos.Count == 0)
            {
                _database.Remove(gameId);
            }
            else
            {
                if (_database.TryGetValue(gameId, out var existingData))
                {
                    existingData.Videos = videos;
                    if (gameTitle != null) existingData.GameTitle = gameTitle;
                    if (platform != null) existingData.Platform = platform;
                }
                else
                {
                    _database[gameId] = new GameDataEntry
                    {
                        GameTitle = gameTitle ?? string.Empty,
                        Platform = platform ?? string.Empty,
                        Videos = videos
                    };
                }
            }
            Save();
        }

        /// <summary>
        /// Removes a video entry at a specific index for a game.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="index">The index of the video to remove.</param>
        /// <returns>True if the removal was successful, false if the game or index doesn't exist.</returns>
        public bool RemoveVideo(string gameId, int index)
        {
            if (_database.TryGetValue(gameId, out var gameData) && index >= 0 && index < gameData.Videos.Count)
            {
                gameData.Videos.RemoveAt(index);
                if (gameData.Videos.Count == 0)
                {
                    _database.Remove(gameId);
                }
                Save();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes all videos for a specific game.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <returns>True if the game existed and was removed, false otherwise.</returns>
        public bool RemoveAllVideosForGame(string gameId)
        {
            if (_database.Remove(gameId))
            {
                Save();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a game has any videos stored.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <returns>True if the game has videos, false otherwise.</returns>
        public bool HasVideos(string gameId)
        {
            return _database.ContainsKey(gameId) && _database[gameId].Videos.Count > 0;
        }

        /// <summary>
        /// Gets the count of videos for a specific game.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <returns>The number of videos for the game.</returns>
        public int GetVideoCount(string gameId)
        {
            return _database.TryGetValue(gameId, out var gameData) ? gameData.Videos.Count : 0;
        }

        /// <summary>
        /// Reloads the database from the JSON file.
        /// </summary>
        public void Reload()
        {
            _database = Load();
        }

        private Dictionary<string, GameDataEntry> Load()
        {
            if (!File.Exists(_filePath))
            {
                return [];
            }

            try
            {
                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<Dictionary<string, GameDataEntry>>(json, _jsonOptions) ?? [];
            }
            catch (JsonException)
            {
                return [];
            }
        }

        private void Save()
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var json = JsonSerializer.Serialize(_database, _jsonOptions);
            File.WriteAllText(_filePath, json);
        }
    }

    /// <summary>
    /// Represents the data for a game stored in the database, including metadata and videos.
    /// </summary>
    public class GameDataEntry
    {
        public string GameTitle { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
        public List<GameVideoEntry> Videos { get; set; } = [];
    }

    /// <summary>
    /// Represents a video entry stored in the database.
    /// </summary>
    public class GameVideoEntry
    {
        public string Title { get; set; } = string.Empty;
        public string VideoPath { get; set; } = string.Empty;
        public int StartTime { get; set; }
        public int StopTime { get; set; }

        /// <summary>
        /// Creates a GameVideoEntry from an existing GameVideo model.
        /// </summary>
        public static GameVideoEntry FromGameVideo(GameVideo video)
        {
            return new GameVideoEntry
            {
                Title = video.Title,
                VideoPath = video.VideoPath,
                StartTime = video.StartTime,
                StopTime = video.StopTime
            };
        }

        /// <summary>
        /// Converts this entry to a GameVideo model.
        /// </summary>
        public GameVideo ToGameVideo(string gameId = null)
        {
            return new GameVideo
            {
                GameId = gameId,
                Title = Title,
                VideoPath = VideoPath,
                StartTime = StartTime,
                StopTime = StopTime
            };
        }
    }
}
