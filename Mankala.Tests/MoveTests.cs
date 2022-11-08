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
	[InlineData(Ruleset.Mankala)]
	public void MakeMove_BasedOnExistingRuleSet_MakesCorrectPlayerGoNext(Ruleset rulesetType)
	{
		// Arrange
		var ruleset = RulesetSimpleFactory.CreateRuleSet(rulesetType);

		// Act

		// Assert
	}
}