using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OnlineVideoLinks.WPF.UserControls
{
    /// <summary>
    /// Interaction logic for KeyButtonInstruction.xaml
    /// </summary>
    public partial class KeyButtonInstruction : UserControl
    {
        public string KeyImageSource
        {
            set
            {
                imgKey.Source = new BitmapImage(new Uri(value, UriKind.Relative));
            }
        }
        public string GamepadButtonImageSource
        {
            set
            {
                imgButton.Source = new BitmapImage(new Uri(value, UriKind.Relative));
            }
        }
        public string Text
        {
            set
            {
                txtLabel.Text = value;
            }
        }

        public KeyButtonInstruction()
        {
            InitializeComponent();

        }
    }
}
