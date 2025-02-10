using log4net;
using OnlineVideoLinks.Gamepad;
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
        IGameVideoUtility _gameVideoUtilities;

        //GamepadDinputProvider _gamepadDinputProvider;
        IGamepadXinputProvider _gamepadXinputProvider;

        public VideoSelectorForm(IGame game, 
            IGameVideoUtility gameVideoUtilities, 
            IGamepadXinputProvider gamepadXinputProvider)
        {
            InitializeComponent();

            _game = game;
            _gameVideoUtilities = gameVideoUtilities;
            _gamepadXinputProvider = gamepadXinputProvider;

            var customVideos = _gameVideoUtilities.GetGameVideos(_game);
            listBoxVideos.DataSource = customVideos;
            listBoxVideos.DisplayMember = "Title";
            listBoxVideos.SelectedIndex = 0;

            _gamepadXinputProvider.ButtonPressed += _gamepadXinputProvider_ButtonPressed;
            _gamepadXinputProvider.StartListening();
        }

        private void VideoSelectorForm_Load(object sender, EventArgs e)
        {
            if(PluginHelper.StateManager?.IsBigBox == true)
                Cursor.Hide();
        }

        private void _gamepadXinputProvider_ButtonPressed(object sender, Models.XInputEventArgs e)
        {
            if (this.IsHandleCreated)
                Invoke(new Action(() =>
                {
                    HandleXInput_ButtonPressed(e.ButtonPressed);
                }));
            else HandleXInput_ButtonPressed(e.ButtonPressed);
        }

        private void HandleXInput_ButtonPressed(GamepadButtonFlags buttonPressed)
        {
            switch (buttonPressed)
            {
                case GamepadButtonFlags.A:
                    if (!_gameVideoUtilities.IsPlaying())
                    {
                        var selectedVideo = listBoxVideos.SelectedItem as GameVideo;
                        _gameVideoUtilities.Play(selectedVideo);
                    }
                    break;
                case GamepadButtonFlags.B:
                    if (_gameVideoUtilities.IsPlaying())
                    {
                        _gameVideoUtilities.StopPlaying();
                    }
                    else
                        this.Close();
                    break;
                case GamepadButtonFlags.DPadDown:
                    if (!_gameVideoUtilities.IsPlaying() && listBoxVideos.SelectedIndex < listBoxVideos.Items.Count - 1)
                        listBoxVideos.SelectedIndex++;
                    break;
                case GamepadButtonFlags.DPadUp:
                    if (!_gameVideoUtilities.IsPlaying() && listBoxVideos.SelectedIndex > 0)
                        listBoxVideos.SelectedIndex--;
                    break;
            }
        }

        private void listBoxVideos_KeyUp(object sender, KeyEventArgs e)
        {
            // Translating keys to gamepad buttons.
            if (e.KeyCode == Keys.Enter)
                HandleXInput_ButtonPressed(GamepadButtonFlags.A);
            else if (e.KeyCode == Keys.Escape)
                HandleXInput_ButtonPressed(GamepadButtonFlags.B);
        }

        private void VideoSelectorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PluginHelper.StateManager?.IsBigBox == true)
                Cursor.Show();

            //_gamepadDinputProvider.TurnOff();
            _gamepadXinputProvider.StopListening();
            _log.Info("Video Selector Form closing. Gamepad provider turned off.");
        }
    }
}
