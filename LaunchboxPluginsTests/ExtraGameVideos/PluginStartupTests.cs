using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins.Data;
using Xunit;
using YoutubeGameVideos;

namespace LaunchboxPluginsTests.ExtraGameVideos
{
    public class PluginStartupTests
    {
        [Fact]
        public void Can_Download_VLC_Addon_At_Startup()
        {
            // First check that VLC plugin doesn't already exist in the working folder
            var vlcEnvironment = Environment.Is64BitOperatingSystem ? "x64" : "x86";
            var addonFilePath = $"VLC\\{vlcEnvironment}\\lua\\playlist\\youtube.luac";
            if(File.Exists(addonFilePath))
                File.Delete(addonFilePath);

            var pluginStartup = new PluginStartup();
            pluginStartup.OnEventRaised(SystemEventTypes.LaunchBoxStartupCompleted);

            Assert.True(File.Exists(addonFilePath));
        }
    }
}
