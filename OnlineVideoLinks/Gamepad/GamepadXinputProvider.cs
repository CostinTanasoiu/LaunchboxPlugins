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
        Timer timer;

        public event EventHandler<XInputEventArgs> ButtonPressed;

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
            timer = new Timer(new TimerCallback(TimerTick), null, 0, 100);
        }

        /// <summary>
        /// Stops listening to gamepad events.
        /// </summary>
        public void StopListening()
        {
            timer.Dispose();
        }

        private void TimerTick(object timerState)
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
                                var btnValue = (int)state.Gamepad.Buttons;
                                _log.Info($"XInput pressed button '{state.Gamepad.Buttons}'");

                                ButtonPressed?.Invoke(this, new XInputEventArgs(state.Gamepad.Buttons));
                            }
                        }

                        _previousStates[controller.UserIndex] = state;
                    }
                }
            }
            catch (Exception ex)
            {
                timer.Dispose();
                _log.Error("XInput tick error", ex);
            }
        }
    }
}
