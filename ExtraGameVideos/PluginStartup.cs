using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace YoutubeGameVideos
{
    public class PluginStartup : ISystemEventsPlugin
    {
        // I learned about the YouTube VLC addon here:
        // https://www.latecnosfera.com/2016/10/vlc-unable-to-open-mrl.html

        /// <summary>
        /// This is a link to the youtube.luac file from the development branch of VLC.
        /// </summary>
        private const string YoutubeVlcAddonUrl = "http://git.videolan.org/?p=vlc.git;a=blob_plain;f=share/lua/playlist/youtube.lua;hb=HEAD";

        public void OnEventRaised(string eventType)
        {
            // On startup, we want to check if Launchbox's VLC distribution has the latest YouTube addon.
            if(eventType == SystemEventTypes.LaunchBoxStartupCompleted)
            {
                // Look for Launchbox's VLC distro
                var vlcEnvironment = Environment.Is64BitOperatingSystem ? "x64" : "x86";
                var vlcAddonsFolder = $"VLC\\{vlcEnvironment}\\lua\\playlist\\";

                var youtubeAddonPath = "";

                // Check if the YouTube VLC addon is installed
                if (Directory.Exists(vlcAddonsFolder))
                {
                    var files = Directory.GetFiles(vlcAddonsFolder, "youtube.luac", SearchOption.AllDirectories);
                    // I'll assume it's the first one in the array
                    if (files.Length != 0)
                        youtubeAddonPath = files[0];
                }
                else Directory.CreateDirectory(vlcAddonsFolder);

                // If we don't have the YouTube addon OR the file is more than a month old, then let's download it.
                // This is because YouTube transmission protocols change over time, and VLC developers are quite
                // keep up by updating their YouTube add-on file.
                if(youtubeAddonPath == "" 
                    || (DateTime.Now - File.GetLastWriteTime(youtubeAddonPath)).TotalDays > 30)
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(YoutubeVlcAddonUrl, vlcAddonsFolder + "youtube.luac");
                    }
                }

                // All done. Now VLC should be able to play YouTube videos!
            }
        }
    }
}
