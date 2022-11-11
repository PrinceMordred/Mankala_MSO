namespace Mankala
{
    public class Player
    {
        public Player(int playerNumber,
            string playerName,
            ConsoleColor consoleColor,
            int lastSelectedHole)
        {
            PlayerNumber = playerNumber;
            PlayerName = playerName;
            ConsoleColor = consoleColor;
            LastSelectedHole = lastSelectedHole;
        }

        public void PrintInColor()
        {
            Console.ForegroundColor = ConsoleColor;
            Console.Write(PlayerName);
            Console.ResetColor();
        }

        public int PlayerNumber { get; init; }
        public string PlayerName { get; init; }
        public ConsoleColor ConsoleColor { get; init; }
        public int LastSelectedHole { get; set; }

        public void Deconstruct(out int playerNumber, out string playerName, out ConsoleColor consoleColor, out int? lastSelectedHole)
        {
            playerNumber     = PlayerNumber;
            playerName       = PlayerName;
            consoleColor     = ConsoleColor;
            lastSelectedHole = LastSelectedHole;
        }

		public GameLogics.GameLogic GameLogic
		{
			get => default;
			set
			{
			}
		}

		public Board Board
		{
			get => default;
			set
			{
			}
		}
	}
}
