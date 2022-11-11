namespace Mankala.GameLogics;

public class WariGameLogic : GameLogic
{
	public WariGameLogic(Board board, Player[] playerList) : base(board, playerList)
	{
	}

	protected override Player NextPlayer(Player player, int lastHoleIndex)
    {
		return OtherPlayer(player); 
    }

	public override int PerformMove(Player player, int holeIndex)
	{
		// make the move && change current player
		var holesCycle = _board.GetHolesCycle(holeIndex);

		holesCycle.MoveNext();
		var currentHole = holesCycle.Current;
		var stonesToSpread = currentHole.StoneCount;
		currentHole.StoneCount = 0;

		while (holesCycle.MoveNext() && stonesToSpread > 0)
		{
			currentHole = holesCycle.Current;
			if (_board.IsMainHoleOf(OtherPlayer(player).PlayerNumber, currentHole.HoleIndex) || _board.IsMainHoleOf(player.PlayerNumber, currentHole.HoleIndex))
				continue; // Don't affect the players main holes

			currentHole.StoneCount += 1;
			stonesToSpread -= 1;
			
            if(stonesToSpread == 0) //last stone placed
            {
				var otherside = _board.GetRangeOfHoles(OtherPlayer(player).PlayerNumber);
				var currentStones = currentHole.StoneCount;
				// if the last stone landed in the other players holes and it has 2 or 3 stones
				if (otherside.Start.Value <= currentHole.HoleIndex &&
					otherside.End.Value   >= currentHole.HoleIndex &&
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
