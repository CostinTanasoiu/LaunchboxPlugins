using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVideoLinks.Models
{
    public class SearchModel
    {
        public string ChannelName { get; set; }
        public string ExtraQuery { get; set; }
        public string[] GameNames { get; set; }
        public bool StrictSearch { get; set; }
        public int MaxNrOfItemsPerGame { get; set; }
    }
}
