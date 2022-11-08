using Mankala.GameLogics;
using Mankala.Rulesets;

namespace Mankala.GameLogics.DontUse;

public class MankalaGameLogic : GameLogic
{
    public MankalaGameLogic(Board board, IRuleset ruleset) : base(board, ruleset)
    {
    }

    public override int MakeMove(int holeIndex, int otherPlayerIndex)
	{
        int finalHoleIndex = 0;
        // pick up all stones out of a hole
        byte stonesToSpread = _board.GetHoles[holeIndex];
		_board.GetHoles[holeIndex] = 0;
        Board.HoleKind oppositeHoleIndex = otherPlayerIndex == 1 ? Board.HoleKind.MainHoleP1 : Board.HoleKind.MainHoleP2;
        // spread stones
        while (stonesToSpread > 0)
        {
            // move to next hole
            HoleCycle.MoveNext();
            // if the hole is not the other player's base hole, put a stone in it
            if (HoleCycle.Current.Item2 == oppositeHoleIndex)
                continue;
            else
            {
                _board.GetHoles[HoleCycle.Current.Item1]++;
                stonesToSpread--;
                if (stonesToSpread == 0)
                    finalHoleIndex = HoleCycle.Current.Item1;
            }
        }
        return finalHoleIndex;
	}
	
}