using log4net;
using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OnlineVideoLinks.Gamepad
{
    public class GamepadDinputProvider
    {
        ILog _log = LogManager.GetLogger(nameof(GamepadDinputProvider));
        DirectInput _directInput;

        List<Joystick> _acquiredJoysticks = new List<Joystick>();
        Timer timer;

        public GamepadDinputProvider()
        {
            //var xInput = new XInput
            _directInput = new DirectInput();
            var gamepads = _directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AttachedOnly);
            var joysticks = _directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AttachedOnly);

            var allPads = new List<DeviceInstance>();
            allPads.AddRange(gamepads);
            allPads.AddRange(joysticks);

            _log.Info($"Found {allPads.Count} DirectInput devices.");

            foreach (var device in allPads)
                PollController(device);

            timer = new Timer(new TimerCallback(TimerTick), null, 0, 100);
        }

        public void TurnOff()
        {
            timer.Dispose();
        }

        private void PollController(DeviceInstance deviceInstance)
        {
            var joystick = new Joystick(_directInput, deviceInstance.InstanceGuid);

            // Set BufferSize in order to use buffered data.
            joystick.Properties.BufferSize = 128;

            // Acquire the joystick
            joystick.Acquire();

            _acquiredJoysticks.Add(joystick);


        }

        private void TimerTick(object timerState)
        {
            try
            {
                foreach (var joystick in _acquiredJoysticks)
                {
                    // Poll events from joystick
                    joystick.Poll();
                    var data = joystick.GetBufferedData();
                    if (data.Length > 0)
                    {
                        var lastState = data.Last();
                        _log.Info($"DInput pressed '{lastState.Offset}'. Raw offset '{lastState.RawOffset}'. Value: '{lastState.Value}'");
                    }
                }
            }
            catch (Exception ex)
            {
                timer.Dispose();
                _log.Error("DInput tick error", ex);
            }
        }
    }
}
