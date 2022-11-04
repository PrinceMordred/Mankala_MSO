using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    internal class MankalaRuleset : IRuleset
    {
        public int numHolesPerPlayer() => 6;
        public int numStartStones()    => 4;
        public bool CheckValidMove(Board board, int clickedHole)
        {
            return board.HolesP1()[clickedHole] == null;
        }

        public bool CheckWin(Board state)
        {
            IEnumerable<byte> s = state.HolesP1();
            foreach (byte b in s)
                if (b != 0)
                    return false;
            return true;
        }
        public Player NextPlayer(Player p, IBoardState state)
        {
            return p;
        }
    }
}
