using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    public sealed class MankalaRuleset : IRuleset
    {
        public int numHolesPerPlayer() => 6;
        public int numStartStones()    => 4;
        public bool CheckValidMove(Board board, int clickedHole)
        {
            return board.GetHoles[clickedHole] != 0;
        }

        public bool CheckWin(Board state)
        {
            IEnumerable<byte> s = state.GetHolesP1;
            foreach (byte b in s)
                if (b != 0)
                    return false;
            return true;
        }
        
        public Player NextPlayer(Player p, Board before, Board after)
        {
            // todo : check if the last stone was dropped in an empty hole on the player's side
            bool scoredPoint = after.GetHoles[p.PlayerNumber == 1 ? numHolesPerPlayer() : 13] > before.GetHoles[p.PlayerNumber == 1 ? numHolesPerPlayer() : 13];
            bool lastStoneInBaseHole = after.GetHoles[p.PlayerNumber == 1 ? numHolesPerPlayer() +1 : 13] > before.GetHoles[p.PlayerNumber == 1 ? numHolesPerPlayer() +1 : 0];
            if (scoredPoint && lastStoneInBaseHole)
                return p;
            else
                return p.PlayerNumber == 1 ? new Player(2, ConsoleColor.Red, 0, false) : new Player(1, ConsoleColor.Blue, 0, false);
        }
        
    }
}
