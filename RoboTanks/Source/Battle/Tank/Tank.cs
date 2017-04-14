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

        public Tank(IAI ai)
        {
            _ai = ai;
            _moving = new MovingSubsystem();
            _ai.Initialize(_moving.Interface);
            _aiEnumerator = _ai.Cycle();
        }

        public void Update(int time)
        {
            _moving.Update();
            _ai.Time = time;
            try
            {
                _aiEnumerator.MoveNext();
            }
            catch { }
        }
    }
}
