namespace Mankala.GameLogics;

public class MankalaGameLogic : GameLogic
{
	public MankalaGameLogic(Board board, Player[] players) : base(board, players)
	{
	}

	public override Player? GetWinner()
	{
		//check if one of the players has no stones left
		var Pred1 = true;
		var Pred2 = true;
        foreach (var stones in _board.GetHoles(1))
        {
            if (stones != 0)
                Pred1 = false;
                break;
        }
        foreach (var stones in _board.GetHoles(2))
        {
            if (stones != 0)
                Pred2 = false;
                break;
        }
		if ( Pred1 || Pred2)
        {
            //if so, the player with the most stones in the main holes wins
            return _board.GetMainHole(1) > _board.GetMainHole(2) ? GetP1 : GetP2;
        }
        return null;
    }

    protected override Player NextPlayer(Player player, int lastHoleIndex)
	{
        return otherPlayer(player);
    }

	public override float DetermineScore(Player p)
	{
		throw new NotImplementedException();
	}
}