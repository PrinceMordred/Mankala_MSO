namespace Mankala.GameLogics;

public enum GameLogics
{
	Mankala, Wari
}

public static class SimpleGameFactory
{
	public static GameLogic CreateRuleSet(GameLogics gameLogic, Board board) => gameLogic switch
	{
		GameLogics.Mankala => new MankalaGameLogic(board),
		_                  => throw new Exception("Cannot create non-existing IRuleSet")
	};
}