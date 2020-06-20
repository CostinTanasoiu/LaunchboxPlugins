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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace BulkGenreEditor
{
    public partial class FormGenreEditor : Form
    {
        private readonly IDataManager _launchboxDataManager;
        private IGame[] _selectedGames;
        private ActionPerformedEnum _actionPerformed = ActionPerformedEnum.None;

        /// <summary>
        /// The form constructor.
        /// </summary>
        /// <param name="selectedGames">The list of currently selected games in Launchbox.</param>
        public FormGenreEditor(IDataManager launchboxDataManager, IGame[] selectedGames)
        {
            InitializeComponent();
            this._launchboxDataManager = launchboxDataManager;
            this._selectedGames = selectedGames;
            lblInstructions.Text = string.Format(lblInstructions.Text, selectedGames.Length);
        }

        private void FormGenreEditor_Load(object sender, EventArgs e)
        {
            // So far, it seems the only way to retrieve all genres
            // is to extract them from game entries.
#if DEBUG
            var allGames = PluginHelper.DataManager != null ? PluginHelper.DataManager.GetAllGames() : _selectedGames;
#else
            var allGames = PluginHelper.DataManager.GetAllGames();
#endif
            var allGenres = allGames.SelectMany(x => x.Genres).Distinct().ToArray();
            checklistGenres.Items.AddRange(allGenres);
            SetLoading(false);

            multiSelectionBox.AutocompleteItems = allGenres.OrderBy(x => x).ToArray();
            // Focusing on the custom genre textbox when showing the form
            multiSelectionBox.Select();
        }

#region Form event handlers

        private void btnAddGenres_Click(object sender, EventArgs e)
        {
            SetLoading(true);
            if(PluginHelper.DataManager != null)
                PluginHelper.DataManager.ReloadIfNeeded();
            AddSelectedGenres();
            
            backgroundWorker.RunWorkerAsync();
        }

        private void btnRemoveGenres_Click(object sender, EventArgs e)
        {
            SetLoading(true);
            if (PluginHelper.DataManager != null)
                PluginHelper.DataManager.ReloadIfNeeded();
            RemoveSelectedGenres();

            backgroundWorker.RunWorkerAsync();
        }

        private void multiSelectionBox_SelectionCompleted(object sender, UserControls.MultiSelectionEventArgs e)
        {
            SelectGenres(e.TextValue);
        }

#endregion

#region Public Methods

        /// <summary>
        /// Adds the selected genres to the loaded games and exits the form.
        /// </summary>
        public void AddSelectedGenres()
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

                _actionPerformed = ActionPerformedEnum.AddedGenres;
            }
        }

        /// <summary>
        /// Removes the selected genres from the loaded games and exits the form.
        /// </summary>
        public void RemoveSelectedGenres()
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
                _actionPerformed = ActionPerformedEnum.RemovedGenres;
            }
        }

        /// <summary>
        /// Marks a genre or multiple genres as selected in the checklist.
        /// If the genre already exists, it will mark it as selected.
        /// If the genre does not exist, then it will create it and mark it as selected.
        /// </summary>
        /// <param name="genres">The genres to select, separated by semicolons.</param>
        public void SelectGenres(string genres)
        {
            genres = genres.Trim();
            if (!string.IsNullOrWhiteSpace(genres))
            {
                var genreItems = genres.Split(';')
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => x.Trim());

                foreach (var genre in genreItems)
                {
                    // If the new genre doesn't exist in the list, then add it
                    var itemIndex = checklistGenres.FindStringExact(genre);
                    if (itemIndex == ListBox.NoMatches)
                        itemIndex = checklistGenres.Items.Add(genre, true);
                    else
                        checklistGenres.SetItemChecked(itemIndex, true);

                    CenterAndHighlightChecklistItem(itemIndex);
                }
                multiSelectionBox.Text = "";
            }
        }

        /// <summary>
        /// Deselects a genre from the checklist.
        /// </summary>
        /// <param name="genre">The name of the genre to select.</param>
        public void DeselectGenre(string genre)
        {
            genre = genre.Trim();

            if (!string.IsNullOrWhiteSpace(genre))
            {
                var itemIndex = checklistGenres.FindStringExact(genre);
                if (itemIndex != ListBox.NoMatches)
                {
                    checklistGenres.SetItemChecked(itemIndex, false);
                }
            }
        }

#endregion

        private void SetLoading(bool displayLoader)
        {
            if (displayLoader)
            {
                progressBar.Visible = true;
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                this.Enabled = false;
            }
            else
            {
                progressBar.Visible = false;
                this.Cursor = System.Windows.Forms.Cursors.Default;
                this.Enabled = true;
            }
        }

        private void CenterAndHighlightChecklistItem(int index)
        {
            // Center this item in the checklist and highlight it
            var indexToScrollTo = index >= 4 ? index - 4 : 0;
            checklistGenres.TopIndex = indexToScrollTo;
            checklistGenres.SelectedIndex = index;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (PluginHelper.DataManager != null)
                PluginHelper.DataManager.Save(true);

#if DEBUG
            Thread.Sleep(5000);
#endif
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch (_actionPerformed)
            {
                case ActionPerformedEnum.AddedGenres:
                    MessageBox.Show($"{checklistGenres.CheckedItems.Count} genre(s) were added to the {_selectedGames.Length} selected game(s).", "Success!", MessageBoxButtons.OK);
                    break;
                case ActionPerformedEnum.RemovedGenres:
                    MessageBox.Show($"{checklistGenres.CheckedItems.Count} genre(s) were removed from the {_selectedGames.Length} selected game(s).", "Success!", MessageBoxButtons.OK);
                    break;
                default:
                    MessageBox.Show("No actions performed.");
                    break;
            }

            SetLoading(false);
            this.Close();
        }

        private void checklistGenres_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var count = e.NewValue == CheckState.Checked 
                ? checklistGenres.CheckedItems.Count + 1 
                : checklistGenres.CheckedItems.Count - 1;
            lblSelectionCount.Text = $"Selected genres: {count}";
        }
    }
}
