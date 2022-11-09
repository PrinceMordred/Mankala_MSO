namespace Mankala.GameLogics;

public enum GameLogics
{
	Mankala, Wari
}

public static class SimpleGameLogicFactory
{
	public static GameLogic CreateGameLogic(GameLogics gameLogic, Board board, Player[] players) => gameLogic switch
	{
		GameLogics.Mankala => new MankalaGameLogic(board, players),
		_                  => throw new Exception("Cannot create non-existing IRuleSet")
	};
}