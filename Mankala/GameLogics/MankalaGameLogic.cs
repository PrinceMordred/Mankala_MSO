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
        return OtherPlayer(player);
    }

	public override float DetermineScore(Player p)
	{
        return _board.GetMainHole(p.PlayerNumber);
    }
}