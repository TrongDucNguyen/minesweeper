using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class GameState
    {
        public enum StateType { Unknow, Flag, Open }

        public int X { get; set; }

        public int Y { get; set; }

        public int Value { get; set; }

        public bool IsMine { get; set; }

        public StateType State { get; set; }

        public GameState(int x, int y)
        {
            X = x;
            Y = y;
            IsMine = false;
            State = StateType.Unknow;
        }
    }
}
