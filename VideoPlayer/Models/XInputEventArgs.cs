using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Text;

namespace VideoPlayer.Models
{
    public class XInputEventArgs : EventArgs
    {
        public GamepadButtonFlags ButtonPressed { get; set; }

        public XInputEventArgs(GamepadButtonFlags buttonFlag)
        {
            ButtonPressed = buttonFlag;
        }
    }
}
