using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mankala
{
	public class ProgramUI
	{
		public Board Board
		{
			get => default;
			set
			{
			}
		}

		public GameLogics.GameLogic GameLogic
		{
			get => default;
			set
			{
			}
		}

		public Player Player
		{
			get => default;
			set
			{
			}
		}

		/// <summary></summary>
		/// <remarks></remarks>
		public GameLogics.SimpleGameLogicFactory SimpleGameLogicFactory
		{
			get => default;
			set
			{
			}
		}

		public ILogger ILogger
		{
			get => default;
			set
			{
			}
		}

		public Logger Logger
		{
			get => default;
			set
			{
			}
		}

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

		Board promptBoard()
		{
			var stonesPerHole = prompt("How many stones should every hole have?",
				x => (byte.TryParse(x, out var parsed) && parsed > 0, parsed));
			var normalHolesPerPlayer = prompt("How many holes should evey player have?",
				x => (byte.TryParse(x, out var parsed) && parsed > 1, parsed));

			return new Board(stonesPerHole, normalHolesPerPlayer);
		}

		Player promptPlayer(int playerNumber)
		{
			var name = prompt($"What will the name be of player {playerNumber}?",
				x => (!string.IsNullOrWhiteSpace(x), x),
				"[ERROR] That's a weird name. Not judging, but please give another name.");

			var color = prompt("What color will this player be? (Examples: Red, Blue)",
				x => (Enum.TryParse<ConsoleColor>(x, out var parsed), parsed),
				"[ERROR] That's an unknown color to us. Please retry.");

			return new Player(playerNumber, name, color, -1);
		}

		void promptSelectHole(int currentHole, Range holeIndexes)
		{
			void moveSelectionArrow(int currentHoleIndex)
			{

			}
		}

		(int, int, int) printBoardAndSelectionArrow(bool selectUpperHole)
		{
			return (1, 2, 3);
		}
	}
}