using LaunchboxPluginsTests.MockedClasses;
using OnlineVideoLinks.Utilities;
using OnlineVideoLinks.WPF;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unbroken.LaunchBox.Plugins.Data;
using YoutubeGameVideos;

namespace WpfTestProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		private void Application_Startup(object sender, StartupEventArgs e)
		{
            RunVideoSelectorWindow();
		}

		private void RunVideoSelectorWindow()
        {
			new PluginStartup();
			OnlineVideoLinks.Utilities.VlcUtilities.VerifyYoutubeAddon();

            var game = new GameMock
            {
                Title = "Death and Return of Superman, The",
                Genres = new BlockingCollection<string> { "Beat' Em Up" },
                PlayModes = new string[] { "Single Player" },
                AdditionalApplications = new List<IAdditionalApplication>()
                {
                    new AdditionalApplicationMock
                    {
                        Name = "Video: Longplay",
                        ApplicationPath = @"C:\Games\LaunchBox\ThirdParty\VLC\x64\vlc.exe",
                        CommandLine = "https://youtu.be/yka30n1D6L0"
                    },
                    new AdditionalApplicationMock
                    {
                        Name = "Video: Is Death and Return of Superman Worth Playing Today?",
                        ApplicationPath = @"C:\Games\LaunchBox\ThirdParty\VLC\x64\vlc.exe",
                        CommandLine = "https://www.youtube.com/watch?v=7hfYthWLJsA"
                    },
                    new AdditionalApplicationMock
                    {
                        Name = "Video: Big Buck Bunny",
                        ApplicationPath = @"C:\Games\LaunchBox\ThirdParty\VLC\x64\vlc.exe",
                        CommandLine = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4"
                    },
                    new AdditionalApplicationMock
                    {
                        Name = "Video: Steam Video",
                        ApplicationPath = @"C:\Games\LaunchBox\ThirdParty\VLC\x64\vlc.exe",
                        CommandLine = "https://cdn.cloudflare.steamstatic.com/steam/apps/256790115/movie480_vp9.webm?t=1592821021"
                    },
                    new AdditionalApplicationMock
                    {
                        Name = "Video: Longplay",
                        ApplicationPath = @"C:\Games\LaunchBox\ThirdParty\VLC\x64\vlc.exe",
                        CommandLine = "https://youtu.be/yka30n1D6L0"
                    },
                    new AdditionalApplicationMock
                    {
                        Name = "Video: Is Death and Return of Superman Worth Playing Today?",
                        ApplicationPath = @"C:\Games\LaunchBox\ThirdParty\VLC\x64\vlc.exe",
                        CommandLine = "https://youtu.be/7hfYthWLJsA"
                    }
                }
            };

            var window = new VideoSelectorWindow(game,
                new GameVideoUtility(),
                new GamepadXinputProvider());
            window.Show();
        }
	}
}
