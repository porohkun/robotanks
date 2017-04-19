using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboTanks.Battle
{
    public struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        #region Методы

        public Point OffsetByDirection(Direction direction, bool forward)
        {
            return new Point(
                X + (direction == (forward ? Direction.East : Direction.West) ? 1 : 0) - (direction == (forward ? Direction.West : Direction.East) ? 1 : 0),
                Y + (direction == (forward ? Direction.South : Direction.North) ? 1 : 0) - (direction == (forward ? Direction.North : Direction.South) ? 1 : 0)
                );
        }

        public static double Distance(Point P1, Point P2)
        {
            return P1.DistanceTo(P2);
        }

        public double DistanceTo(Point P)
        {
            return Math.Sqrt((this.X - P.X) * (this.X - P.X) + (this.Y - P.Y) * (this.Y - P.Y));
        }

        #endregion

        #region Переназначение операторов

        public static bool operator ==(Point p1, Point p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y;
        }

        public static bool operator !=(Point p1, Point p2)
        {
            return p1.X != p2.X || p1.Y != p2.Y;
        }

        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        #endregion

        #region Object

        public override bool Equals(object Point)
        {
            return this == (Point)Point;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("[{0}; {1}]", X, Y);
        }

        #endregion
    }
}
