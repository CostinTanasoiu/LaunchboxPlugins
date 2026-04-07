using LaunchboxPluginsTests.MockedClasses;
using OnlineVideoLinks;
using OnlineVideoLinks.Gamepad;
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

            var game = new GameMock
            {
                Id = "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
                Title = "Death and Return of Superman, The",
                Genres = new BlockingCollection<string> { "Beat' Em Up" },
                PlayModes = new string[] { "Single Player" }
            };

            var window = new VideoSelectorWindow(game,
                new GameVideoUtility(),
                new GamepadXinputProvider());

            window.Show();
        }
	}
}
