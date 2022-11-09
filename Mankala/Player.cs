namespace Mankala
{
    public class Player
    {
        public Player(int playerNumber,
            string playerName,
            ConsoleColor consoleColor,
            int lastSelectedHole)
        {
            PlayerNumber = PlayerNumber;
            PlayerName = playerName;
            ConsoleColor = consoleColor;
            LastSelectedHole = lastSelectedHole;
        }

        public void PrintInColor()
        {
            Console.ForegroundColor = ConsoleColor;
            Console.Write(PlayerName);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public int PlayerNumber { get; init; }
        public string PlayerName { get; init; }
        public ConsoleColor ConsoleColor { get; init; }
        public int LastSelectedHole { get; set; }

        public void Deconstruct(out int playerNumber, out string playerName, out ConsoleColor consoleColor, out int? lastSelectedHole)
        {
            playerNumber = this.PlayerNumber;
            playerName = this.PlayerName;
            consoleColor = this.ConsoleColor;
            lastSelectedHole = this.LastSelectedHole;
        }
    }
}
