using System.Text;
using Mankala;

const byte startingStonesPerHole = 4;
const byte holesPerPlayer = 3;

var board = new Board(); // No constructor means that user input is requested

// remember the state of who (and how) selects the next hole to make a move
var playerOneIsOn = true;
int selectedHoleP1 = 0;
int selectedHoleP2 = board.IndexOfBaseHoleP2 - 1;

while (true)
{
	// interpret current state
	ref var currentHoleIndex = ref playerOneIsOn ? ref selectedHoleP1 : ref selectedHoleP2;
	var rangeOfHolesOfCurrentPlayer = playerOneIsOn ? board.RangeOfHolesP1 : board.RangeOfHolesP2;
	var otherPlayerIndex = playerOneIsOn ? 2 : 1;
	
	// Ask the player to make a move
	Console.Clear();
	promptSelectHole(ref currentHoleIndex, rangeOfHolesOfCurrentPlayer);
	Console.Beep();
	
	// Make instructed move
	board.MakeMove(currentHoleIndex, otherPlayerIndex);
	playerOneIsOn = !playerOneIsOn;
}

// Converts selectedHoleOfCurrentPlayer to the new selected hole index
void promptSelectHole(ref int currentHoleIndex, Range holeIndexes)
{
	// Print the board and the selection arrow to the console.
	var (holesOffset, holeWidth, arrowY) = printBoardAndSelectionArrow(!playerOneIsOn);
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
		}
		catch (Exception e)
		{
		}
		
		switch (inputKey)
		{
			case ConsoleKey.RightArrow: currentHoleIndex += playerOneIsOn ? 1 : -1; // the ternary compensates for the fact that the upper holes go from right to left
				break;
			case ConsoleKey.LeftArrow:  currentHoleIndex += playerOneIsOn ? -1 : 1; // the ternary compensates for the fact that the upper holes go from right to left
				break;
			default: // wrong keypresses should be ignored
				continue;
		}
		// Clamp selected hole to player's holes
		currentHoleIndex = Math.Clamp(currentHoleIndex, holeIndexes.Start.Value, holeIndexes.End.Value - 1);
		

		// Move the arrow to the new selected hole
		moveSelectionArrow(currentHoleIndex);
	}

	// Literally moves the arrow in the console to the right hole
	void moveSelectionArrow(int currentHoleIndex)
	{
		// Make the arrow go form right to left when selecting upper row
		var nthHoleFromLeft = playerOneIsOn ? currentHoleIndex : (holeIndexes.End.Value - 1) - currentHoleIndex; 
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