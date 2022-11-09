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

	protected override Player NextPlayer(Player player, int lastHoleIndex)
	{
        // check if the lastHoleIndex is on the players side
        if (player.PlayerNumber == 1)
        {
            if (!(lastHoleIndex <= _board.IndexOfMainHoleP1)) // !self for p1
            {
                if (_board.GetHoles[lastHoleIndex] == 2 || _board.GetHoles[lastHoleIndex] == 3) // capture
                {
                    _board.GetHoles[_board.IndexOfMainHoleP1] += _board.GetHoles[lastHoleIndex];
                    _board.GetHoles[lastHoleIndex] = 0;
                }
            }
            return new Player(2, ConsoleColor.Red, 0, false);
        }
        else
        {
            if (!(lastHoleIndex > _board.IndexOfMainHoleP1 && lastHoleIndex <= _board.IndexOfMainHoleP2)) //!self for p2
            {
                if (_board.GetHoles[lastHoleIndex] == 2 || _board.GetHoles[lastHoleIndex] == 3) // capture
                {
                    _board.GetHoles[_board.IndexOfMainHoleP2] += _board.GetHoles[lastHoleIndex];
                    _board.GetHoles[lastHoleIndex] = 0;
                }
            }
            return new Player(1, ConsoleColor.Blue, 0, false);
        }
    }
}