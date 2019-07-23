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
            comboBoxFields.Select();
        }

        #region Form event handlers

        private void comboBoxFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFieldValues();
        }

        private void btnAddGenres_Click(object sender, EventArgs e)
        {
            PluginHelper.DataManager.BackgroundReloadSave(AddFieldValues);
        }

        private void btnRemoveGenres_Click(object sender, EventArgs e)
        {
            PluginHelper.DataManager.BackgroundReloadSave(RemoveFieldValues);
        }

        private void btnCustomGenre_Click(object sender, EventArgs e)
        {
            AddCustomFieldValue();
        }

        private void txtCustomGenre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                AddCustomFieldValue();
        }

        #endregion

        private void LoadFieldValues()
        {
            // So far, it seems the only way to retrieve all genres
            // is to extract them from game entries.
            var allGames = PluginHelper.DataManager.GetAllGames();
            string[] allFieldValues = null;

            SetLoadingBar(true);

            checklistFieldValues.Items.Clear();
            switch (comboBoxFields.SelectedItem)
            {
                case "Genre":
                    allFieldValues = allGames.SelectMany(x => x.Genres).Distinct().ToArray();
                    break;
                case "Play Mode":
                    allFieldValues = allGames.SelectMany(x => x.PlayModes).Distinct().ToArray();
                    break;
            }

            btnAddGenres.Text = "Add " + comboBoxFields.SelectedItem;
            btnRemoveGenres.Text = "Remove " + comboBoxFields.SelectedItem;

            if (allFieldValues != null)
            {
                checklistFieldValues.Items.AddRange(allFieldValues);

                // Focusing on the custom genre textbox when showing the form
                txtCustomGenre.Select();
            }

            SetLoadingBar(false);
        }

        private void SetLoadingBar(bool displayLoader)
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

        private void AddFieldValues()
        {
            if (comboBoxFields.SelectedItem != null && checklistFieldValues.CheckedItems.Count > 0)
            {
                var fieldType = comboBoxFields.SelectedItem.ToString();
                var checkedValues = checklistFieldValues.CheckedItems.Cast<string>().ToList();
                foreach (var game in _selectedGames)
                {
                    if (fieldType == "Genre")
                    {
                        var genres = new List<string>(game.Genres);
                        foreach (var checkedGenre in checkedValues)
                        {
                            if (!game.Genres.Contains(checkedGenre))
                                genres.Add(checkedGenre);
                        }
                        genres.Sort();
                        game.GenresString = string.Join(";", genres);
                    }
                    else if (fieldType == "Play Mode")
                    {
                        var playModes = new List<string>(game.PlayModes);
                        foreach (var checkedPlayMode in checkedValues)
                        {
                            if (!game.PlayModes.Contains(checkedPlayMode))
                                playModes.Add(checkedPlayMode);
                        }
                        playModes.Sort();
                        game.PlayMode = string.Join(";", playModes);
                    }
                }
            }

            this.Close();
        }

        private void RemoveFieldValues()
        {
            if (comboBoxFields.SelectedItem != null && checklistFieldValues.CheckedItems.Count > 0)
            {
                var fieldType = comboBoxFields.SelectedItem.ToString();
                var checkedValues = checklistFieldValues.CheckedItems.Cast<string>().ToList();
                foreach (var game in _selectedGames)
                {
                    if (fieldType == "Genre")
                    {
                        var genres = new List<string>(game.Genres);
                        foreach (var checkedGenre in checkedValues)
                        {
                            genres.Remove(checkedGenre);
                        }
                        game.GenresString = string.Join("; ", genres);
                    }
                    else if (fieldType == "Play Mode")
                    {
                        var playModes = new List<string>(game.PlayModes);
                        foreach (var checkedPlayMode in checkedValues)
                        {
                            playModes.Remove(checkedPlayMode);
                        }
                        game.PlayMode = string.Join("; ", playModes);
                    }
                }
            }

            this.Close();
        }

        private void AddCustomFieldValue()
        {
            var newFieldValue = txtCustomGenre.Text;
            if (comboBoxFields.SelectedItem != null && !string.IsNullOrWhiteSpace(newFieldValue))
            {
                var fieldType = comboBoxFields.SelectedItem.ToString();

                // We don't want the ';' character in the field value
                if (newFieldValue.Contains(';'))
                {
                    MessageBox.Show($"';' is not a valid character for a {fieldType} name.");
                    return;
                }

                // If the new genre doesn't exist in the list, then add it
                if (checklistFieldValues.FindStringExact(newFieldValue) == ListBox.NoMatches)
                    checklistFieldValues.Items.Add(newFieldValue, true);
                else
                    MessageBox.Show($"{fieldType} already exists in the list.");
                txtCustomGenre.Text = "";
            }
        }
    }
}
