using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVideoLinks.Models
{
    public class YoutubeVideoInfo
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ChannelName { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string Url { get; set; }
        public string Thumbnail { get; set; }
        public string ViewCount { get; set; }
        public string PostedWhen { get; set; }
    }
}
