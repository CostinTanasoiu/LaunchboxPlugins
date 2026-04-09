using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineVideoLinks.Models;

namespace OnlineVideoLinks.Utilities
{
    public interface IVideoPlayerPanel
    {
        /// <summary>
        /// Checks whether the player is visible.
        /// </summary>
        public bool IsVisible { get; }

        /// <summary>
        /// Checks whether this video is currently playing.
        /// </summary>
        public bool IsPlaying { get; }

        /// <summary>
        /// Plays this video.
        /// </summary>
        public Task Play(GameVideo video);

        /// <summary>
        /// Stops playing the video.
        /// </summary>
        public void StopPlaying();

        /// <summary>
        /// Skips 10 seconds forward.
        /// </summary>
        public void SkipForward();

        /// <summary>
        /// Skips 10 seconds backward.
        /// </summary>
        public void SkipBackward();

        /// <summary>
        /// Toggles the play/pause function.
        /// </summary>
        public void PlayPause();

        /// <summary>
        /// Sends gamepad input.
        /// </summary>
        /// <param name="button"></param>
        void SendGamepadInput(GamepadButtonFlags button);
    }
}
