using Mankala.Rulesets;

namespace Mankala.GameLogics.DontUse;

public class WariGameLogic : GameLogic
{
    public WariGameLogic(Board board, IRuleset ruleset) : base(board, ruleset)
    {
    }

    public override void MakeMove(int holeIndex, int otherPlayerIndex)
    {
        throw new NotImplementedException();
    }
}