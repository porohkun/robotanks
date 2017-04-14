using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboTanks.Battle
{
    public class BattleGround
    {
        private Map _map;
        private Tank[] _tanks;
        private Point[] _tanksPos;
        private Direction[] _tanksDir;

        public int Time { get; private set; }

        public BattleGround(Map map, IAI[] ais)
        {
            _map = map;
            _tanks = new Tank[ais.Length];
            _tanksPos = (Point[])_map.TanksPos.Clone();
            _tanksDir = new Direction[ais.Length];
            for (int i = 0; i < _tanks.Length; i++)
            {
                _tanks[i] = new Tank(ais[i]);
            }
        }

        public void Update()
        {
            Time++;
            for (int i = 0; i < _tanks.Length; i++)
                _tanks[i].Update(Time);
        }
    }
}
