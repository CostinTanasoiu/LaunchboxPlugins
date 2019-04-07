using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OnlineVideoLinks.Models;
using System.Diagnostics;

namespace OnlineVideoLinks.UserControls
{
    public partial class YoutubeSearchResult : UserControl
    {
        private YoutubeVideoInfo _videoInfo;
        public event EventHandler DeleteClicked;

        public YoutubeSearchResult(YoutubeVideoInfo videoInfo)
        {
            InitializeComponent();

            _videoInfo = videoInfo;
            pictureBox1.Load(videoInfo.Thumbnail);
            lblTitle.Text = videoInfo.Title;
            lblSubtitle.Text = $"Channel: {videoInfo.ChannelName} • Author: {videoInfo.Author} • {videoInfo.ViewCount} views • Posted: {videoInfo.PostedWhen} • Duration: {videoInfo.Duration}";
        }

        private void YoutubeSearchResult_Load(object sender, EventArgs e)
        {

        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            var gameVideo = new GameVideo
            {
                Title = _videoInfo.Title,
                VideoPath = _videoInfo.Url
            };

            var vlcExecutable = Utilities.GetVlcExecutablePath();
            var cmdArgs = gameVideo.GetVlcCmdArguments();
            Process.Start(vlcExecutable, cmdArgs);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            OnDeleteClicked(new EventArgs());
        }

        protected virtual void OnDeleteClicked(EventArgs e)
        {
            EventHandler handler = DeleteClicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
