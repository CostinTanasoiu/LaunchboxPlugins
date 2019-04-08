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
    public partial class YoutubeSearchResultCtrl : UserControl
    {
        private YoutubeVideoInfo _videoInfo;
        public event EventHandler DeleteClicked;

        public bool IsSelected
        {
            get { return checkBox1.Checked; }
            set { checkBox1.Checked = value; }
        }
        public YoutubeVideoInfo VideoInfo => _videoInfo;

        public YoutubeSearchResultCtrl(YoutubeVideoInfo videoInfo)
        {
            InitializeComponent();

            _videoInfo = videoInfo;
            pictureBox1.Load(videoInfo.Thumbnail);
            lblTitle.Text = videoInfo.Title;
            lblTitle.Links.Add(0, lblTitle.Text.Length, videoInfo.Url);
            lblSubtitle.Text = $"Channel: {videoInfo.ChannelName} • Author: {videoInfo.Author} • {videoInfo.ViewCount} views • Posted: {videoInfo.PostedWhen} • Duration: {videoInfo.Duration}";
        }

        private void YoutubeSearchResult_Load(object sender, EventArgs e)
        {

        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            var gameVideo = ExtractGameVideo();

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

        public GameVideo ExtractGameVideo()
        {
            return new GameVideo
            {
                GameTitle = _videoInfo.GameSearched,
                Title = string.IsNullOrEmpty(txtAltTitle.Text) ? _videoInfo.Title : txtAltTitle.Text,
                VideoPath = _videoInfo.Url,
                StartTime = txtStartTime.GetSeconds(),
                StopTime = txtStopTime.GetSeconds()
            };
        }

        private void lblTitle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(_videoInfo.Url);
        }
    }
}
