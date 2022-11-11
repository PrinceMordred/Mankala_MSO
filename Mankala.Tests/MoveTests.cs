using Mankala.GameLogics;

namespace Mankala.Tests;

public class MoveTests
{
	// Global variables, which are instantiated on every call to reset test-data 
	private Board defaultBoard => new Board(4, 6);
	private Player[] defaultPlayers => new []
	{
		new Player(1, "Player 1", ConsoleColor.Red, -1),
		new Player(2, "Player 2", ConsoleColor.Blue, -1)
	};
	
	[Theory]
	[InlineData(GameLogicTypes.Mankala)]
	[InlineData(GameLogicTypes.Wari)]
	public void MakeMove_WhenGivenAnInvalidMove_ShouldNotAlterTheBoardState(GameLogicTypes gameLogicType)
	{
		// Arrange
		var players = defaultPlayers;
		var board = defaultBoard;
		var gameLogic = SimpleGameLogicFactory.CreateGameLogic(gameLogicType, board, players);

		var holeIndexOfInvalidMoveP1 = board.GetMainHoleIndex(1);
		var holeIndexOfInvalidMoveP2 = board.GetMainHoleIndex(2);

		var previousHolesStateP1 = board.GetHoles(1);
		var previousHolesStateP2 = board.GetHoles(2);

		// Act
		gameLogic.MakeMove(players[0], holeIndexOfInvalidMoveP1);
		gameLogic.MakeMove(players[1], holeIndexOfInvalidMoveP2);
		
		// Assert
		Assert.Equal(previousHolesStateP1, board.GetHoles(1));
		Assert.Equal(previousHolesStateP2, board.GetHoles(2));
	}

	[Theory]
	[InlineData(GameLogicTypes.Mankala)]
	[InlineData(GameLogicTypes.Wari)]
	public void MakeMove_WhenGivenAValidMove_AltersState(GameLogicTypes gameLogicType)
	{
		// Arrange
		var players = defaultPlayers;
		var board     = defaultBoard;
		var gameLogic = SimpleGameLogicFactory.CreateGameLogic(gameLogicType, board, players);
		const int numberOfPlayerMakingMove = 1;

		var holeIndexOfValidMove = 0; // TODO: this is by defenition of the implementation, yet this is too high coupling
		var previousHolesState = board.GetHoles(numberOfPlayerMakingMove);

		// Act
		gameLogic.MakeMove(players[0], holeIndexOfValidMove);
		
		// Assert
		Assert.NotEqual(previousHolesState, board.GetHoles(numberOfPlayerMakingMove));
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
	[InlineData(GameLogics.GameLogicTypes.Mankala)]
	public void MakeMove_UsingInlineGameLogic_MakesCorrectPlayerGoNext(GameLogicTypes gameLogicType)
	{
		// Arrange
		var board = new Board(6, 4);
		Player[] players =
		{
			new(1, "playerOne", ConsoleColor.Gray, -1),
			new(2, "playerTwo", ConsoleColor.Gray, -1)
		};
		var ruleset = SimpleGameLogicFactory.CreateGameLogic(gameLogicType, board, players);

		// Act

		// Assert
	}
}