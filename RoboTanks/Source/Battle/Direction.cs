using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboTanks.Battle
{
    public enum Direction : int
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }

    public static class DirectionExtension
    {
        public static Direction RotateLeft(this Direction dir)
        {
            int d = (int)dir;
            d--;
            if (d < 0) d += 4;
            return (Direction)d;
        }

        public static Direction RotateRight(this Direction dir)
        {
            int d = (int)dir;
            d++;
            if (d >= 4) d -= 4;
            return (Direction)d;
        }
    }
}
