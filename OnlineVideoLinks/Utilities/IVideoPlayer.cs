using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineVideoLinks.Models;

namespace OnlineVideoLinks.Utilities
{
    /// <summary>
    /// Interface for a self-managing video player.
    /// The player shows itself when Play is called and disposes when closed.
    /// Create a new instance for each video playback session.
    /// </summary>
    public interface IVideoPlayer
    {
        /// <summary>
        /// Raised when the player is closed.
        /// </summary>
        event EventHandler PlayerClosed;

        /// <summary>
        /// Checks whether this video is currently playing.
        /// </summary>
        bool IsPlaying { get; }

        /// <summary>
        /// Plays this video. The player window will show itself.
        /// </summary>
        Task Play(GameVideo video);

        /// <summary>
        /// Stops playing the video and closes the player.
        /// </summary>
        void StopPlaying();

        /// <summary>
        /// Skips forward.
        /// </summary>
        void SkipForward();

        /// <summary>
        /// Skips backward.
        /// </summary>
        void SkipBackward();

        /// <summary>
        /// Toggles the play/pause function.
        /// </summary>
        void PlayPause();

        /// <summary>
        /// Sends gamepad input.
        /// </summary>
        /// <param name="button"></param>
        void SendGamepadInput(GamepadButtonFlags button);
    }
}
