using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboTanks.Battle
{
   public interface IAI
    {
        int Time { set; }
        IEnumerator Cycle();
        void Initialize(MovingSubsystemInterface movingInterface);
    }
}
