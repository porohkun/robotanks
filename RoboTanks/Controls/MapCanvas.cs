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
        public double DrawWidth { get { return ImageSize * _map.Width; } }
        public double DrawHeight { get { return ImageSize * _map.Height; } }

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
            for (int i = 0; i < Map.TanksPos.Length; i++)
            {
                double x = Map.TanksPos[i].X;
                double y = Map.TanksPos[i].Y;
                var transform = new RotateTransform(((int)Map.TanksDir[i]) * 90, (x + 0.5d) * ImageSize, (y + 0.5d) * ImageSize);
                dc.PushTransform(transform);
                dc.DrawImage(Tanks[i], new Rect(x * ImageSize, y * ImageSize, ImageSize, ImageSize));
                //DrawImage(dc, Tanks[i], Map.TanksPos[i].X, Map.TanksPos[i].Y);
                dc.Pop();
            }
        }

        private void DrawImage(DrawingContext dc, ImageSource image, int x, int y)
        {
            dc.DrawImage(image, new Rect(x * ImageSize, y * ImageSize, ImageSize, ImageSize));
        }

        internal void Draw(Type brush, int brushId, Point point)
        {
            int x = (int)(point.X / ImageSize);
            int y = (int)(point.Y / ImageSize);
            var cell = Map[x, y];
            if (cell == null) return;
            switch (brush.Name)
            {
                case "SurfaceType": cell.Surface = (Battle.SurfaceType)brushId; break;
                case "BarrierType": cell.Barrier = (Battle.BarrierType)brushId; break;
                case "Int32": Map.TanksPos[brushId] = new Battle.Point(x, y); break;
                default: return;
            }
            InvalidateVisual();
        }
    }
}
