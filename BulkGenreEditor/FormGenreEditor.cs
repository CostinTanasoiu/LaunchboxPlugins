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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace BulkGenreEditor
{
    public partial class FormGenreEditor : Form
    {
        private IGame[] _selectedGames;

        /// <summary>
        /// The form constructor.
        /// </summary>
        /// <param name="selectedGames">The list of currently selected games in Launchbox.</param>
        public FormGenreEditor(IGame[] selectedGames)
        {
            InitializeComponent();

            _selectedGames = selectedGames;
            lblInstructions.Text = string.Format(lblInstructions.Text, selectedGames.Length);
        }

        private void FormGenreEditor_Load(object sender, EventArgs e)
        {
            // So far, it seems the only way to retrieve all genres
            // is to extract them from game entries.
            var allGames = PluginHelper.DataManager.GetAllGames();
            var allGenres = allGames.SelectMany(x => x.Genres).Distinct().ToArray();
            checklistGenres.Items.AddRange(allGenres);

            SetLoading(false);

            // Focusing on the custom genre textbox when showing the form
            txtCustomGenre.Select();
        }

        #region Form event handlers

        private void btnAddGenres_Click(object sender, EventArgs e)
        {
            PluginHelper.DataManager.BackgroundReloadSave(AddGenres);
        }

        private void btnRemoveGenres_Click(object sender, EventArgs e)
        {
            PluginHelper.DataManager.BackgroundReloadSave(RemoveGenres);
        }

        private void btnCustomGenre_Click(object sender, EventArgs e)
        {
            AddCustomGenre();
        }

        private void txtCustomGenre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                AddCustomGenre();
        }

        #endregion

        private void SetLoading(bool displayLoader)
        {
            if (displayLoader)
            {
                picLoader.Visible = true;
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            }
            else
            {
                picLoader.Visible = false;
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private void AddGenres()
        {
            if (checklistGenres.CheckedItems.Count > 0)
            {
                var checkedGenres = checklistGenres.CheckedItems.Cast<string>().ToList();
                foreach (var game in _selectedGames)
                {
                    var genres = new List<string>(game.Genres);
                    foreach (var checkedGenre in checkedGenres)
                    {
                        if (!game.Genres.Contains(checkedGenre))
                            genres.Add(checkedGenre);
                    }
                    genres.Sort();
                    game.GenresString = string.Join(";", genres);
                }
            }

            this.Close();
        }

        private void RemoveGenres()
        {
            if (checklistGenres.CheckedItems.Count > 0)
            {
                var checkedGenres = checklistGenres.CheckedItems.Cast<string>().ToList();
                foreach (var game in _selectedGames)
                {
                    var genres = new List<string>(game.Genres);
                    foreach (var checkedGenre in checkedGenres)
                    {
                        genres.Remove(checkedGenre);
                    }
                    game.GenresString = string.Join(";", genres);
                }
            }

            this.Close();
        }

        private void AddCustomGenre()
        {
            var newGenre = txtCustomGenre.Text;
            if (!string.IsNullOrWhiteSpace(newGenre))
            {
                // We don't want the ';' character in the genre name
                if (newGenre.Contains(';'))
                {
                    MessageBox.Show("';' is not a valid character for a genre name.");
                    return;
                }

                // If the new genre doesn't exist in the list, then add it
                if (checklistGenres.FindStringExact(newGenre) == ListBox.NoMatches)
                    checklistGenres.Items.Add(newGenre, true);
                else
                    MessageBox.Show("Genre already exists in the list.");
                txtCustomGenre.Text = "";
            }
        }
    }
}
