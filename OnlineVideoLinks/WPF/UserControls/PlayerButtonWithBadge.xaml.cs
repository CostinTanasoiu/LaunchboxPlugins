using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for PlayerButtonWithBadge.xaml
    /// </summary>
    public partial class PlayerButtonWithBadge : UserControl
    {
        public PlayerButtonWithBadge()
        {
            InitializeComponent();
        }

        public PackIconKind MainIcon
        {
            set
            {
                iconMain.Kind = value;
            }
        }

        public PackIconKind BadgeIcon
        {
            set
            {
                iconBadge.Kind = value;
            }
        }

        public Brush BadgeColor
        {
            set
            {
                iconBadge.Foreground = value;
            }
        }

        public event RoutedEventHandler Click;

        private void theButton_Click(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(this, e);
        }
    }
}
