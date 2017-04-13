using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboTanks.Battle
{
    public class Cell
    {
        public SurfaceType Surface { get; set; }
        public BarrierType Barrier { get; set; }
        public int Tank { get; private set; }

        public Cell(SurfaceType surface, BarrierType barrier)
        {
            Surface = surface;
            Barrier = barrier;
        }

        public Cell() : this(SurfaceType.Dirt, BarrierType.None) { }
    }
}
