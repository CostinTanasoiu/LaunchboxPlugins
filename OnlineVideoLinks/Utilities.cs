using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVideoLinks
{
    public class Utilities
    {
        /// <summary>
        /// Retrieves the VLC executable path.
        /// </summary>
        /// <returns></returns>
        public static string GetVlcExecutablePath()
        {
            var vlcEnvironment = Environment.Is64BitOperatingSystem ? "x64" : "x86";
            return $"VLC\\{vlcEnvironment}\\vlc.exe";
        }

    }
}
