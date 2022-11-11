using Mankala.GameLogics;

namespace Mankala.Tests;

public class MoveTests
{
	// Global variables, which are instantiated on every call to reset test-data 
	const int defaultNumStartStones = 4;
	const int defaultNumNormalHolesPerPlayer = 6;
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

		var holeIndexOfInvalidMove = board.GetMainHoleIndex(1);
		var previousHolesState   = board.GetHoles(1);

		// Act
		gameLogic.MakeMove(holeIndexOfInvalidMove);
		
		// Assert
		Assert.Equal(previousHolesState, board.GetHoles(1));
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
		gameLogic.MakeMove(holeIndexOfValidMove);
		
		// Assert
		Assert.NotEqual(previousHolesState, board.GetHoles(numberOfPlayerMakingMove));
	}

	[Theory]
	[InlineData(GameLogicTypes.Mankala)]
	[InlineData(GameLogicTypes.Wari)]
	public void MakeMove_WhenMoveIsValidAndGoesOverAMainHole_OnlyIncrementsPlayersMainHole(GameLogicTypes gameLogicType)
	{
		// Arrange
		Board     b         = defaultBoard;
		Player[]  ps        = defaultPlayers;
		GameLogic gameLogic = SimpleGameLogicFactory.CreateGameLogic(gameLogicType, b, ps);

		var targetHoleIndex = b.GetMainHoleIndex(ps[0].PlayerNumber) - 1;

		// Act
		gameLogic.MakeMove(targetHoleIndex);

		// Assert
		Assert.NotEqual(0, b.GetMainHole(1));
		Assert.Equal(0, b.GetMainHole(2));
	}

	[Theory]
	[InlineData(GameLogicTypes.Mankala, 1, true)]
	[InlineData(GameLogicTypes.Mankala, 2, false)]
	// [InlineData(GameLogicTypes.Wari,    1, false)] TODO
	// [InlineData(GameLogicTypes.Wari,    1, true)] TODO
	public void MakeMove_UsingInlineGameLogic_MakesCorrectPlayerGoNext(GameLogicTypes gameLogicType, int holeIndex, bool otherPlayerShouldBeNext)
	{
		// Arrange
		Board     b         = defaultBoard;
		Player[]  ps        = defaultPlayers;
		GameLogic gameLogic = SimpleGameLogicFactory.CreateGameLogic(gameLogicType, b, ps);

		// Act
		gameLogic.MakeMove(holeIndex);
		
		// Assert
		Assert.Equal(otherPlayerShouldBeNext, gameLogic.CurrentPlayer.PlayerNumber != 1);
	}
	
	[Theory]
	[InlineData(GameLogicTypes.Mankala)]
	[InlineData(GameLogicTypes.Wari)]
	public void MakeMove_AfterMove_TotalNumberOfStonesDidNotChange(GameLogicTypes gameLogicType)
	{
		// Arrange
		Board     b         = defaultBoard;
		Player[]  ps        = defaultPlayers;
		GameLogic gameLogic = SimpleGameLogicFactory.CreateGameLogic(gameLogicType, b, ps);

		var initialStoneCount = countStoneCount();
		Assert.Equal(initialStoneCount, countStoneCount());
			// Tests whether CountStoneCount does not manipulate test data

		// Act
		gameLogic.MakeMove(0);
		gameLogic.MakeMove(1);
		gameLogic.MakeMove(2);
		gameLogic.MakeMove(4);

		// Assert
		Assert.Equal(defaultNumNormalHolesPerPlayer * 2 * defaultNumStartStones, countStoneCount());
			// Tests whether the CountStoneCount works like intended
		
		Assert.Equal(initialStoneCount, countStoneCount());

		int countStoneCount()
		{
			var cycle = b.GetHolesCycle(0);
			int count = 0;

			for (int i = 0; i <= b.GetMainHoleIndex(2); i++)
			{
				cycle.MoveNext();
				count += cycle.Current.StoneCount;
			}

			return count;
		}
	}
	
	[Theory]
	[InlineData(GameLogicTypes.Mankala, 5)]
	[InlineData(GameLogicTypes.Wari, 5)]
	public void MakeMove_AfterEveryMove_MainHoleStoneCountsDontDecrease(GameLogicTypes gameLogicType, int iterations)
	{
		// Arrange
		Board     b         = defaultBoard;
		Player[]  ps        = defaultPlayers;
		GameLogic gameLogic = SimpleGameLogicFactory.CreateGameLogic(gameLogicType, b, ps);

		byte[,] stoneCountHistory = new byte[iterations, 2];

		// Act
		for (int i = 0; i < iterations; i++)
		{
			gameLogic.MakeMove(i);
			stoneCountHistory[i, 0] = b.GetMainHole(1);
			stoneCountHistory[i, 1] = b.GetMainHole(2);
		}

		// Assert
		for (int i = 1; i < iterations; i++)
		{
			Assert.True(stoneCountHistory[i, 0] >= stoneCountHistory[i-1, 0]);
			Assert.True(stoneCountHistory[i, 1] >= stoneCountHistory[i-1, 1]);
		}
	}
}