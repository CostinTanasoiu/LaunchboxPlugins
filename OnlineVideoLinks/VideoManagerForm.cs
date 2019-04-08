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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;
using OnlineVideoLinks.Models;

namespace OnlineVideoLinks
{
    public partial class VideoManagerForm : Form
    {
        private IGame _game;
        private HelpForm _helpForm = new HelpForm();

        private BindingList<GameVideo> _gameVideos = new BindingList<GameVideo>();

        public BindingList<GameVideo> GameVideos
        {
            get { return _gameVideos; }
        }

        public VideoManagerForm()
        {
            InitializeComponent();
        }

        public VideoManagerForm(IGame game)
        {
            InitializeComponent();
            _game = game;

            this.Text = "Manage videos for: " + game.Title;

            var videoList = GameVideo.ExtractVideoLinksFromGame(_game);
            foreach(var gameVideo in videoList)
                _gameVideos.Add(gameVideo);
        }

        private void GameVideoManagerForm_Load(object sender, EventArgs e)
        {
            gridVideos.AutoGenerateColumns = false;
            gridVideos.DataSource = _gameVideos;
        }

        /// <summary>
        /// Builds a new video model.
        /// </summary>
        /// <param name="addToList">If true, it will add it to the loaded game's additional apps list.</param>
        /// <returns>The game video model.</returns>
        private GameVideo BuildNewVideo(bool addToList)
        {
            var title = txtVideoTitle.Text.Trim();
            var path = txtVideoPath.Text.Trim();
            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(path))
            {
                if (_gameVideos.Any(x => x.Title.Equals(title, StringComparison.CurrentCultureIgnoreCase)))
                    MessageBox.Show("A video with that title already exists for this game.");
                else
                {
                    var newVideo = new GameVideo
                    {
                        Title = txtVideoTitle.Text,
                        VideoPath = txtVideoPath.Text,
                        StartTime = txtStartTime.GetSeconds(),
                        StopTime = txtStopTime.GetSeconds()
                    };

                    if (addToList)
                    {
                        _gameVideos.Add(newVideo);
                        ResetNewVideoFields();
                    }

                    return newVideo;
                }
            }
            else
                MessageBox.Show("Please fill in all the required fields first.");

            return null;
        }

        private void PlayVideo(GameVideo gameVideo)
        {
            var vlcExecutable = Utilities.GetVlcExecutablePath();
            var cmdArgs = gameVideo.GetVlcCmdArguments();
            Process.Start(vlcExecutable, cmdArgs);
        }

        private void ResetNewVideoFields()
        {
            txtVideoTitle.Text = txtVideoPath.Text = "";
            txtStartTime.ClearValue();
            txtStopTime.ClearValue();
        }

        private void btnAddVideo_Click(object sender, EventArgs e)
        {
            BuildNewVideo(true);
        }

        private void btnTestNewVideo_Click(object sender, EventArgs e)
        {
            var video = BuildNewVideo(false);
            if (video != null)
                PlayVideo(video);
        }

        private void gridVideos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn 
                && e.RowIndex >= 0 )
            {
                // Cell button clicked
                if (senderGrid.Columns[e.ColumnIndex].Name == "Delete")
                {
                    senderGrid.Rows.RemoveAt(e.RowIndex);
                }
                else if (senderGrid.Columns[e.ColumnIndex].Name == "Play")
                {
                    PlayVideo(_gameVideos[e.RowIndex]);
                }
            }
        }

        private void gridVideos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var columnName = gridVideos.Columns[e.ColumnIndex].Name;
            if (columnName == "StartTime" || columnName == "StopTime")
            {
                var textValue = e.FormattedValue.ToString();
                int number;
                if (string.IsNullOrEmpty(textValue) || !int.TryParse(textValue, out number))
                {
                    e.Cancel = true;
                    lblGridError.Text = "The start time and end time fields only accept numeric values.";
                    lblGridError.Visible = true;
                }
            }
        }

        private void gridVideos_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            lblGridError.Visible = false;
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            if(_game != null)
            {
                // Remove all existing videos from the game
                var gameVideoFields = _game.GetAllAdditionalApplications()
                    .Where(x => x.Name.StartsWith(GameVideo.TitlePrefix)).ToList();

                foreach (var videoField in gameVideoFields)
                    _game.TryRemoveAdditionalApplication(videoField);

                // Add the updated list of videos
                foreach(var video in _gameVideos)
                    video.AddVideoToGame(_game);

                PluginHelper.DataManager.Save(true);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkMoreInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _helpForm.ShowDialog();
        }

        private void VideoManagerForm_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            _helpForm.ShowDialog();
        }

        private void timeBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ':'))
                e.Handled = true;

            // only allow one separator
            if ((e.KeyChar == ':') && ((sender as TextBox).Text.IndexOf(':') > -1))
                e.Handled = true;
        }
    }
}
