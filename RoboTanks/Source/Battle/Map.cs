using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboTanks.Battle
{
    public class Map
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Point Tank1Pos { get; private set; }
        public Point Tank2Pos { get; private set; }
        public Point Tank3Pos { get; private set; }
        public Point Tank4Pos { get; private set; }

        public Cell this[int x, int y] { get { return _cells[x, y]; } }

        private Cell[,] _cells;
        
        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            _cells = new Cell[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    _cells[x, y] = new Cell();
        }
    }
}
