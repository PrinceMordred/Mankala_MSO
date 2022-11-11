using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala.GameLogics
{
    internal class FrozenYoghurtGameLogic : GameLogic
    {
        public FrozenYoghurtGameLogic(Board board, Player[] pList) : base(board, pList)
        {
        }

        public override int PerformMove(Player player, int holeIndex)
        {
            {
                // make the move && change current player
                var holesCycle = _board.GetHolesCycle(holeIndex);

                holesCycle.MoveNext();
                
                var currentHole = holesCycle.Current;
                var stonesToSpread = currentHole.StoneCount;
                currentHole.StoneCount = 0;
                // do it three times just for funsies
                while (holesCycle.MoveNext() && holesCycle.MoveNext() && holesCycle.MoveNext() && stonesToSpread > 0)
                {
                    currentHole = holesCycle.Current;
                    // in froyo you're allowed to affect the main holes ;)

                    currentHole.StoneCount += 1;
                    stonesToSpread -= 1;
                    if (stonesToSpread == 0) //last stone placed
                    {
                        var otherside = _board.GetRangeOfHoles(OtherPlayer(player).PlayerNumber);
                        var currentStones = currentHole.StoneCount;
                        // if the last stone landed in the other players holes and it has 2 or 3 stones
                        if (otherside.Start.Value <= currentHole.HoleIndex &&
                            otherside.End.Value >= currentHole.HoleIndex &&
                            currentStones == 2 || currentStones == 3)
                        {
                            _board.GetMainHoleRef(player.PlayerNumber).StoneCount += currentStones;
                            currentHole.StoneCount = 0;
                        }
                    }

                }
                return currentHole.HoleIndex;
            }
        }

        protected override Player NextPlayer(Player player, int lastHoleIndex)
        {
            return OtherPlayer(player);
        }
    }
}
