namespace Mankala.Tests;

public class BoardTests
{
	[Theory]
	[InlineData(1)]	[InlineData(4)]	[InlineData(6)]	[InlineData(50)] [InlineData(0)]
	public void InitialBoard_WithSpecifiedStoneCount_ShouldContainCorrectNumberOfStones(byte stonesPerHole, byte holesPerPlayer = 6, byte numMainHolesPerPlayer = 1)
	{
		throw new Exception("All tests in BoardTests do not take numMainHolesPerPlayer in account");
		// Arrange
		var board = new Board(stonesPerHole, holesPerPlayer, numMainHolesPerPlayer);

		// Act
		// (board should have initialized itself)

		// Assert
		Assert.Equal(0, board.GetMainHole(1));
		Assert.Equal(0, board.GetMainHole(2));
		Assert.All(board.GetHoles(1), b => Assert.Equal(stonesPerHole, b));
		Assert.All(board.GetHoles(2), b => Assert.Equal(stonesPerHole, b));
	}
	[Theory]
	[InlineData(1)]	[InlineData(4)]	[InlineData(6)]	[InlineData(50)] [InlineData(0)]
	public void InitialBoard_WithSpecifiedHoleCount_ShouldContainCorrectNumberOfHoles(byte holesPerPlayer, byte stonesPerHole = 4, byte numMainHolesPerPlayer = 1)
	{
		throw new Exception("All tests in BoardTests do not take numMainHolesPerPlayer in account");
		// Arrange
		var board = new Board(stonesPerHole, holesPerPlayer, numMainHolesPerPlayer);

		// Act
		// (board should have initialized itself)

		// Assert
		Assert.Equal(holesPerPlayer, board.GetHoles(1).Length);
		Assert.Equal(holesPerPlayer, board.GetHoles(2).Length);
	}

	[Fact]
	public void GetHolesCycle_ImmutablyAndInfinitelyReturnsAllHoles()
	{
		throw new Exception("All tests in BoardTests do not take numMainHolesPerPlayer in account");
		// Arrange
		const byte numStartStones = 4;
		const byte numNormalHolesPerPlayer = 6;
		const byte numMainHolesPerPlayer = 1;
		const int testCycles = 4;
		var stonesPerCycle = numNormalHolesPerPlayer * 2 + 2; //TODO: 2 should be replaced with numOfMainHolesPerPlayer
		
		Board board = new(numStartStones, numNormalHolesPerPlayer, numMainHolesPerPlayer);

		var expectedStoneCounts = new List<byte>();
		for (var i = 0; i < 2; i++)
		{
			for (var j = 0; j < numNormalHolesPerPlayer; j++)
				expectedStoneCounts.Add(numStartStones);
			expectedStoneCounts.Add(0);
		}

		// Act
		var iterator = board.GetHolesCycle(0);
		for (var i = 0; i < testCycles; i++)
		{
			List<byte> aggregatedStoneCounts = new();
			for (var j = 0; j < stonesPerCycle; j++)
			{
				iterator.MoveNext();
				aggregatedStoneCounts.Add(iterator.Current.StoneCount);
			}
			
			// Assert
			Assert.Equal(expectedStoneCounts, aggregatedStoneCounts);
		}
	}

	[Theory]
	[InlineData(1, 1)] [InlineData(2, 1)]
	public void IsMainHoleOf_WhenAskedForInlinePlayer_ReturnsCorrectBool(int playerNumber, byte numMainHolesPerPlayer)
	{
		throw new Exception("All tests in BoardTests do not take numMainHolesPerPlayer in account");
		// Arrange
		Board board = new(4, 6, numMainHolesPerPlayer);
		var correctMainHoleIndex = board.GetMainHoleIndex(playerNumber);
		var incorrectMainHoleIndex = Math.Max(correctMainHoleIndex - 1, 0); //TODO: -1 works fine for 1 main hole, but more will not work this way

		// Act
		var resultCorrect = board.IsMainHoleOf(playerNumber, correctMainHoleIndex);
		var resultIncorrect = board.IsMainHoleOf(playerNumber, incorrectMainHoleIndex);

		// Assert
		Assert.True(resultCorrect);
		Assert.False(resultIncorrect);
	}
}