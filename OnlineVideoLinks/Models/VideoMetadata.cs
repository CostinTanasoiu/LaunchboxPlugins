using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineVideoLinks.Models
{
    public class VideoMetadata
    {
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public string Url { get; set; }
        public string Thumbnail { get; set; }

        /// <summary>
        /// Start time, in seconds. 0 value ignored.
        /// </summary>
        public int StartTime { get; set; }
    }
}
