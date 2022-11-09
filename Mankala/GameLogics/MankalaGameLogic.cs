namespace Mankala.GameLogics;

public class MankalaGameLogic : GameLogic
{
	public MankalaGameLogic(Board board) : base(board)
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
        // check if the lastHoleIndex is on the playes side
        if (player.PlayerNumber == 1)
        {
            if (lastHoleIndex <= _board.IndexOfBaseHoleP1)
                return player;
            return new Player(2, "" ,ConsoleColor.Red, 0);
        }
        else
        {
            if (lastHoleIndex > _board.IndexOfBaseHoleP1 && lastHoleIndex <= _board.IndexOfBaseHoleP2)
                return player;
            return new Player(1, "",  ConsoleColor.Blue, 0);
        }
    }
}