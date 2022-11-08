namespace Mankala.GameLogics;

public abstract class GameLogic
{
	private Board _board;
	private IEnumerator<(byte, Board.HoleKind)> HoleCycle => _board.GetHolesCycle(); 

	/// <param name="holeIndex">the hole from which the move is performed, AKA the hole which stones are spread</param>
	/// <param name="otherPlayerIndex">The index of the OTHER player</param>
	public abstract void MakeMove(int holeIndex, int otherPlayerIndex); // factory method
	
	protected GameLogic(Board board)
	{
		_board = board;
	}
}