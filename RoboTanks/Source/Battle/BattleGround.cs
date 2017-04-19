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

        public int Time { get; private set; }

        public BattleGround(Map map, IAI[] ais)
        {
            _map = map;
            _tanks = new Tank[ais.Length];
            for (int i = 0; i < _tanks.Length; i++)
            {
                _tanks[i] = new Tank(ais[i], (Point)_map.TanksPos[i], Direction.North);
                _tanks[i].CellFree = CellFree;
            }
        }

        private bool CellFree(Point pos)
        {
            var cell = _map[pos];
            if (cell == null) return false;
            var free = cell.Barrier == BarrierType.None && cell.Surface != SurfaceType.Water;
            for (int i = 0; i < _tanks.Length; i++)
                free = free && _tanks[i].TankPos != pos;
            return free;
        }

        public void Update()
        {
            Time++;
            for (int i = 0; i < _tanks.Length; i++)
                _tanks[i].Update(Time);
            for (int i = 0; i < _tanks.Length; i++)
                _tanks[i].AiUpdate(Time);
            for (int i = 0; i < _tanks.Length; i++)
            {
                _map.TanksPos[i] = _tanks[i].TankPos;
                _map.TanksDir[i] = _tanks[i].TankDir;
            }
        }
    }
}
