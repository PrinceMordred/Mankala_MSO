namespace Mankala.GameLogics;

public enum GameLogicTypes
{
	Mankala, Wari, FrozenYoghurt
}

public class SimpleGameLogicFactory
{
	public GameLogicTypes GameLogicTypes
	{
		get => default;
		set
		{
		}
	}

	public static GameLogic CreateGameLogic(GameLogicTypes gameLogicType, Board board, Player[] players) => gameLogicType switch
	{
		GameLogicTypes.Mankala		 => new MankalaGameLogic(board, players),
		GameLogicTypes.Wari			 => new WariGameLogic(board, players),
		GameLogicTypes.FrozenYoghurt => new FrozenYoghurtGameLogic(board, players),
		_                            => throw new Exception("Cannot create non-existing GameLogic")
	};
}