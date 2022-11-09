namespace Mankala.GameLogics;

public class MankalaGameLogic : GameLogic
{
	public MankalaGameLogic(Board board, Player[] players) : base(board, players)
	{
	}

	public override Player? GetWinner()
	{
		throw new NotImplementedException();
		/*
		 * Je zou LINQ kunnen gebruiken:
		 * state.GetHolesP1.All(x => x == 0)     //bool
		   || state.GetHolesP2.All(x => x == 0); //bool
		 *
		 */
	}

	protected override Player NextPlayer(Player player, int lastHoleIndex)
	{
		throw new NotImplementedException();
	}

	public override float DetermineScore(Player p)
	{
		throw new NotImplementedException();
	}
}