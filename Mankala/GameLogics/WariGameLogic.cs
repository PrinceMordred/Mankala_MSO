namespace Mankala.GameLogics;

public class WariGameLogic : GameLogic
{
	public WariGameLogic(Board board) : base(board)
	{
	}

	public override Player? GetWinner()
	{
		throw new NotImplementedException();
	}

	protected override bool CheckValidMove(int selectedHole)
	{
		throw new NotImplementedException();
	}

	protected override Player NextPlayer(Player player)
	{
		throw new NotImplementedException();
	}
}