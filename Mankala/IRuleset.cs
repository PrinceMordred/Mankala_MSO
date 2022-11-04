using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    public interface IRuleset
    {
        // variables for a Ruleset
        public int numHolesPerPlayer();
        public int numStartStones();

        // methods for a ruleset
        public bool CheckValidMove(IBoardState state, int clickedHole);
        public bool CheckWin      (IBoardState state);
        public int NextPlayer     (IBoardState state);
    }
}
