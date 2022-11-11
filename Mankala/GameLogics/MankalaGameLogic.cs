namespace Mankala.GameLogics;

public class MankalaGameLogic : GameLogic
{
	public MankalaGameLogic(Board board, Player[] players) : base(board, players)
	{
	}


    protected override Player NextPlayer(Player player, int lastHoleIndex)
	{
        //check what hole the stones were placed in
        if (_board.IsMainHoleOf(player.PlayerNumber, lastHoleIndex))
            return player; //if it was the main hole, the player gets another turn
        return OtherPlayer(player);
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
                stonesToSpread -= 1;
                if (stonesToSpread == 0) //last stone placed
                {
                    //check if the last move was in the home hole
                    if (_board.IsMainHoleOf(player.PlayerNumber, currentHole.HoleIndex)) { }
                    //not in the main hole
                    //check if the hole was not empty
                    else if (currentHole.StoneCount > 1)
                    { 
                        stonesToSpread = currentHole.StoneCount;
                        currentHole.StoneCount = 0;
                    }
                    // stone landed in an empty hole on the players side
                    else if (_board.GetRangeOfHoles(player.PlayerNumber).Start.Value <= currentHole.HoleIndex &&
                             _board.GetRangeOfHoles(player.PlayerNumber).End.Value   >= currentHole.HoleIndex)
                    {
                        var opp = _board.GetOppositeHole(currentHole.HoleIndex).StoneCount;
                        if (opp == 0) { }
                        else 
                        {
                            _board.GetMainHoleRef(player.PlayerNumber).StoneCount += opp;
                            _board.GetOppositeHole(currentHole.HoleIndex).StoneCount = 0;
                        }
                    }
                }
                    
            }
            return currentHole.HoleIndex;
        }
    }
}