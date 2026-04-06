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

using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineVideoLinks.Utilities
{
    public class VlcUtilities
    {
        private static readonly ILog _log = LogManager.GetLogger(nameof(VlcUtilities));

        /// <summary>
        /// URL to download yt-dlp executable.
        /// </summary>
        private const string YtDlpDownloadUrl = "https://github.com/yt-dlp/yt-dlp/releases/latest/download/yt-dlp.exe";

        /// <summary>
        /// Retrieves the VLC folder path.
        /// </summary>
        /// <returns></returns>
        public static string GetVlcFolderPath()
        {
            var vlcEnvironment = Environment.Is64BitOperatingSystem ? "x64" : "x86";
            var relativePath = $"ThirdParty\\VLC\\{vlcEnvironment}";
            var fullPath = Path.Combine(Environment.CurrentDirectory, relativePath);

            if (File.Exists(fullPath + "\\vlc.exe"))
                return relativePath;

            // Otherwise try the old Launchbox location - root level.
            relativePath = $"VLC\\{vlcEnvironment}";
            fullPath = Path.Combine(Environment.CurrentDirectory, relativePath);
            if (File.Exists(fullPath + "\\vlc.exe"))
                return relativePath;

            // Otherwise try the Program Files installation
            var absolutePath = "C:\\Program Files\\VideoLAN\\VLC";
            fullPath = Path.Combine(absolutePath, "vlc.exe");
            if (File.Exists(fullPath))
                return absolutePath;

            return null;
        }

        /// <summary>
        /// Retrieves the VLC executable path.
        /// </summary>
        /// <returns></returns>
        public static string GetVlcExecutablePath()
        {
            var folderPath = GetVlcFolderPath();
            if (!string.IsNullOrEmpty(folderPath))
                return $"\"{folderPath}\\vlc.exe\"";
            return null;
        }

        /// <summary>
        /// Checks whether VLC is installed.
        /// </summary>
        public static bool IsVlcInstalled()
        {
            return !string.IsNullOrEmpty(GetVlcExecutablePath());
        }

        /// <summary>
        /// Gets the path to the yt-dlp executable in the plugin directory (Plugins\Costin.OnlineVideoLinks\Tools).
        /// </summary>
        public static string GetYtDlpPath()
        {
            var pluginDir = Path.Combine(Environment.CurrentDirectory, "Plugins", "Costin.OnlineVideoLinks", "Tools");
            return Path.Combine(pluginDir, "yt-dlp.exe");
        }

        /// <summary>
        /// Downloads yt-dlp if not already installed.
        /// yt-dlp is used to extract direct video URLs from YouTube since VLC's built-in
        /// YouTube support frequently breaks due to YouTube API changes.
        /// </summary>
        public static void VerifyYtDlp()
        {
            var ytDlpPath = GetYtDlpPath();
            var pluginDir = Path.GetDirectoryName(ytDlpPath);

            // Ensure the plugin directory exists
            if (!Directory.Exists(pluginDir))
            {
                Directory.CreateDirectory(pluginDir);
            }

            // Download yt-dlp if it doesn't exist or is older than 7 days
            if (!File.Exists(ytDlpPath) || (DateTime.Now - File.GetLastWriteTime(ytDlpPath)).TotalDays > 7)
            {
                try
                {
                    _log.Info($"Downloading yt-dlp to {ytDlpPath}...");
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                        var bytes = client.GetByteArrayAsync(YtDlpDownloadUrl).Result;
                        File.WriteAllBytes(ytDlpPath, bytes);
                    }
                    _log.Info("yt-dlp downloaded successfully.");
                }
                catch (Exception ex)
                {
                    _log.Error("Could not download yt-dlp.", ex);
                }
            }
        }

        /// <summary>
        /// Checks if the given URL is a YouTube URL.
        /// </summary>
        public static bool IsYouTubeUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            return url.Contains("youtube.com") || url.Contains("youtu.be");
        }

        /// <summary>
        /// Uses yt-dlp to extract the direct video URL(s) from a YouTube link.
        /// Returns video and audio URLs separately if they are split, or a single URL for combined formats.
        /// Returns null if yt-dlp fails or is not available.
        /// </summary>
        public static (string videoUrl, string audioUrl) GetDirectVideoUrls(string youtubeUrl)
        {
            if (!IsYouTubeUrl(youtubeUrl))
                return (youtubeUrl, null);

            var ytDlpPath = GetYtDlpPath();
            if (!File.Exists(ytDlpPath))
            {
                _log.Warn("yt-dlp not found, falling back to direct URL.");
                return (youtubeUrl, null);
            }

            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = ytDlpPath,
                        // Get best video+audio format that's playable by VLC, prefer mp4
                        // This may return two URLs: one for video, one for audio
                        Arguments = $"-f \"bv*[ext=mp4]+ba[ext=m4a]/b[ext=mp4]/bv*+ba/b\" --get-url \"{youtubeUrl}\"",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                var output = process.StandardOutput.ReadToEnd().Trim();
                var error = process.StandardError.ReadToEnd();
                process.WaitForExit(30000); // 30 second timeout

                if (process.ExitCode == 0 && !string.IsNullOrWhiteSpace(output))
                {
                    var urls = output.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    if (urls.Length >= 2)
                    {
                        // First URL is video, second is audio
                        _log.Debug("Resolved YouTube URL to separate video and audio streams.");
                        return (urls[0], urls[1]);
                    }
                    else if (urls.Length == 1)
                    {
                        // Single combined URL
                        _log.Debug("Resolved YouTube URL to combined stream.");
                        return (urls[0], null);
                    }
                }

                if (!string.IsNullOrWhiteSpace(error) && !error.Contains("WARNING"))
                {
                    _log.Warn($"yt-dlp error: {error}");
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error running yt-dlp.", ex);
            }

            return (youtubeUrl, null);
        }
    }
}
