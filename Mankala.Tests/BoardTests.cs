namespace Mankala.Tests;

public class BoardTests
{
	[Theory]
	[InlineData(1)]	[InlineData(4)]	[InlineData(6)]	[InlineData(50)] [InlineData(0)]
	public void InitialBoard_WithSpecifiedStoneCount_ShouldContainCorrectNumberOfStones(byte stonesPerHole, byte holesPerPlayer = 6)
	{
		// Arrange
		var board = new Board(stonesPerHole, holesPerPlayer);

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
	public void InitialBoard_WithSpecifiedHoleCount_ShouldContainCorrectNumberOfHoles(byte holesPerPlayer, byte stonesPerHole = 4)
	{
		// Arrange
		var board = new Board(stonesPerHole, holesPerPlayer);

		// Act
		// (board should have initialized itself)

		// Assert
		Assert.Equal(holesPerPlayer, board.GetHoles(1).Length);
		Assert.Equal(holesPerPlayer, board.GetHoles(2).Length);
	}

	[Fact]
	public void GetHolesCycle_ImmutablyAndInfinitelyReturnsAllHoles()
	{
		// Arrange
		const byte numStartStones = 4;
		const byte numNormalHolesPerPlayer = 6;
		const int testCycles = 4;
		var stonesPerCycle = numNormalHolesPerPlayer * 2 + 2; //TODO: 2 should be replaced with numOfMainHolesPerPlayer
		
		Board board = new(numStartStones, numNormalHolesPerPlayer);

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
	[InlineData(1)] [InlineData(2)]
	public void IsMainHoleOf_WhenAskedForInlinePlayer_ReturnsCorrectBool(int playerNumber)
	{
		// Arrange
		Board board = new(4, 6);
		var correctMainHoleIndex = board.GetMainHoleIndex(playerNumber);
		var incorrectMainHoleIndex = Math.Max(correctMainHoleIndex - 1, 0);

		// Act
		var resultCorrect = board.IsMainHoleOf(playerNumber, correctMainHoleIndex);
		var resultIncorrect = board.IsMainHoleOf(playerNumber, incorrectMainHoleIndex);

		// Assert
		Assert.True(resultCorrect);
		Assert.False(resultIncorrect);
	}
}