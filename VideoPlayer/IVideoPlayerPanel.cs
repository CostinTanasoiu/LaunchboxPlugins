using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPlayer.Models;

namespace VideoPlayer
{
    public interface IVideoPlayerPanel
    {
        /// <summary>
        /// Plays this video.
        /// </summary>
        public Task Play(GameVideo video);

        /// <summary>
        /// Stops playing the video.
        /// </summary>
        public void StopPlaying();

        /// <summary>
        /// Checks whether this video is currently playing.
        /// </summary>
        public bool IsPlaying();

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
    }
}
