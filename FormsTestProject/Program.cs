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
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins.Data;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var dummyGames = new GameMock[]
            {
                new GameMock
                {
                    Title = "Death and Return of Superman, The",
                    Genres = new BlockingCollection<string> { "Beat' Em Up" },
                    PlayModes = new string[] {"Single Player"},
                    CustomFields = new List<ICustomField>
                    {
                        new CustomFieldMock{ Name="Fred", Value="Flintstone" }
                    }
                },
                new GameMock
                {
                    Title = "Aladdin",
                    Genres = new BlockingCollection<string>{"Action", "Adventure"},
                    PlayModes = new string[] {"Single Player"},
                    CustomFields = new List<ICustomField>
                    {
                        new CustomFieldMock{ Name="Barney", Value="Rubble" }
                    }
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

            //var form = new OnlineVideoLinks.VideoManagerForm();
            var form = new BulkGenreEditor.FormCustomFieldEditor(dummyGames);
            Application.Run(form);
        }
    }
}
