using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BulkGenreEditor.UserControls
{
    public partial class MultiSelectionBox : UserControl
    {
        public string[] AutocompleteItems { get; set; }

        public string Text { 
            get => txtSearchBox.Text;
            set
            {
                txtSearchBox.Text = value;
            }
        }

        #region Events

        public delegate void SelectionCompletedHandler(object sender, MultiSelectionEventArgs e);
        public event SelectionCompletedHandler SelectionCompleted;

        #endregion

        public MultiSelectionBox()
        {
            InitializeComponent();
        }

        private void MultiSelectionBox_Load(object sender, EventArgs e)
        {
            listBoxAutocomplete.Visible = false;
            txtSearchBox.Select();

            this.Height = txtSearchBox.Margin.Top + txtSearchBox.Height;
        }

        private void txtSearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                case Keys.Enter:
                case Keys.Up:
                case Keys.Down:
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        private void txtSearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            // If we press Escape, we hide the autocomplete list box.
            if (e.KeyCode == Keys.Escape)
                listBoxAutocomplete.Visible = false;

            if(e.KeyCode == Keys.Up)
            {
                if (listBoxAutocomplete.SelectedIndex > 0)
                    listBoxAutocomplete.SelectedIndex--;
            }

            if (e.KeyCode == Keys.Down)
            {
                if (listBoxAutocomplete.SelectedIndex < listBoxAutocomplete.Items.Count - 1)
                    listBoxAutocomplete.SelectedIndex++;
            }

            if (e.KeyCode == Keys.Enter)
            {
                // If the autocomplete listbox is visible and still has items, 
                // we want to apply the selection from it.
                if (listBoxAutocomplete.Visible && listBoxAutocomplete.Items.Count > 0)
                {
                    var textValues = txtSearchBox.Text.Split(';');
                    if (textValues.Length > 0)
                        textValues[textValues.Length - 1] = listBoxAutocomplete.SelectedItem.ToString();
                    
                    txtSearchBox.Text = string.Join(";", textValues);

                    // Place the cursor at the end of the text
                    txtSearchBox.Select(txtSearchBox.Text.Length, 0);
                }
                // Otherwise, raise an event that we want to pass these values back to the form.
                else if (SelectionCompleted != null)
                {
                    SelectionCompleted(this, new MultiSelectionEventArgs(txtSearchBox.Text));
                }

                listBoxAutocomplete.Visible = false;
            }

            if(e.KeyCode == Keys.OemSemicolon)
            {
                var textValues = txtSearchBox.Text.Split(';')
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => x.Trim())
                    .ToArray();
                txtSearchBox.Text = string.Join(";", textValues) + ";";

                // Place the cursor at the end of the text
                txtSearchBox.Select(txtSearchBox.Text.Length, 0);
            }
        }

        private void txtSearchBox_TextChanged(object sender, EventArgs e)
        {
            var text = txtSearchBox.Text.Split(';').Last().Trim();
            listBoxAutocomplete.Visible = true;
            var newItems = AutocompleteItems
                .Where(x => x.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
                .ToArray();

            if (newItems.Length > 0)
                listBoxAutocomplete.DataSource = newItems;
            else
                listBoxAutocomplete.Visible = false;
        }

        private void listBoxAutocomplete_VisibleChanged(object sender, EventArgs e)
        {
            // When visibility changes, we also update the control's size.
            if (listBoxAutocomplete.Visible)
                this.Height = txtSearchBox.Margin.Top + txtSearchBox.Height 
                    + listBoxAutocomplete.Height + listBoxAutocomplete.Margin.Bottom;
            else
                this.Height = txtSearchBox.Margin.Top + txtSearchBox.Height;
        }
    }

    public class MultiSelectionEventArgs : EventArgs
    {
        public string TextValue { get; set; }

        public MultiSelectionEventArgs(string textValue)
        {
            TextValue = textValue;
        }
    }
}
