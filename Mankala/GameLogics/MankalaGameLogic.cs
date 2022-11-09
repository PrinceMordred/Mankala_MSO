namespace Mankala.GameLogics;

public class MankalaGameLogic : GameLogic
{
	public MankalaGameLogic(Board board) : base(board)
	{
	}

	protected override bool CheckValidMove(int selectedHole)
	{
		return _board.GetHolesCopy[selectedHole] != 0;
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

	protected override Player NextPlayer(Player player)
	{
		throw new NotImplementedException();
	}
}