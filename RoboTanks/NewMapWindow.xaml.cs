using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RoboTanks
{
    /// <summary>
    /// Interaction logic for NewMapWindow.xaml
    /// </summary>
    public partial class NewMapWindow : Window
    {
        public NewMapWindow()
        {
            InitializeComponent();
        }

        #region Bindings

        public static readonly DependencyProperty MapWidthProperty =
          DependencyProperty.Register("MapWidth", typeof(int), typeof(NewMapWindow), new UIPropertyMetadata(10));

        public static readonly DependencyProperty MapHeightProperty =
          DependencyProperty.Register("MapHeight", typeof(int), typeof(NewMapWindow), new UIPropertyMetadata(10));

        public static readonly DependencyProperty TanksCountProperty =
          DependencyProperty.Register("TanksCount", typeof(int), typeof(NewMapWindow), new UIPropertyMetadata(4));

        public int MapWidth
        {
            get { return (int)this.GetValue(MapWidthProperty); }
            set
            {
                var val = (int)value;
                if (val < 5)
                    val = 5;
                if (val > 32)
                    val = 32;
                this.SetValue(MapWidthProperty, val);
            }
        }

        public int MapHeight
        {
            get { return (int)this.GetValue(MapHeightProperty); }
            set
            {
                var val = (int)value;
                if (val < 5)
                    val = 5;
                if (val > 32)
                    val = 32;
                this.SetValue(MapHeightProperty, val);
            }
        }

        public int TanksCount
        {
            get { return (int)this.GetValue(TanksCountProperty); }
            set
            {
                var val = (int)value;
                if (val < 2)
                    val = 2;
                if (val > 4)
                    val = 4;
                this.SetValue(TanksCountProperty, val);
            }
        }

        #endregion

        bool done = false;

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            done = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = done;
        }
    }
}
