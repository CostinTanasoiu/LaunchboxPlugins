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
        Bitmap _canvas;
        Image? _imgMain;
        Image? _imgBadge;

        public PlayerButtonWithBadge()
        {
            InitializeComponent();

            // Doing this to fix transparency issue
            //pictureBoxMain.Controls.Add(pictureBoxBadge);

            _canvas = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppPArgb);
            pictureBoxCanvas.Image = _canvas;

            pictureBoxCanvas.Click += PictureBoxCanvas_Click;
        }

        private void PictureBoxCanvas_Click(object? sender, EventArgs e)
        {
            base.OnClick(e);
        }

        private void DrawCanvas()
        {
            using (var gr = Graphics.FromImage(_canvas))
            {
                gr.Clear(Color.Transparent); // Clear the graphic element to transparent, it was black.

                if (_imgMain != null)
                    // Resize and draw the main image to fit within a 64x64 area at the bottom left corner
                    gr.DrawImage(_imgMain, new Rectangle(0, this.Height - 64, 64, 64));

                if (_imgBadge != null)
                    // Resize and draw the badge image to fit within a 32x32 area at the top-right corner
                    gr.DrawImage(_imgBadge, new Rectangle(this.Width - 32, 0, 32, 32));
            }
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
