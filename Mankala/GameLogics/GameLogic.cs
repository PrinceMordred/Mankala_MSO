namespace Mankala.GameLogics;

public abstract class GameLogic
{
	protected Board _board;

	protected int numNormalHolesPerPlayer;
	protected int numMainHolesPerPlayer;
	protected int numStartStones;
	
	// factory methods
	protected abstract bool    CheckValidMove(int selectedHole);
	public abstract    Player? GetWinner();
	protected abstract Player  NextPlayer(Player player);

	public GameLogic(Board board)
	{
		_board   = board;
	}

	
	/// <summary> Performs the move and all logics related </summary>
	/// <returns> The player who is up next </returns>
	public virtual Player MakeMove(Player player, int holeIndex) // Template method
	{
		if (!CheckValidMove())
			return player;
		
		PerformMove(holeIndex, otherPlayerIndex);

		return NextPlayer(player);
	}
	
	public int PerformMove(int holeIndex, int otherPlayerIndex)
	{
		int finalHoleIndex = 0;
		// pick up all stones out of a hole
		byte stonesToSpread = _board.GetHoles[holeIndex];
		_board.GetHoles[holeIndex] = 0;
		Board.HoleKind oppositeHoleIndex = otherPlayerIndex == 1 ? Board.HoleKind.MainHoleP1 : Board.HoleKind.MainHoleP2;
		
		// spread stones
		var holeCycle = _board.GetHolesCycle();
		while (stonesToSpread > 0)
		{
			// move to next hole
			holeCycle.MoveNext();
			// if the hole is not the other player's base hole, put a stone in it
			if (holeCycle.Current.HoleKind == oppositeHoleIndex)
				continue;
			else
			{
				_board.GetHoles[holeCycle.Current.StoneCount]++;
				stonesToSpread--;
				if (stonesToSpread == 0)
					finalHoleIndex = holeCycle.Current.StoneCount;
			}
		}
		return finalHoleIndex;
	}
}