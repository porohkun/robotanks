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
    /// Interaction logic for MapEditorWindow.xaml
    /// </summary>
    public partial class MapEditorWindow : Window
    {
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
          DependencyProperty.Register("Zoom", typeof(double), typeof(MapEditorWindow), new UIPropertyMetadata(2d));

        //public static readonly DependencyProperty MapProperty =
        //  DependencyProperty.Register("Map", typeof(Battle.Map), typeof(MapEditorWindow), new UIPropertyMetadata(null));

        public double Zoom
        {
            get { return (double)this.GetValue(ZoomProperty); }
            set { this.SetValue(ZoomProperty, value); }
        }

        //public Battle.Map Map
        //{
        //    get { return (Battle.Map)this.GetValue(MapProperty); }
        //    set { this.SetValue(MapProperty, value); }
        //}

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
            }
        }

        private void CommandBinding_Open(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("open");
        }

        private void CommandBinding_Save(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("open");
        }

        private void CommandBinding_SaveAs(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("open");
        }

        private void CommandBinding_Close(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("open");
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

        private Type _brush;
        private int _brushId;

        private void SetBrush(object obj)
        {
            _brush = obj.GetType();
            _brushId = (int)obj;
        }

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

        private void mapCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                mapCanvas.Draw(_brush, _brushId, Mouse.GetPosition(mapCanvas));
            }
        }

        private void mapCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mapCanvas.Draw(_brush, _brushId, Mouse.GetPosition(mapCanvas));
        }
    }
}
