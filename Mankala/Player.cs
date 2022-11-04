using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    public class Player
    {
        public ConsoleColor cColor;
        public int Score;
        public bool IsAi;

        public Player(ConsoleColor _cColor, int _score, bool _isAi)
        {
            cColor = _cColor;
            Score = _score;
            IsAi = _isAi;
        }
    }
}
