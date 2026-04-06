using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoPlayer.Forms.UserControls
{
    public partial class PlayerButtonWithBadge : UserControl
    {
        Bitmap? _canvas;
        Image? _imgMain;
        Image? _imgBadge;

        public PlayerButtonWithBadge()
        {
            InitializeComponent();

            pictureBoxCanvas.Click += PictureBoxCanvas_Click;
            this.Resize += PlayerButtonWithBadge_Resize;

            RecreateCanvas();
        }

        private void PlayerButtonWithBadge_Resize(object? sender, EventArgs e)
        {
            RecreateCanvas();
        }

        private void RecreateCanvas()
        {
            if (this.Width <= 0 || this.Height <= 0)
                return;

            _canvas?.Dispose();
            _canvas = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppPArgb);
            pictureBoxCanvas.Image = _canvas;
            DrawCanvas();
        }

        private void PictureBoxCanvas_Click(object? sender, EventArgs e)
        {
            base.OnClick(e);
        }

        private void DrawCanvas()
        {
            if (_canvas == null)
                return;

            // Calculate sizes proportionally based on the control's current size
            // Main icon takes up ~84% of the control height, positioned at bottom-left
            int mainIconSize = (int)(this.Height * 0.84);
            // Badge icon takes up ~42% of the control height, positioned at top-right
            int badgeIconSize = (int)(this.Height * 0.42);

            using (var gr = Graphics.FromImage(_canvas))
            {
                gr.Clear(Color.Transparent);
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                if (_imgMain != null)
                    gr.DrawImage(_imgMain, new Rectangle(0, this.Height - mainIconSize, mainIconSize, mainIconSize));

                if (_imgBadge != null)
                    gr.DrawImage(_imgBadge, new Rectangle(this.Width - badgeIconSize, 0, badgeIconSize, badgeIconSize));
            }

            pictureBoxCanvas.Invalidate();
        }

        [Browsable(true)]
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image? MainIcon
        {
            get => _imgMain;
            set
            {
                _imgMain = value;
                DrawCanvas();
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image? BadgeIcon
        {
            get => _imgBadge;
            set
            {
                _imgBadge = value;
                DrawCanvas();
            }
        }

        //public event RoutedEventHandler Click;
    }
}
