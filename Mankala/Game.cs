using Mankala.Rulesets;

namespace Mankala;

public class Game
{
	protected Board _board;
	protected IRuleset _ruleset;

	public Game(Board board, IRuleset ruleset)
	{
		_board   = board;
		_ruleset = ruleset;
	}

	public Player MakeMove(Player player, int holeIndex)
	{
		if (!_ruleset.CheckValidMove())
			return player;
		
		PerformMove(holeIndex, otherPlayerIndex);

		return _ruleset.NextPlayer(player);
	}

	public Player? GetWinner()
	{
		return _ruleset.CheckWin();
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