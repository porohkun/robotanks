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
using Microsoft.Win32;
using System.IO;

namespace RoboTanks
{
    /// <summary>
    /// Interaction logic for MapEditorWindow.xaml
    /// </summary>
    public partial class MapEditorWindow : Window
    {
        private string _filename = null;
        private bool _edited = false;
        private Battle.Map __map = null;
        private Battle.Map _map
        {
            get { return __map; }
            set
            {
                __map = value;
                mapCanvas.Map = value;
            }
        }

        public MapEditorWindow()
        {
            InitializeComponent();
            _map = new Battle.Map(10, 10);
        }

        #region Bindings

        public static readonly DependencyProperty ZoomProperty =
          DependencyProperty.Register("Zoom", typeof(double), typeof(MapEditorWindow), new UIPropertyMetadata(1d));

        public double Zoom
        {
            get { return (double)this.GetValue(ZoomProperty); }
            set { this.SetValue(ZoomProperty, value); }
        }

        #endregion

        #region Commands

        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #region File

        private void CommandBinding_New(object sender, ExecutedRoutedEventArgs e)
        {
            var newMapWin = new NewMapWindow();
            if (newMapWin.ShowDialog().Value)
            {
                _map = new Battle.Map(newMapWin.MapWidth, newMapWin.MapHeight);
                _filename = null;
            }
        }

        private void CommandBinding_Open(object sender, ExecutedRoutedEventArgs e)
        {
            if (CheckEdited())
            {
                var ofd = new OpenFileDialog();
                ofd.InitialDirectory = Settings.MapsPath;
                ofd.Filter = "Map files|*.map|All files|*.*";
                if (ofd.ShowDialog().Value)
                {
                    _map = new Battle.Map(File.OpenRead(ofd.FileName));
                    _filename = ofd.FileName;
                    _edited = false;
                }
            }
        }

        private void CommandBinding_Save(object sender, ExecutedRoutedEventArgs e)
        {
            Save();
        }

        private void CommandBinding_SaveAs(object sender, ExecutedRoutedEventArgs e)
        {
            SaveAs();
        }

        private void CommandBinding_Close(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void EditorWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !CheckEdited();
        }

        private void CommandBinding_ZoomIn(object sender, ExecutedRoutedEventArgs e)
        {
            Zoom = Zoom * 2;
        }

        private void CommandBinding_ZoomOut(object sender, ExecutedRoutedEventArgs e)
        {
            if (Zoom > 1)
                Zoom = Zoom / 2;
        }

        #endregion

        #region Surface

        private void CommandBinding_Dirt(object sender, ExecutedRoutedEventArgs e)
        {
            SetBrush(Battle.SurfaceType.Dirt);
        }

        private void CommandBinding_Grass(object sender, ExecutedRoutedEventArgs e)
        {
            SetBrush(Battle.SurfaceType.Grass);
        }

        private void CommandBinding_Gravel(object sender, ExecutedRoutedEventArgs e)
        {
            SetBrush(Battle.SurfaceType.Gravel);
        }

        private void CommandBinding_Water(object sender, ExecutedRoutedEventArgs e)
        {
            SetBrush(Battle.SurfaceType.Water);
        }

        #endregion

        #region Barrier

        private void CommandBinding_None(object sender, ExecutedRoutedEventArgs e)
        {
            SetBrush(Battle.BarrierType.None);
        }

        private void CommandBinding_Rock(object sender, ExecutedRoutedEventArgs e)
        {
            SetBrush(Battle.BarrierType.Rock);
        }

        private void CommandBinding_Tree(object sender, ExecutedRoutedEventArgs e)
        {
            SetBrush(Battle.BarrierType.Tree);
        }

        private void CommandBinding_Wall(object sender, ExecutedRoutedEventArgs e)
        {
            SetBrush(Battle.BarrierType.Wall);
        }

        #endregion

        #region Tanks

        private void CommandBinding_Tank1(object sender, ExecutedRoutedEventArgs e)
        {
            SetBrush(0);
        }

        private void CommandBinding_Tank2(object sender, ExecutedRoutedEventArgs e)
        {
            SetBrush(1);
        }

        private void CommandBinding_Tank3(object sender, ExecutedRoutedEventArgs e)
        {
            SetBrush(2);
        }

        private void CommandBinding_Tank4(object sender, ExecutedRoutedEventArgs e)
        {
            SetBrush(3);
        }

        #endregion

        #endregion

        private bool CheckEdited()
        {
            if (!_edited) return true;
            var result = MessageBox.Show("Would you like to save changes?", "Map edited", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes: Save(); return true;
                case MessageBoxResult.No: return true;
                default: return false;
            }
        }

        private void Save()
        {
            if (string.IsNullOrEmpty(_filename))
                SaveAs();
            else
            {
                _map.SaveTo(File.OpenWrite(_filename));
                _edited = false;
            }
        }

        private void SaveAs()
        {
            var sfd = new SaveFileDialog();
            sfd.InitialDirectory = Settings.MapsPath;
            sfd.Filter = "Map files|*.map";
            if (sfd.ShowDialog().Value)
            {
                _filename = sfd.FileName;
                Save();
            }
        }

        private Type _brush;
        private int _brushId;

        private void SetBrush(object obj)
        {
            _brush = obj.GetType();
            _brushId = (int)obj;
        }

        private void mapCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                mapCanvas.Draw(_brush, _brushId, Mouse.GetPosition(mapCanvas));
                _edited = true;
            }
        }

        private void mapCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mapCanvas.Draw(_brush, _brushId, Mouse.GetPosition(mapCanvas));
            _edited = true;
        }

        private void ToolBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
            {
                overflowGrid.Visibility = toolBar.HasOverflowItems ? Visibility.Visible : Visibility.Collapsed;
            }

            var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
            if (mainPanelBorder != null)
            {
                var defaultMargin = new Thickness(0, 0, 11, 0);
                mainPanelBorder.Margin = toolBar.HasOverflowItems ? defaultMargin : new Thickness(0);
            }
        }
    }
}
