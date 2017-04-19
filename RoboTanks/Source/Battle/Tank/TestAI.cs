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

        private MovingSubsystem _movingSubsystem;

        public void Initialize(MovingSubsystem movingInterface)
        {
            _movingSubsystem = movingInterface;
        }

        public IEnumerator Cycle()
        {
            int ticks = 0;
            while (true)
            {
                _movingSubsystem.RotateRight(out ticks);
                for (int i = 0; i < ticks; i++) yield return null;
                _movingSubsystem.MoveForward(out ticks);
                for (int i = 0; i < ticks; i++) yield return null;
                _movingSubsystem.MoveForward(out ticks);
                for (int i = 0; i < ticks; i++) yield return null;
                _movingSubsystem.MoveForward(out ticks);
                for (int i = 0; i < ticks; i++) yield return null;
            }
        }
    }
}
