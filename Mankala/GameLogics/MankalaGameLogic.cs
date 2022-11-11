namespace Mankala.GameLogics;

public class MankalaGameLogic : GameLogic
{
	public MankalaGameLogic(Board board, Player[] players) : base(board, players)
	{
	}

	public override Player? GetWinner()
	{
        //check if one of the players has no stones left
        bool Pred1 = _board.GetHoles(1).All(x => x == 0) && CurrentPlayer.PlayerNumber == 1;
        bool Pred2 = _board.GetHoles(2).All(x => x == 0) && CurrentPlayer.PlayerNumber == 2;
		if ( Pred1 || Pred2)
        {
            //if so, the player with the most stones in the main holes wins
            return _board.GetMainHole(1) > _board.GetMainHole(2) ? GetP1 : GetP2;
        }
        return null;
    }

    protected override Player NextPlayer(Player player, int lastHoleIndex)
	{
        //check what hole the stones were placed in
        int stonesAfterTurn = _board.GetHole(lastHoleIndex);
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
                if (stonesToSpread == 0)
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
                            _board.GetMainholeRef(player.PlayerNumber).StoneCount += opp;
                            opp = 0;
                        }
                    }
                }
                    
            }
            return currentHole.HoleIndex;
        }
    }
    public override float DetermineScore(Player p)
	{
        return _board.GetMainHole(p.PlayerNumber);
    }
}