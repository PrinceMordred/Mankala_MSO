namespace Mankala.GameLogics;

public abstract class GameLogic
{
	protected Board _board;

	protected int numNormalHolesPerPlayer;
	protected int numMainHolesPerPlayer;
	protected int numStartStones;
	
	// factory methods
	protected virtual bool    CheckValidMove(Player player, int selectedHole)
	{
        return !(_board.GetHoles[selectedHole] == 0);
    }
	public abstract    Player? GetWinner();
	protected abstract Player  NextPlayer(Player player, int lastHoleIndex);
	
	public GameLogic(Board board)
	{
		_board   = board;
	}

	
	/// <summary> Performs the move and all logics related </summary>
	/// <returns> The player who is up next </returns>
	public virtual (Player, int) MakeMove(Player player, int holeIndex) // Template method //returns the player who played and the index of the last hole
	{
		if (!CheckValidMove(player, holeIndex))
			return (player, -1);

		return (NextPlayer(player, PerformMove(holeIndex)), -1);
	}
	
	public int PerformMove(int holeIndex)
	{
		int finalHoleIndex = 0;
		// pick up all stones out of a hole
		byte stonesToSpread = _board.GetHoles[holeIndex];
		_board.GetHoles[holeIndex] = 0;
		
		// spread stones
		var holeCycle = _board.GetHolesCycle();
		while (stonesToSpread > 0)
		{
			// move to next hole
			holeCycle.MoveNext();
			// if the hole is not the other player's base hole, put a stone in it
			if (holeCycle.Current.HoleKind is Board.HoleKind.MainHoleP2)
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