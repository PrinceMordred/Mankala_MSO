namespace Mankala.GameLogics;

public class MankalaGameLogic : GameLogic
{
	public MankalaGameLogic(Board board, Player player) : base(board, player)
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
        //TODO
    }
}