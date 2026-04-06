using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoPlayer.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VideoPlayer.Forms
{
    public partial class VideoPlayerFormVLC : Form, IVideoPlayerPanel
    {
        const string TempVideoPath = "temp_video.mp4";
        GameVideo? _gameVideo;

        LibVLC _libVLC;
        MediaPlayer _mediaPlayer;

        public VideoPlayerFormVLC()
        {
            InitializeComponent();

            // Removing the border to make the form look full-screen
            this.FormBorderStyle = FormBorderStyle.None;

            if (!DesignMode)
                LibVLCSharp.Shared.Core.Initialize();

            var libVLCOptions = new[]
            {
                "--aout=directsound" // Set audio output module to directsound
            };


            _libVLC = new LibVLC(enableDebugLogs: true, libVLCOptions);
            _libVLC.SetLogFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs\\libvlc.log"));

            _mediaPlayer = new MediaPlayer(_libVLC);
            vlcView.MediaPlayer = _mediaPlayer;
        }

        public bool IsPlaying()
        {
            return false;
        }

        public async Task Play(GameVideo video)
        {
            // TODO
            this.Show();

            //mediaPlayer.URL = video.VideoPath;
            ////mediaPlayer.URL = @"C:\Work_Personal\LaunchboxPlugins\Resources\BigBuckBunny_640x360.mp4";
            //mediaPlayer.settings.volume = 50; // Set volume to 50%
            //mediaPlayer.settings.mute = false; // Ensure the player is not muted
            //mediaPlayer.Ctlcontrols.play();
            //mediaPlayer.stretchToFit = true;

            _gameVideo = video;

            //progressBar.Visibility = Visibility.Visible;

            //if (VideoMetadataUtilities.IsYoutubeUrl(_gameVideo.VideoPath))
            //{
            //    //await LoadYoutubeVideo();
            //}
            //else
            await LoadRegularVideo();
        }

        public void PlayPause()
        {
            throw new NotImplementedException();
        }

        public void SkipBackward()
        {
            throw new NotImplementedException();
        }

        public void SkipForward()
        {
            throw new NotImplementedException();
        }

        public void StopPlaying()
        {

        }

        private async void VideoPlayerFormVLC_Load(object sender, EventArgs e)
        {

        }

        private async Task LoadRegularVideo()
        {
            var media = new Media(_libVLC, new Uri(_gameVideo.VideoPath));

            await media.Parse(MediaParseOptions.ParseNetwork);
            var subitem = media.SubItems.FirstOrDefault();

            //progressBar.Visibility = Visibility.Collapsed;

            if (subitem != null)
                _mediaPlayer.Play(subitem);
            else
                _mediaPlayer.Play(media);

            _mediaPlayer.Volume = 50;
            _mediaPlayer.Mute = false;
        }

        //private async Task LoadYoutubeVideo()
        //{
        //    await YoutubeDownloader.DownloadTimestampedVideoMP4(_gameVideo.VideoPath, TempVideoPath, _gameVideo.StartTime, _gameVideo.StopTime);

        //    var media = new Media(_libVLC, TempVideoPath, FromType.FromPath);

        //    //progressBar.Visibility = Visibility.Collapsed;
        //    vlcView.MediaPlayer.Play(media);
        //}
    }
}
