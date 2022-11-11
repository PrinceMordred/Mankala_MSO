namespace Mankala.GameLogics;

public class WariGameLogic : GameLogic
{
	public WariGameLogic(Board board, Player[] playerList) : base(board, playerList)
	{
	}

	public override Player? GetWinner()
	{
		throw new NotImplementedException();
	}

	protected override Player NextPlayer(Player player, int lastHoleIndex)
    {
		return OtherPlayer(player); //TODO
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

            while (holesCycle.MoveNext() && stonesToSpread > 0)
            {
                currentHole = holesCycle.Current;
                if (_board.IsMainHoleOf(OtherPlayer(player).PlayerNumber, currentHole.HoleIndex))
                    continue; // Don't affect other player's main hole

                currentHole.StoneCount += 1;
                if (stonesToSpread == 0)
                {
                    // wari variant 
                    if (currentHole.StoneCount == 1 && _board.IsMainHoleOf(player.PlayerNumber, currentHole.HoleIndex))
                    {
                        
                        
                    }
                    else
                    {
                        PerformMove(player, currentHole.HoleIndex);
                    }
                }
            }
            return currentHole.HoleIndex;
        }
    }
    public override float DetermineScore(Player p)
	{
		throw new NotImplementedException();
	}
}
