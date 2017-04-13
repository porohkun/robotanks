using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RoboTanks
{
    public class MapCanvas : Canvas
    {
        public int ImageSize { get; set; }
        public ImageSource[] Surfaces { get; set; }
        public ImageSource[] Barriers { get; set; }
        public ImageSource[] Tanks { get; set; }

        private Battle.Map _map = null;
        public Battle.Map Map
        {
            get { return _map; }
            set
            {
                _map = value;
                Width = _map.Width * ImageSize;
                Height = _map.Height * ImageSize;
                InvalidateVisual();
            }
        }

        //public static readonly DependencyProperty MapProperty =
        //  DependencyProperty.Register("Map", typeof(Battle.Map), typeof(MapCanvas), new UIPropertyMetadata(null));

        protected override void OnRender(DrawingContext dc)
        {
            if (Map == null) return;

            for (int x = 0; x < Map.Width; x++)
                for (int y = 0; y < Map.Height; y++)
                {
                    var cell = Map[x, y];
                    DrawImage(dc, Surfaces[(int)cell.Surface], x, y);
                    if (cell.Barrier != Battle.BarrierType.None)
                        DrawImage(dc, Barriers[(int)cell.Barrier], x, y);
                }
            DrawImage(dc, Tanks[0], Map.Tank1Pos.X, Map.Tank1Pos.Y);
            DrawImage(dc, Tanks[1], Map.Tank2Pos.X, Map.Tank2Pos.Y);
            DrawImage(dc, Tanks[2], Map.Tank3Pos.X, Map.Tank3Pos.Y);
            DrawImage(dc, Tanks[3], Map.Tank4Pos.X, Map.Tank4Pos.Y);
        }

        private void DrawImage(DrawingContext dc, ImageSource image, int x, int y)
        {
            dc.DrawImage(image, new Rect(x * ImageSize, y * ImageSize, ImageSize, ImageSize));
        }

        internal void Draw(Type brush, int brushId, Point point)
        {
            int x = (int)(point.X / ImageSize);
            int y = (int)(point.Y / ImageSize);
            switch (brush.Name)
            {
                case "SurfaceType": Map[x, y].Surface = (Battle.SurfaceType)brushId; break;
                case "BarrierType": Map[x, y].Barrier = (Battle.BarrierType)brushId; break;
                default: return;
            }
            InvalidateVisual();
        }
    }
}
