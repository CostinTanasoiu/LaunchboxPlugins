using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineVideoLinks.UserControls
{
    public partial class TimeTextBoxCtrl : UserControl
    {
        public override string Text
        {
            get { return txtTimeBox.Text; }
            set { txtTimeBox.Text = Text; }
        }

        public TimeTextBoxCtrl()
        {
            InitializeComponent();
        }

        public int GetSeconds()
        {
            if (string.IsNullOrEmpty(txtTimeBox.Text))
                return 0;

            if (txtTimeBox.Text.Contains(':'))
            {
                var values = txtTimeBox.Text.Split(':');
                return int.Parse(values[0]) * 60 + int.Parse(values[1]);
            }
            else return int.Parse(txtTimeBox.Text);
        }

        public void ClearValue()
        {
            txtTimeBox.Text = "";
        }

        private void txtTimeBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ':'))
                e.Handled = true;

            // only allow one separator
            if ((e.KeyChar == ':') && ((sender as TextBox).Text.IndexOf(':') > -1))
                e.Handled = true;
        }
    }
}
