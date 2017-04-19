using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

            Refresh();
        }

        private void Refresh()
        {
            mapBox.Items.Clear();
            foreach (var file in Directory.GetFiles(Settings.MapsPath))
                mapBox.Items.Add(Path.GetFileNameWithoutExtension(file));

            tank1Box.Items.Clear();
            tank2Box.Items.Clear();
            tank3Box.Items.Clear();
            tank4Box.Items.Clear();
            tank1Box.Items.Add("");
            tank2Box.Items.Add("");
            tank3Box.Items.Add("");
            tank4Box.Items.Add("");
            foreach (var file in Directory.GetFiles(Settings.TanksPath))
            {
                var ai = Path.GetFileNameWithoutExtension(file);
                tank1Box.Items.Add(ai);
                tank2Box.Items.Add(ai);
                tank3Box.Items.Add(ai);
                tank4Box.Items.Add(ai);
            }

        }

        private void MapEditor_Click(object sender, RoutedEventArgs e)
        {
            var window = new MapEditorWindow();
            window.Show();
        }

        private void StartBattle_Click(object sender, RoutedEventArgs e)
        {
            var window = new BattleWindow((string)mapBox.SelectedItem, new string[]
            {
                (string)tank1Box.SelectedItem,
                (string)tank2Box.SelectedItem,
                (string)tank3Box.SelectedItem,
                (string)tank4Box.SelectedItem
            });
            window.Show();
        }

        private void OpenTanksDir_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Settings.TanksPath);
        }

    }
}
