using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OnlineVideoLinks.Forms.UserControls
{
    /// <summary>
    /// A Windows Forms control that displays animated GIF images.
    /// </summary>
    public class LoadingAnimationControl : UserControl
    {
        private readonly PictureBox _pictureBox;
        private Image? _animationImage;
        private MemoryStream? _imageStream; // Must keep stream alive for animated GIFs

        public LoadingAnimationControl()
        {
            _pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            Controls.Add(_pictureBox);
        }

        /// <summary>
        /// Loads an animation from a stream (e.g., embedded resource).
        /// </summary>
        public bool LoadFromStream(Stream stream)
        {
            try
            {
                // Clean up previous resources
                CleanupImage();

                // Copy to a MemoryStream that we keep alive
                _imageStream = new MemoryStream();
                stream.CopyTo(_imageStream);
                _imageStream.Position = 0;

                _animationImage = Image.FromStream(_imageStream);
                _pictureBox.Image = _animationImage;
                return true;
            }
            catch
            {
                CleanupImage();
                return false;
            }
        }

        /// <summary>
        /// Loads an animation from an embedded resource.
        /// </summary>
        public bool LoadFromEmbeddedResource(string resourceName, System.Reflection.Assembly? assembly = null)
        {
            assembly ??= typeof(LoadingAnimationControl).Assembly;
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                System.Diagnostics.Debug.WriteLine($"LoadingAnimationControl: Resource '{resourceName}' not found.");
                System.Diagnostics.Debug.WriteLine($"Available resources: {string.Join(", ", assembly.GetManifestResourceNames())}");
                return false;
            }

            return LoadFromStream(stream);
        }

        /// <summary>
        /// Starts playing the animation (GIFs animate automatically when visible).
        /// </summary>
        public void StartAnimation()
        {
            // GIFs animate automatically in PictureBox when the control is visible
            // ImageAnimator handles this internally
            if (_animationImage != null && ImageAnimator.CanAnimate(_animationImage))
            {
                ImageAnimator.Animate(_animationImage, OnFrameChanged);
            }
        }

        /// <summary>
        /// Stops the animation.
        /// </summary>
        public void StopAnimation()
        {
            if (_animationImage != null)
            {
                ImageAnimator.StopAnimate(_animationImage, OnFrameChanged);
            }
        }

        private void OnFrameChanged(object? sender, System.EventArgs e)
        {
            // Invalidate the PictureBox to redraw with the new frame
            // Use Invoke to ensure we're on the UI thread
            if (!IsDisposed && !_pictureBox.IsDisposed)
            {
                try
                {
                    if (_pictureBox.InvokeRequired)
                    {
                        _pictureBox.BeginInvoke(() => _pictureBox.Invalidate());
                    }
                    else
                    {
                        _pictureBox.Invalidate();
                    }
                }
                catch (ObjectDisposedException)
                {
                    // Control was disposed, ignore
                }
            }
        }

        private void CleanupImage()
        {
            _pictureBox.Image = null;
            _animationImage?.Dispose();
            _animationImage = null;
            _imageStream?.Dispose();
            _imageStream = null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                StopAnimation();
                CleanupImage();
                _pictureBox?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
