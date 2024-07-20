using OnlineVideoLinks.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace LaunchboxPluginsTests.OnlineVideoLinks
{
    public class VlcUtilitiesTests
    {
        [Fact]
        public void Can_Download_VLC_Addon()
        {
            var vlcEnvironment = Environment.Is64BitOperatingSystem ? "x64" : "x86";

            // Create the VLC root folder and dummy executable in our test project working folder
            var path = Path.Combine(Environment.CurrentDirectory, $"ThirdParty\\VLC\\{vlcEnvironment}");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (!File.Exists(Path.Combine(path, "vlc.exe")))
                File.Create(Path.Combine(path, "vlc.exe"));

            // Check that VLC plugin doesn't already exist in the working folder
            var addonFilePath = $"ThirdParty\\VLC\\{vlcEnvironment}\\lua\\playlist\\youtube.luac";
            if (File.Exists(addonFilePath))
                File.Delete(addonFilePath);


            VlcUtilities.VerifyYoutubeAddon();

            Assert.True(File.Exists(addonFilePath));
        }
    }
}
