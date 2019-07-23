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

using BulkGenreEditor.Models;
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
    public partial class FormCustomFieldEditor : Form
    {
        private IGame[] _selectedGames;
        private BindingList<CustomFieldGridItem> _customFieldGridItems = new BindingList<CustomFieldGridItem>();

        /// <summary>
        /// The form constructor.
        /// </summary>
        /// <param name="selectedGames">The list of currently selected games in Launchbox.</param>
        public FormCustomFieldEditor(IGame[] selectedGames)
        {
            InitializeComponent();

            _selectedGames = selectedGames;
            lblInstructions.Text = string.Format(lblInstructions.Text, selectedGames.Length);
        }
        
        private void FormCustomFieldEditor_Load(object sender, EventArgs e)
        {
            LoadFieldValues();
        }

        private void LoadFieldValues()
        {
            SetLoadingBar(true);

            // So far, it seems the only way to retrieve all custom fields
            // is to extract them from game entries.
            // Extracting custom field names only.
            var allGames = PluginHelper.DataManager.GetAllGames();
            //var allGames = _selectedGames;
            string[] allFields = allGames
                .SelectMany(x => x.GetAllCustomFields())
                .Select(x=>x.Name)
                .Distinct()
                .ToArray();

            foreach (var fieldName in allFields)
                _customFieldGridItems.Add(new CustomFieldGridItem { FieldName = fieldName });

            if (allFields != null)
            {
                gridCustomFields.AutoGenerateColumns = false;
                gridCustomFields.DataSource = _customFieldGridItems;
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
            var selectedFieldsWithValues = _customFieldGridItems
                .Where(x => x.IsSelected && !string.IsNullOrWhiteSpace(x.FieldValue))
                .ToList();
            foreach(var game in _selectedGames)
            {
                var gameCustomFields = game.GetAllCustomFields();
                foreach(var selectedField in selectedFieldsWithValues)
                {
                    // If the game already contains a custom field with this name, we edit it.
                    // Otherwise we add it.
                    var existingGameField = gameCustomFields.FirstOrDefault(x => x.Name == selectedField.FieldName);
                    if (existingGameField != null)
                        existingGameField.Value = selectedField.FieldValue;
                    else 
                    {
                        var newField = game.AddNewCustomField();
                        newField.Name = selectedField.FieldName;
                        newField.Value = selectedField.FieldValue;
                    }
                }
            }
        }

        private void RemoveFieldValues()
        {
            var selectedFields = _customFieldGridItems.Where(x => x.IsSelected).ToList();
            foreach (var game in _selectedGames)
            {
                var gameCustomFields = game.GetAllCustomFields();
                foreach (var selectedField in selectedFields)
                {
                    // If the game does contains a custom field with this name, we remove it
                    var existingGameField = gameCustomFields.FirstOrDefault(x => x.Name == selectedField.FieldName);
                    if (existingGameField != null)
                        game.TryRemoveCustomField(existingGameField);

                }
            }
        }

        #region Form event handlers

        private void btnAddGenres_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("The selected custom fields will have their values UPDATED for all your currently selected games. Are you sure you want to go ahead with this operation?",
                                     "Warning on overwriting values!",
                                     MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                SetLoadingBar(true);
                AddFieldValues();
                //bgWorkerSave.RunWorkerAsync();
                PluginHelper.DataManager.Save(true);
                this.Close();
            }
        }

        private void btnRemoveGenres_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("The selected custom fields will have their values DELETED for all your currently selected games. Are you sure you want to go ahead with this operation?",
                         "Warning on deleting values!",
                         MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                SetLoadingBar(true);
                RemoveFieldValues();
                //bgWorkerSave.RunWorkerAsync();
                PluginHelper.DataManager.Save(true);
                this.Close();
            }
        }

        private void gridCustomFields_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // If it's editing the Name cell of an existing item, then we want to cancel this.
            if (e.RowIndex != gridCustomFields.NewRowIndex
                && gridCustomFields.Columns[e.ColumnIndex].Name == "FieldName")
                e.Cancel = true;
        }

        private void gridCustomFields_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (gridCustomFields.Columns[e.ColumnIndex].Name == "FieldName")
            {
                // Check that the name is unique
                var nameExists = _customFieldGridItems.Any(x => x != _customFieldGridItems[e.RowIndex] && x.FieldName == e.FormattedValue.ToString());
                if (nameExists)
                {
                    e.Cancel = true;
                    MessageBox.Show("A custom field with this name already exists in the list.");
                }
            }
        }

        private void bgWorkerSave_DoWork(object sender, DoWorkEventArgs e)
        {
            PluginHelper.DataManager.Save(true);
        }

        private void bgWorkerSave_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void bgWorkerSave_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetLoadingBar(false);
            this.Close();
        }

        #endregion
    }
}
