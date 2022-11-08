namespace Mankala.GameLogics;
using Mankala;
using Mankala.Rulesets;

public abstract class GameLogic
{
	protected Board _board;
    protected IRuleset _ruleset;
	protected IEnumerator<(byte, Board.HoleKind)> HoleCycle => _board.GetHolesCycle(); 

	/// <param name="holeIndex">the hole from which the move is performed, AKA the hole which stones are spread</param>
	/// <param name="otherPlayerIndex">The index of the OTHER player</param>
	public abstract int MakeMove(int holeIndex, int otherPlayerIndex); // factory method

    protected GameLogic(Board board, IRuleset ruleset)
    {
        _board = board;
        _ruleset = ruleset;
    }
    
}