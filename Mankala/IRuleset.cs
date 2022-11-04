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
        public bool CheckValidMove(Board state, int clickedHole);
        public bool CheckWin      (Board state);
        public int NextPlayer     (Board state);
    }
}
