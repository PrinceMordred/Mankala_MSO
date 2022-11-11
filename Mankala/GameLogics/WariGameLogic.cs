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
		throw new NotImplementedException();
	}

	public override float DetermineScore(Player p)
	{
		throw new NotImplementedException();
	}
}
