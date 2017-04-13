using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RoboTanks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Title = "RoboTanks" +
#if DEBUG
                " [DEBUG] " +
#endif
                "v." + Settings.CurrentVersion.ToString();
            
            if (!System.IO.Directory.Exists(Settings.TanksPath))
                System.IO.Directory.CreateDirectory(Settings.TanksPath);
            if (!System.IO.Directory.Exists(Settings.MapsPath))
                System.IO.Directory.CreateDirectory(Settings.MapsPath);
        }

        private void MapEditor_Click(object sender, RoutedEventArgs e)
        {
            var window = new MapEditorWindow();
            window.Show();
        }

        private void StartBattle_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void OpenTanksDir_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Settings.TanksPath);
        }

    }
}
