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
                        ApplicationPath = @"C:\Programs\LaunchBox\ThirdParty\VLC\x64\vlc.exe",
                        CommandLine = "https://youtu.be/yka30n1D6L0"
                    },
                    new AdditionalApplicationMock
                    {
                        Name = "Video: Is Death and Return of Superman Worth Playing Today?",
                        ApplicationPath = @"C:\Programs\LaunchBox\ThirdParty\VLC\x64\vlc.exe",
                        CommandLine = "https://youtu.be/7hfYthWLJsA"
                    },
                    new AdditionalApplicationMock
                    {
                        Name = "Video: Longplay",
                        ApplicationPath = @"C:\Programs\LaunchBox\ThirdParty\VLC\x64\vlc.exe",
                        CommandLine = "https://youtu.be/yka30n1D6L0"
                    },
                    new AdditionalApplicationMock
                    {
                        Name = "Video: Is Death and Return of Superman Worth Playing Today?",
                        ApplicationPath = @"C:\Programs\LaunchBox\ThirdParty\VLC\x64\vlc.exe",
                        CommandLine = "https://youtu.be/7hfYthWLJsA"
                    },
                    new AdditionalApplicationMock
                    {
                        Name = "Video: Longplay",
                        ApplicationPath = @"C:\Programs\LaunchBox\ThirdParty\VLC\x64\vlc.exe",
                        CommandLine = "https://youtu.be/yka30n1D6L0"
                    },
                    new AdditionalApplicationMock
                    {
                        Name = "Video: Is Death and Return of Superman Worth Playing Today?",
                        ApplicationPath = @"C:\Programs\LaunchBox\ThirdParty\VLC\x64\vlc.exe",
                        CommandLine = "https://youtu.be/7hfYthWLJsA"
                    }
                }
            };

            var window = new VideoSelectorWindow(game, 
                new GameVideoUtility(), new GamepadXinputProvider());
            window.Show();
        }
	}
}
