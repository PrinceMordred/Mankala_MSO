namespace Mankala.Tests;

public class BoardTests
{
	[Theory]
	[InlineData(1)]	[InlineData(4)]	[InlineData(6)]	[InlineData(50)] [InlineData(0)]
	public void InitialBoard_WithSpecifiedStoneCount_ShouldContainCorrectNumberOfStones(byte stonesPerHole)
	{
		// Arrange
		var board = new Board(stonesPerHole, 6);

		// Act

		// Assert
		Assert.Equal(0, board.GetBaseP1);
		Assert.Equal(0, board.GetBaseP2);
		Assert.All(board.GetHolesP1, b => Assert.Equal(stonesPerHole, b));
		Assert.All(board.GetHolesP2, b => Assert.Equal(stonesPerHole, b));
	}
	[Theory]
	[InlineData(1)]	[InlineData(4)]	[InlineData(6)]	[InlineData(50)] [InlineData(0)]
	public void InitialBoard_WithSpecifiedHoleCount_ShouldContainCorrectNumberOfHoles(byte holesPerPlayer)
	{
		// Arrange
		var board = new Board(4, holesPerPlayer);

		// Act

		// Assert
		Assert.Equal(holesPerPlayer, board.GetHolesP1.Length);
		Assert.Equal(holesPerPlayer, board.GetHolesP2.Length);
	}
}