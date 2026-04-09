using SkiaSharp;
using SkiaSharp.Skottie;
using SkiaSharp.Views.Desktop;
using System;
using System.IO;

namespace VideoPlayer.Forms.UserControls
{
    /// <summary>
    /// A Windows Forms control that renders Lottie animations using SkiaSharp.Skottie.
    /// </summary>
    public class LottieAnimationControl : SKControl
    {
        private Animation? _animation;
        private System.Windows.Forms.Timer? _animationTimer;
        private DateTime _startTime;
        private bool _isPlaying;

        public LottieAnimationControl()
        {
            this.DoubleBuffered = true;
        }

        /// <summary>
        /// Loads a Lottie animation from a JSON string.
        /// </summary>
        public bool LoadFromJson(string json)
        {
            StopAnimation();
            _animation?.Dispose();

            using var data = SKData.CreateCopy(System.Text.Encoding.UTF8.GetBytes(json));
            if (Animation.TryCreate(data, out _animation))
            {
                return true;
            }

            _animation = null;
            return false;
        }

        /// <summary>
        /// Loads a Lottie animation from a stream (e.g., embedded resource).
        /// </summary>
        public bool LoadFromStream(Stream stream)
        {
            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();
            return LoadFromJson(json);
        }

        /// <summary>
        /// Loads a Lottie animation from an embedded resource.
        /// </summary>
        public bool LoadFromEmbeddedResource(string resourceName, System.Reflection.Assembly? assembly = null)
        {
            assembly ??= System.Reflection.Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                return false;

            return LoadFromStream(stream);
        }

        /// <summary>
        /// Starts playing the animation in a loop.
        /// </summary>
        public void StartAnimation()
        {
            if (_animation == null || _isPlaying)
                return;

            _startTime = DateTime.Now;
            _isPlaying = true;

            _animationTimer = new System.Windows.Forms.Timer
            {
                Interval = 16 // ~60 FPS
            };
            _animationTimer.Tick += AnimationTimer_Tick;
            _animationTimer.Start();
        }

        /// <summary>
        /// Stops the animation.
        /// </summary>
        public void StopAnimation()
        {
            _isPlaying = false;

            if (_animationTimer != null)
            {
                _animationTimer.Stop();
                _animationTimer.Tick -= AnimationTimer_Tick;
                _animationTimer.Dispose();
                _animationTimer = null;
            }
        }

        private void AnimationTimer_Tick(object? sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Transparent);

            if (_animation == null)
                return;

            // Calculate the current time in the animation (looping)
            var elapsed = DateTime.Now - _startTime;
            var animationDuration = _animation.Duration;
            var currentTime = TimeSpan.FromTicks(elapsed.Ticks % animationDuration.Ticks);

            // Seek to the current frame
            _animation.SeekFrameTime(currentTime);

            // Calculate scaling to fit the control while maintaining aspect ratio
            var animationSize = _animation.Size;
            var controlWidth = e.Info.Width;
            var controlHeight = e.Info.Height;

            var scaleX = controlWidth / animationSize.Width;
            var scaleY = controlHeight / animationSize.Height;
            var scale = Math.Min(scaleX, scaleY);

            // Center the animation
            var scaledWidth = animationSize.Width * scale;
            var scaledHeight = animationSize.Height * scale;
            var offsetX = (controlWidth - scaledWidth) / 2;
            var offsetY = (controlHeight - scaledHeight) / 2;

            canvas.Save();
            canvas.Translate(offsetX, offsetY);
            canvas.Scale(scale);

            // Render the animation
            _animation.Render(canvas, new SKRect(0, 0, animationSize.Width, animationSize.Height));

            canvas.Restore();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                StopAnimation();
                _animation?.Dispose();
                _animation = null;
            }

            base.Dispose(disposing);
        }
    }
}
