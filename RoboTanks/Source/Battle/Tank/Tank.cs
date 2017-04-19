using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboTanks.Battle
{
    public class Tank
    {
        private IAI _ai;
        private IEnumerator _aiEnumerator;

        private MovingSubsystem _moving;
        internal Point TankPos;
        internal Direction TankDir;
        private MovingAction _movingAction = MovingAction.None;
        private int _movingLag = 0;

        internal Func<Point, bool> CellFree;

        public Tank(IAI ai, Point tankPos, Direction tankDir)
        {
            TankPos = tankPos;
            TankDir = tankDir;
            _moving = new MovingSubsystem();
            _moving.OnMoveForward = OnMoveForward;
            _moving.OnMoveBackward = OnMoveBackward;
            _moving.OnRotateLeft = OnRotateLeft;
            _moving.OnRotateRight = OnRotateRight;

            _ai = ai;
            _ai.Initialize(_moving);
            _aiEnumerator = _ai.Cycle();
        }

        private bool OnRotateRight()
        {
            if (_movingLag == 0 && _movingAction == MovingAction.None)
            {
                _movingAction = MovingAction.RotateRight;
                _movingLag = Configuration.RotationTime;
                return true;
            }
            else
                return false;
        }

        private bool OnRotateLeft()
        {
            if (_movingLag == 0 && _movingAction == MovingAction.None)
            {
                _movingAction = MovingAction.RotateLeft;
                _movingLag = Configuration.RotationTime;
                return true;
            }
            else
                return false;
        }

        private bool OnMoveForward()
        {
            if (_movingLag == 0 && _movingAction == MovingAction.None)
            {
                _movingAction = MovingAction.MoveForward;
                _movingLag = Configuration.MovingForwardTime;
                return true;
            }
            else
                return false;
        }

        private bool OnMoveBackward()
        {
            if (_movingLag == 0 && _movingAction == MovingAction.None)
            {
                _movingAction = MovingAction.MoveBackward;
                _movingLag = Configuration.MovingBackwardTime;
                return true;
            }
            else
                return false;
        }

        public void Update(int time)
        {
            _moving.Update();
            if (_movingLag > 0)
            {
                _movingLag--;
                if (_movingLag == 0)
                {
                    switch (_movingAction)
                    {
                        case MovingAction.RotateLeft: TankDir = TankDir.RotateLeft(); ; break;
                        case MovingAction.RotateRight: TankDir = TankDir.RotateRight(); break;
                        case MovingAction.MoveForward:
                            {
                                var pos = TankPos.OffsetByDirection(TankDir, true);
                                if (CellFree(pos))
                                    TankPos = pos;
                                break;
                            }
                        case MovingAction.MoveBackward:
                            {
                                var pos = TankPos.OffsetByDirection(TankDir, false);
                                if (CellFree(pos))
                                    TankPos = pos;
                                break;
                            }
                    }
                    _movingAction = MovingAction.None;
                }
            }
        }

        public void AiUpdate(int time)
        {
            try
            {
                _ai.Time = time;
                _aiEnumerator.MoveNext();
            }
            catch { }
        }
    }
}
