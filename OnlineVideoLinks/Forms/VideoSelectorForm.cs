using log4net;
using OnlineVideoLinks.Models;
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
using OnlineVideoLinks.Gamepad;
using OnlineVideoLinks.Utilities;

namespace OnlineVideoLinks.Forms
{
    public partial class VideoSelectorForm : Form
    {
        ILog _log = LogManager.GetLogger(nameof(VideoSelectorForm));
        IGame _game;
        IGameVideoUtility _gameVideoUtilities;
        Func<IVideoPlayer> _videoPlayerFactory;

        //GamepadDinputProvider _gamepadDinputProvider;
        IGamepadXinputProvider _gamepadXinputProvider;

        // Current video player instance (created fresh for each video)
        private IVideoPlayer _currentPlayer;

        // Flag to indicate the form is ready to process input (set after handle is created in production,
        // or immediately in tests where forms aren't shown)
        private bool _isReadyForInput = false;

        public VideoSelectorForm(IGame game,
            IGameVideoUtility gameVideoUtilities,
            Func<IVideoPlayer> videoPlayerFactory,
            IGamepadXinputProvider gamepadXinputProvider)
        {
            InitializeComponent();

            _game = game;
            _gameVideoUtilities = gameVideoUtilities;
            _videoPlayerFactory = videoPlayerFactory;
            _gamepadXinputProvider = gamepadXinputProvider;

            var customVideos = _gameVideoUtilities.GetGameVideos(_game);
            listBoxVideos.DataSource = customVideos;
            listBoxVideos.DisplayMember = "Title";
            listBoxVideos.SelectedIndex = 0;

            _gamepadXinputProvider.ButtonPressed += _gamepadXinputProvider_ButtonPressed;
            _gamepadXinputProvider.StartListening();

            // In test scenarios (no handle), enable input immediately
            // In production, input is enabled after form is shown (in Form_Load)
            if (!this.IsHandleCreated)
                _isReadyForInput = true;
        }

        private void VideoSelectorForm_Load(object sender, EventArgs e)
        {
            if(PluginHelper.StateManager?.IsBigBox == true)
                Cursor.Hide();

            // Enable gamepad input processing now that handle is created
            _isReadyForInput = true;
        }

        private void _gamepadXinputProvider_ButtonPressed(object sender, XInputEventArgs e)
        {
            // Don't process input until form is ready
            if (!_isReadyForInput || this.IsDisposed)
                return;

            // If form handle is created, marshal to UI thread; otherwise call directly (for tests)
            if (this.IsHandleCreated)
            {
                Invoke(new Action(() =>
                {
                    HandleXInput_ButtonPressed(e.ButtonPressed);
                }));
            }
            else
            {
                // Direct call for tests (no handle, but _isReadyForInput is true)
                HandleXInput_ButtonPressed(e.ButtonPressed);
            }
        }

        private void HandleXInput_ButtonPressed(GamepadButtonFlags buttonPressed)
        {
            if (buttonPressed == GamepadButtonFlags.None)
                return;

            // If a player exists and is playing, forward input to it
            if (_currentPlayer != null && _currentPlayer.IsVisible)
            {
                _currentPlayer.SendGamepadInput(buttonPressed);
                _log.Info($"Sent gamepad input to player panel: {buttonPressed}");
                return;
            }

            _log.Info($"Allowing gamepad button pressed on selector panel: {buttonPressed}");

            switch (buttonPressed)
            {
                case GamepadButtonFlags.A:
                    var selectedVideo = listBoxVideos.SelectedItem as GameVideo;
                    if (selectedVideo == null)
                        return;

                    // Create video player - use Invoke only if handle is created (for thread safety)
                    // Otherwise call directly (tests or same-thread scenarios)
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(() =>
                        {
                            _currentPlayer = _videoPlayerFactory();
                            _currentPlayer.PlayerClosed += (s, e) => _currentPlayer = null;
                            _ = _currentPlayer.Play(selectedVideo);
                        });
                    }
                    else
                    {
                        _currentPlayer = _videoPlayerFactory();
                        _currentPlayer.PlayerClosed += (s, e) => _currentPlayer = null;
                        _ = _currentPlayer.Play(selectedVideo);
                    }
                    break;
                case GamepadButtonFlags.B:
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
        }

        private void listBoxVideos_KeyUp(object sender, KeyEventArgs e)
        {
            _log.Info($"Key up event: {e.KeyCode}");

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
