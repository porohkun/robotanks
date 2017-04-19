using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboTanks.Battle
{
    public class Map
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Point[] TanksPos { get; private set; }
        public Direction[] TanksDir { get; private set; }

        public Cell this[Point pos] { get { return this[pos.X, pos.Y]; } }

        public Cell this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= Width || y < 0 || y >= Height)
                    return null;
                return _cells[x, y];
            }
        }

        private Cell[,] _cells;

        public Map(int width, int height)
        {
            TanksPos = new Point[]
            {
                new Point(0, 0),
                new Point(0, height - 1),
                new Point(width - 1, 0),
                new Point(width - 1, height - 1)
            };
            TanksDir = new Direction[]
            {
                 Direction.North,
                 Direction.West,
                 Direction.South,
                 Direction.East
            };
            Width = width;
            Height = height;
            _cells = new Cell[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    _cells[x, y] = new Cell();
        }

        public Map(Stream stream)
        {
            using (var br = new BinaryReader(stream))
            {
                var version = br.ReadInt32();
                switch (version)
                {
                    case 1: DeserializeVer1(br); break;
                }
            }
        }

        private void DeserializeVer1(BinaryReader br)
        {
            Width = br.ReadInt32();
            Height = br.ReadInt32();
            TanksPos = new Point[br.ReadInt32()];
            for (int i = 0; i < TanksPos.Length; i++)
                TanksPos[i] = new Point(br.ReadInt32(), br.ReadInt32());
            _cells = new Cell[Width, Height];
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    _cells[x, y] = new Cell((SurfaceType)br.ReadInt32(), (BarrierType)br.ReadInt32());
        }

        internal void SaveTo(Stream stream)
        {
            using (var bw = new BinaryWriter(stream))
            {
                bw.Write(1);
                bw.Write(Width);
                bw.Write(Height);
                bw.Write(TanksPos.Length);
                for (int i = 0; i < TanksPos.Length; i++)
                {
                    bw.Write(TanksPos[i].X);
                    bw.Write(TanksPos[i].Y);
                }
                for (int x = 0; x < Width; x++)
                    for (int y = 0; y < Height; y++)
                    {
                        bw.Write((int)_cells[x, y].Surface);
                        bw.Write((int)_cells[x, y].Barrier);
                    }
            }
        }
    }
}
