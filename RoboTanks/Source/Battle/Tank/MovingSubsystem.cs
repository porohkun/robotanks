using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboTanks.Battle
{
    public class MovingSubsystemInterface
    {
        internal Func<int> OnRotateRight;
        internal Func<int> OnRotateLeft;
        internal Func<int> OnMoveForward;
        internal Func<int> OnMoveBackward;

        public int RotateRight()
        {
            if (OnRotateRight != null)
                return OnRotateRight();
            else
                return -1;
        }

        public int RotateLeft()
        {
            if (OnRotateLeft != null)
                return OnRotateLeft();
            else
                return -1;
        }

        public int MoveForward()
        {
            if (OnMoveForward != null)
                return OnMoveForward();
            else
                return -1;
        }

        public int MoveBackward()
        {
            if (OnMoveBackward != null)
                return OnMoveBackward();
            else
                return -1;
        }
    }

    public class MovingSubsystem
    {
        public MovingSubsystemInterface Interface { get; private set; }

        public MovingSubsystem()
        {
            Interface = new MovingSubsystemInterface();
            Interface.OnRotateRight = RotateRight;
            Interface.OnRotateLeft = RotateLeft;
            Interface.OnMoveForward = MoveForward;
            Interface.OnMoveBackward = MoveBackward;
        }

        private int RotateRight()
        {
            throw new NotImplementedException();
        }

        private int RotateLeft()
        {
            throw new NotImplementedException();
        }

        private int MoveForward()
        {
            throw new NotImplementedException();
        }

        private int MoveBackward()
        {
            throw new NotImplementedException();
        }

        internal void Update()
        {
            throw new NotImplementedException();
        }
    }
}
