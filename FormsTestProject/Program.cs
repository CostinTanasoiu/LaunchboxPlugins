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
using OnlineVideoLinks.Gamepad;
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

            var form1 = RunCustomFieldEditor();
            var form2 = RunVideoManagerForm();
            var form3 = RunVideoSelectorForm();
            
            form1.Show();
            form2.Show();
            form3.Show();

            Application.Run(new MainForm());
        }

        static Form RunVideoManagerForm()
        {
            new PluginStartup();
            var form = new OnlineVideoLinks.Forms.NewVideoManagerForm();
            return form;
        }

        static Form RunVideoSelectorForm()
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
                        Name = "Video: (local file) Turtle",
                        ApplicationPath = @"C:\Games\LaunchBox\ThirdParty\VLC\x64\vlc.exe",
                        CommandLine = Path.Combine(Environment.CurrentDirectory, @"OnlineVideoLinks\Assets\turtle_960x540_30s.mp4")
                    },
                    new AdditionalApplicationMock
                    {
                        Name = "Video: (local file) Jellyfish",
                        ApplicationPath = @"C:\Games\LaunchBox\ThirdParty\VLC\x64\vlc.exe",
                        CommandLine = Path.Combine(Environment.CurrentDirectory, @"OnlineVideoLinks\Assets\jellyfish_9s.mp4")
                    },
                    new AdditionalApplicationMock
                    {
                        Name = "Video: (youtube) Superman Worth Playing Today?",
                        ApplicationPath = @"C:\Games\LaunchBox\ThirdParty\VLC\x64\vlc.exe",
                        CommandLine = "https://youtu.be/7hfYthWLJsA"
                    },
                    new AdditionalApplicationMock
                    {
                        Name = "Video: (youtube) Sea of Stars timestamped",
                        ApplicationPath = @"C:\Games\LaunchBox\ThirdParty\VLC\x64\vlc.exe",
                        CommandLine = "--start-time=1840 --stop-time=1886 https://youtu.be/lETECcvmjlY"
                    },
                    new AdditionalApplicationMock
                    {
                        Name = "Video: (youtube) Pizza Possum Trailer",
                        ApplicationPath = @"C:\Games\LaunchBox\ThirdParty\VLC\x64\vlc.exe",
                        CommandLine = "https://www.youtube.com/watch?v=0GeWLeDWjng"
                    },
                    new AdditionalApplicationMock
                    {
                        Name = "Video: (youtube) Piza Possum Review (Co-Op Bros)",
                        ApplicationPath = @"C:\Games\LaunchBox\ThirdParty\VLC\x64\vlc.exe",
                        CommandLine = "https://www.youtube.com/watch?v=xg_O7Fc57CY"
                    },
                    new AdditionalApplicationMock
                    {
                        Name = "Video: (steam) Pizza Possum Co-Op Trailer",
                        ApplicationPath = @"C:\Games\LaunchBox\ThirdParty\VLC\x64\vlc.exe",
                        CommandLine = "https://video.fastly.steamstatic.com/store_trailers/256951885/movie_max_vp9.webm?t=1686321338"
                    }
                }
            }, new GameVideoUtility(), new GamepadXinputProvider());
            return form;
        }

        static Form RunCustomFieldEditor()
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
