using log4net;
using OnlineVideoLinks.Models;
using OnlineVideoLinks.Utilities;
using SharpDX.DirectInput;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace OnlineVideoLinks
{
    public partial class VideoSelectorForm : Form
    {
        ILog _log = LogManager.GetLogger(nameof(VideoSelectorForm));

        IGame _game;
        GameVideo _selectedVideo;

        //GamepadDinputProvider _gamepadDinputProvider;
        GamepadXinputProvider _gamepadXinputProvider;

        public VideoSelectorForm(IGame game)
        {
            _game = game;
            InitializeComponent();
        }

        private void VideoSelectorForm_Load(object sender, EventArgs e)
        {
            var videos = GameVideoUtilities.GetGameVideos(_game);
            listBoxVideos.DataSource = videos;
            listBoxVideos.DisplayMember = "Title";
            listBoxVideos.SelectedIndex = 0;

            //_gamepadDinputProvider = new GamepadDinputProvider();
            _gamepadXinputProvider = new GamepadXinputProvider();
            _gamepadXinputProvider.ButtonPressed += _gamepadXinputProvider_ButtonPressed;
        }

        private void _gamepadXinputProvider_ButtonPressed(object sender, Models.XInputEventArgs e)
        {
            Invoke(new Action(() =>
            {
                switch (e.ButtonPressed)
                {
                    case GamepadButtonFlags.A:
                        _selectedVideo = listBoxVideos.SelectedItem as GameVideo;
                        _selectedVideo.Play();
                        break;
                    case GamepadButtonFlags.B:
                        if (_selectedVideo != null)
                        {
                            _selectedVideo.StopPlaying();
                            _selectedVideo = null;
                        }
                        else
                            this.Close();
                        break;
                    case GamepadButtonFlags.DPadDown:
                        if (listBoxVideos.SelectedIndex < listBoxVideos.Items.Count - 1)
                            listBoxVideos.SelectedIndex++;
                        break;
                    case GamepadButtonFlags.DPadUp:
                        if (listBoxVideos.SelectedIndex > 0)
                            listBoxVideos.SelectedIndex--;
                        break;
                }
            }));
        }

        private void listBoxVideos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _selectedVideo = listBoxVideos.SelectedItem as GameVideo;
                _selectedVideo.Play();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (_selectedVideo != null)
                {
                    _selectedVideo.StopPlaying();
                    _selectedVideo = null;
                }
                else
                    this.Close();
            }
        }

        private void VideoSelectorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //_gamepadDinputProvider.TurnOff();
            _gamepadXinputProvider.TurnOff();
            _log.Info("Video Selector Form closing. Gamepad provider turned off.");
        }
    }
}
