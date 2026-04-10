using log4net;
using OnlineVideoLinks.Gamepad;
using OnlineVideoLinks.Models;
using OnlineVideoLinks.Utilities;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace OnlineVideoLinks.WPF
{
    /// <summary>
    /// Interaction logic for VideoSelectorWindow.xaml
    /// </summary>
    public partial class VideoSelectorWindow : Window
    {
        ILog _log = LogManager.GetLogger(nameof(Forms.VideoSelectorForm));
        IGame _game;
        IGameVideoUtility _gameVideoUtilities;
        Func<IVideoPlayer> _videoPlayerFactory;

        //GamepadDinputProvider _gamepadDinputProvider;
        IGamepadXinputProvider _gamepadXinputProvider;

        // Current video player instance (created fresh for each video)
        private IVideoPlayer _currentPlayer;

        public VideoSelectorWindow(IGame game,
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
            listBoxVideos.ItemsSource = customVideos;
            listBoxVideos.SelectedIndex = 0;
            listBoxVideos.Focus();

            listBoxVideos.SelectionChanged += (s, e) =>
                listBoxVideos.ScrollIntoView(listBoxVideos.SelectedItem);

            _gamepadXinputProvider.ButtonPressed += _gamepadXinputProvider_ButtonPressed;
            _gamepadXinputProvider.StartListening();

            if (PluginHelper.StateManager?.IsBigBox == true)
                Cursor = Cursors.None;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            listBoxVideos.Height = GetItemHeight(listBoxVideos) * 3;
        }

        private double GetItemHeight(ListBox myListBox)
        {
            double itemHeight = 0.0;

            if (myListBox.ItemContainerGenerator != null)
            {
                ListBoxItem item = myListBox.ItemContainerGenerator.ContainerFromIndex(0) as ListBoxItem;
                if (item != null)
                {
                    itemHeight = item.ActualHeight + item.Margin.Top + item.Margin.Bottom;
                }
            }

            return itemHeight;
        }

        private void _gamepadXinputProvider_ButtonPressed(object sender, Models.XInputEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                HandleXInput_ButtonPressed(e.ButtonPressed);
            }));
        }

        private void HandleXInput_ButtonPressed(GamepadButtonFlags buttonPressed)
        {
            if (buttonPressed == GamepadButtonFlags.None)
                return;

            // If a player exists and is playing, forward input to it
            if (_currentPlayer != null && _currentPlayer.IsPlaying)
            {
                _currentPlayer.SendGamepadInput(buttonPressed);
                return;
            }

            switch (buttonPressed)
            {
                case GamepadButtonFlags.A:
                    var selectedVideo = listBoxVideos.SelectedItem as GameVideo;
                    // Create a new player instance for this video
                    _currentPlayer = _videoPlayerFactory();
                    _currentPlayer.PlayerClosed += (s, e) => _currentPlayer = null;
                    _currentPlayer.Play(selectedVideo);
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
            // Translating keys to gamepad buttons.
            if (e.Key == Key.Enter)
                HandleXInput_ButtonPressed(GamepadButtonFlags.A);
            else if (e.Key == Key.Escape)
                HandleXInput_ButtonPressed(GamepadButtonFlags.B);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (PluginHelper.StateManager?.IsBigBox == true)
                Cursor = Cursors.Arrow;

            //_gamepadDinputProvider.TurnOff();
            _gamepadXinputProvider.StopListening();
        }
    }
}
