using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboTanks.Battle
{
    public class TestAI : IAI
    {
        public int Time { get; set; }

        private MovingSubsystemInterface _movingInterface;

        public void Initialize(MovingSubsystemInterface movingInterface)
        {
            _movingInterface = movingInterface;
        }

        public IEnumerator Cycle()
        {
            int ticks = 0;
            while (true)
            {
                ticks = _movingInterface.RotateRight();
                for (int i = 0; i < ticks; i++) yield return null;
                ticks = _movingInterface.MoveForward();
                for (int i = 0; i < ticks; i++) yield return null;
            }
        }
    }
}
