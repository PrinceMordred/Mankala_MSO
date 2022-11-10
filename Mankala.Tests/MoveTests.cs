using Mankala.GameLogics;

namespace Mankala.Tests;

public class MoveTests
{
	[Fact]
	public void MakeMove_WhenGivenAnInvalidMove_ShouldNotAlterTheBoardState()
	{
		
	}

	[Fact]
	public void MakeMove_WhenGivenAValidMove_AltersState()
	{
		
	}

	[Fact]
	public void MakeMove_WhenMoveShouldEndGame_EndsTheGame()
	{
		
	}

	[Fact]
	public void MakeMove_WhenMoveIsValid_SpreadsStones()
	{
		
	}

	[Fact]
	public void MakeMove_WhenMoveIsValidAndGoesOverABaseHole_OnlyIncrementsPlayersBaseHole()
	{
		
	}

	[Theory]
	[InlineData(GameLogics.GameLogics.Mankala)]
	public void MakeMove_UsingInlineGameLogic_MakesCorrectPlayerGoNext(GameLogics.GameLogics gameLogic)
	{
		// Arrange
		var board = new Board(6, 4, 1);
		Player[] players =
		{
			new(1, "playerOne", ConsoleColor.Gray, -1),
			new(2, "playerTwo", ConsoleColor.Gray, -1)
		};
		var ruleset = SimpleGameLogicFactory.CreateGameLogic(gameLogic, board, players);

		// Act

		// Assert
	}
}