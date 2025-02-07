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

using LaunchboxPluginsTests.MockedClasses;
using OnlineVideoLinks.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;
using YoutubeGameVideos;

namespace FormsTestProject
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            OnlineVideoLinks.Utilities.VlcUtilities.VerifyYoutubeAddon();

            var form1 = ConfigureCustomFieldEditor();
            var form2 = ConfigureVideoManagerForm();
            var form3 = ConfigureVideoSelectorForm();
            
            form1.Show();
            form2.Show();
            form3.Show();

            Application.Run(new MainForm());
        }

        static Form ConfigureVideoManagerForm()
        {
            new PluginStartup();
            var form = new OnlineVideoLinks.Forms.NewVideoManagerForm();
            return form;
        }

        static Form ConfigureVideoSelectorForm()
        {
            new PluginStartup();
            var form = new OnlineVideoLinks.VideoSelectorForm(new GameMock
            {
                Title = "Death and Return of Superman, The",
                Genres = new BlockingCollection<string> { "Beat' Em Up" },
                PlayModes = new string[] { "Single Player" },
                AdditionalApplications = new List<IAdditionalApplication>()
                {
                    new AdditionalApplicationMock
                    {
                        Name = "Video: (local file) Jellyfish",
                        ApplicationPath = @"C:\Games\LaunchBox\ThirdParty\VLC\x64\vlc.exe",
                        CommandLine = Path.Combine(Environment.CurrentDirectory, @"OnlineVideoLinks\Assets\jellyfish.mp4")
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
            }, new GameVideoUtility(), new GamepadXinputProvider());
            return form;
        }

        static Form ConfigureCustomFieldEditor()
        {
            var dummyGames = new GameMock[]
            {
                new GameMock
                {
                    Title = "Death and Return of Superman, The",
                    Genres = new BlockingCollection<string> { "Beat' Em Up" },
                    PlayModes = new string[] {"Single Player"}
                },
                new GameMock
                {
                    Title = "Aladdin",
                    Genres = new BlockingCollection<string>{"Action", "Adventure"},
                    PlayModes = new string[] {"Single Player"}
                },
                new GameMock
                {
                    Title = "The Ghoul Patrol",
                    Genres = new BlockingCollection<string>(),
                    PlayModes = new string[] {"Cooperative", "Multiplayer"}
                },
                new GameMock
                {
                    Title = "Dragon View",
                    Genres = new BlockingCollection<string>{ "Action", "RPG" },
                    PlayModes = new string[] {"Single Player"}
                }
            };

            for (var i = 1; i <= 20; i++)
                dummyGames.First().Genres.Add($"Test {i:00}");

            var form = new BulkGenreEditor.FormGenreEditor(PluginHelper.DataManager, dummyGames);
            return form;
        }
    }
}
