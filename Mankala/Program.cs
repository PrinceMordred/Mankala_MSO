using System.Text;
using Mankala;
using Mankala.GameLogics;

// A generic method which removes a lot of duplicate code for prompting the player for input
T prompt<T>(string question, Func<string?, (bool, T)> tryParse,
	string errorMessage = "[ERROR] That's some invalid input. Please try again.")
{
	while (true) // while incorrect input
	{
		Console.Write(question + " > ");
		var input = Console.ReadLine();
		var (success, parsed) = tryParse(input);
		
		if (success) return parsed;
		
		Console.WriteLine(errorMessage);
	}
}

ILogger logger = new Logger();

#region Set up board
var board = promptBoard();
board.Subscribe(logger);
Board promptBoard()
{
	var stonesPerHole  = prompt("How many stones should every hole have?",
		x => (byte.TryParse(x, out var parsed), parsed));
	var holesPerPlayer = prompt("How many holes should evey player have?",
		x => (byte.TryParse(x, out var parsed) && parsed > 1, parsed));

	return new Board(stonesPerHole, holesPerPlayer);
}
#endregion

Player promptPlayer(int playerNumber)
{
	var name = prompt($"What will the name be of player {playerNumber}?",
		x => (!string.IsNullOrWhiteSpace(x), x),
		"That's a weird name. Not judging, but please give another name.");

	var color = prompt("What color will this player be? (Examples: Red, Blue)",
		x => (Enum.TryParse<ConsoleColor>(x, out var parsed), parsed),
		"That's an unknown color to us. Please retry.");

	return new Player(playerNumber, name, color, -1);
}

Player[] players = { promptPlayer(1), promptPlayer(2) };
players[0].LastSelectedHole = 0;
players[1].LastSelectedHole = board.GetMainHoleIndex(players[1].PlayerNumber) - 1;

Console.Clear();

#region Set up game logics

var gameLogic = prompt(@"What ruleset would you like the game to follow?

These are your options:
	- Mankala
	- Wari

", //TODO: werk deze lijst bij
	x => (Enum.TryParse(x, out GameLogics gameLogic),
		SimpleGameLogicFactory.CreateGameLogic(gameLogic, board, players)));
gameLogic.Subscribe(logger);
#endregion

int GetCurrentSelectedHole() => gameLogic.CurrentPlayer.LastSelectedHole; 
bool lowerPlayerIsMakingAMove() => gameLogic.CurrentPlayer.PlayerNumber == 1;

Player? winner;
while ((winner = gameLogic.GetWinner()) == null) // game loop
{
	// Ask the player to make a move
	Console.Clear();
	promptSelectHole(GetCurrentSelectedHole(), board.GetRangeOfHoles(gameLogic.CurrentPlayer.PlayerNumber));
	
	// Make instructed move 
	gameLogic.MakeMove(gameLogic.CurrentPlayer, GetCurrentSelectedHole());
}

printBoardAndSelectionArrow(true);

Console.WriteLine("\n\nWe have a winner!");
winner.PrintInColor();
Console.Write($" won with a score of {gameLogic.DetermineScore(winner)}");

#region Console interactions
// Converts selectedHoleOfCurrentPlayer to the new selected hole index
void promptSelectHole(int currentHoleIndex, Range holeIndexes)
{
	// Print the board and the selection arrow to the console.
	var (holesOffset, holeWidth, arrowY) = printBoardAndSelectionArrow(!lowerPlayerIsMakingAMove());
	var currentArrowX = 0; // Note that the selection arrow is printed on x=0 by default
	
	moveSelectionArrow(currentHoleIndex); // Move the arrow to the standard/last known selected hole

	ConsoleKey inputKey;
	while ((inputKey = Console.ReadKey().Key) != ConsoleKey.Enter) // Keep moving the selection arrow until player presses enter
	{
		try // Sadly, not every computer likes this code
		{
			// Reset cursor position after input
			var (x, y) = Console.GetCursorPosition();
			Console.SetCursorPosition(x -1, y);
		} catch (Exception e) { }
		
		switch (inputKey)
		{
			case ConsoleKey.RightArrow: currentHoleIndex += lowerPlayerIsMakingAMove() ? 1 : -1; // the ternary compensates for the fact that the upper holes go from right to left
				break;
			case ConsoleKey.LeftArrow:  currentHoleIndex += lowerPlayerIsMakingAMove() ? -1 : 1; // the ternary compensates for the fact that the upper holes go from right to left
				break;
			default: // wrong keypresses should be ignored
				continue;
		}
		// Clamp selected hole to player's holes
		currentHoleIndex = Math.Clamp(currentHoleIndex, holeIndexes.Start.Value, holeIndexes.End.Value - 1);
		
		// Move the arrow to the new selected hole
		moveSelectionArrow(currentHoleIndex);

		gameLogic.CurrentPlayer.LastSelectedHole = currentHoleIndex;
	}

	// Literally moves the arrow in the console to the correct hole
	void moveSelectionArrow(int currentHoleIndex)
	{
		// Make the arrow go form right to left when selecting upper row
		var nthHoleFromLeft = lowerPlayerIsMakingAMove() ? currentHoleIndex : (holeIndexes.End.Value - 1) - currentHoleIndex; 
		// Determine (based on the above trick) the new offset of the arrow
		var newArrowX = holesOffset + nthHoleFromLeft * holeWidth + holeWidth - 2; // offset + x of hole + right side of hole
		
		Console.MoveBufferArea( //todo: MoveBufferArea only works in windows...
			// source x, y, width, height
			currentArrowX, arrowY, 1, 2, 
			// target x, y
			newArrowX, arrowY);

		currentArrowX = newArrowX;
	}
}

(int, int, int) printBoardAndSelectionArrow(bool selectUpperHole)
{
	var (boardAscii, boardWidth, holeWidth, holesOffset) = board.PrintBoard();
	StringBuilder stringBuilder = new();
	
	// create potential arrow above the board
	stringBuilder.Append(selectUpperHole ? "|\nV\n" : "\n\n");
	
	// create the board
	stringBuilder.Append(boardAscii);
	
	// create potential arrow under the board
	stringBuilder.AppendLine(!selectUpperHole ? "^\n|\n" : "\n\n");
	
	// Explain what the hell is going on
	stringBuilder.Append("Use your arrow keys to\nmove the arrow, and press\nenter to make a move\n\n> ");
	
	// print to console
	Console.Write(stringBuilder);

	var arrowY = selectUpperHole ? 0 : 2 + 5; // padding + board height
	return (holesOffset, holeWidth, arrowY);
}
#endregion