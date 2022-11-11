namespace Mankala.GameLogics;

public enum GameLogicTypes
{
	Mankala, Wari, FrozenYoghurtGameLogic
}

public static class SimpleGameLogicFactory
{
	public static GameLogic CreateGameLogic(GameLogicTypes gameLogicType, Board board, Player[] players) => gameLogicType switch
	{
		GameLogicTypes.Mankala => new MankalaGameLogic(board, players),
		GameLogicTypes.Wari    => new WariGameLogic(board, players),
		GameLogicTypes.FrozenYoghurtGameLogic => new FrozenYoghurtGameLogic(board, players),
		_                      => throw new Exception("Cannot create non-existing GameLogic")
	};
}