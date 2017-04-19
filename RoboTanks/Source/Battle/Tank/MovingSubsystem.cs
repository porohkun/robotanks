using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboTanks.Battle
{
    public class MovingSubsystem
    {
        private int _tickLag = 0;
        public int TickLag { get { return _tickLag; } private set { _tickLag = value >= 0 ? value : 0; } }

        internal Func<bool> OnRotateRight;
        internal Func<bool> OnRotateLeft;
        internal Func<bool> OnMoveForward;
        internal Func<bool> OnMoveBackward;

        private bool MakeMovingAction(Func<bool> action, int tickLag)
        {
            if (TickLag > 0) return false;
            bool result = false;
            if (action != null)
                result = action();
            if (result)
                TickLag += tickLag;
            return result;
        }

        public bool RotateRight()
        {
            return MakeMovingAction(OnRotateRight, Configuration.RotationTime);
        }

        public bool RotateLeft()
        {
            return MakeMovingAction(OnRotateLeft, Configuration.RotationTime);
        }

        public bool MoveForward()
        {
            return MakeMovingAction(OnMoveForward, Configuration.MovingForwardTime);
        }

        public bool MoveBackward()
        {
            return MakeMovingAction(OnMoveBackward, Configuration.MovingBackwardTime);
        }

        public bool RotateRight(out int lag)
        {
            var result = RotateRight();
            lag = _tickLag;
            return result;
        }

        public bool RotateLeft(out int lag)
        {
            var result = RotateLeft();
            lag = _tickLag;
            return result;
        }

        public bool MoveForward(out int lag)
        {
            var result = MoveForward();
            lag = _tickLag;
            return result;
        }

        public bool MoveBackward(out int lag)
        {
            var result = MoveBackward();
            lag = _tickLag;
            return result;
        }

        internal void Update()
        {
            TickLag -= 1;
        }
    }
}
