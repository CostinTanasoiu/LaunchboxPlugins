using log4net;
using OnlineVideoLinks.Models;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineVideoLinks.Gamepad
{
    public interface IGamepadXinputProvider
    {
        bool IsGamepadConnected { get; }

        event EventHandler<XInputEventArgs> ButtonPressed;

        void StartListening();
        void StopListening();
    }

    public class GamepadXinputProvider : IGamepadXinputProvider
    {
        ILog _log = LogManager.GetLogger(nameof(GamepadXinputProvider));
        Controller[] _controllers;
        Dictionary<UserIndex, State> _previousStates = new Dictionary<UserIndex, State>();
        Timer? _timer;
        private readonly object _timerLock = new object();

        public event EventHandler<XInputEventArgs>? ButtonPressed;

        public bool IsGamepadConnected
        {
            get
            {
                return _controllers.Any(x => x.IsConnected);
            }
        }

        public GamepadXinputProvider()
        {
            // Initialize XInput
            _controllers = new[] {
                new Controller(UserIndex.One),
                new Controller(UserIndex.Two),
                new Controller(UserIndex.Three),
                new Controller(UserIndex.Four) };

            _log.Info($"Found {_controllers.Count(x => x.IsConnected)} XInput controllers.");
        }

        /// <summary>
        /// Starts listening to gamepad events.
        /// </summary>
        public void StartListening()
        {
            lock (_timerLock)
            {
                // Dispose existing timer if any, then create a new one
                _timer?.Dispose();
                _timer = new Timer(new TimerCallback(TimerTick), null, 0, 100);
                _log.Info("Gamepad listening started.");
            }
        }

        /// <summary>
        /// Stops listening to gamepad events.
        /// </summary>
        public void StopListening()
        {
            lock (_timerLock)
            {
                _timer?.Dispose();
                _timer = null;
                _log.Info("Gamepad listening stopped.");
            }
        }

        private void TimerTick(object? timerState)
        {
            try
            {
                foreach (var controller in _controllers)
                {
                    if (controller.IsConnected)
                    {
                        var state = controller.GetState();
                        if (_previousStates.ContainsKey(controller.UserIndex))
                        {
                            var previousState = _previousStates[controller.UserIndex];
                            if (previousState.PacketNumber != state.PacketNumber)
                            {
                                // Edge detection: only fire for buttons that are newly pressed
                                var previousButtons = previousState.Gamepad.Buttons;
                                var currentButtons = state.Gamepad.Buttons;
                                var newlyPressed = currentButtons & ~previousButtons;

                                if (newlyPressed != GamepadButtonFlags.None)
                                {
                                    _log.Info($"XInput pressed button '{newlyPressed}'");
                                    ButtonPressed?.Invoke(this, new XInputEventArgs(newlyPressed));
                                }
                            }
                        }

                        _previousStates[controller.UserIndex] = state;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error but don't stop the timer - allow recovery
                _log.Error("XInput tick error", ex);
            }
        }
    }
}
