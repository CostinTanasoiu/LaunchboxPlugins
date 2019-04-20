using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineVideoLinks
{
    public partial class RtfDescriptionForm : Form
    {
        public RtfDescriptionForm()
        {
            InitializeComponent();
        }

        public void ShowDescription(string text)
        {
            richTextBox1.Text = text;
            this.Show();
        }
    }
}
