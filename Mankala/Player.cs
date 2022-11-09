namespace Mankala
{
    public class Player
    {
        public Player(int PlayerNumber,
            string PlayerName,
            ConsoleColor ConsoleColor,
            int? LastSelectedHole)
        {
            this.PlayerNumber = PlayerNumber;
            this.PlayerName = PlayerName;
            this.ConsoleColor = ConsoleColor;
            this.LastSelectedHole = LastSelectedHole;
        }

        public override string ToString()
        {
            return PlayerName;
        }

        public int PlayerNumber { get; init; }
        public string PlayerName { get; init; }
        public ConsoleColor ConsoleColor { get; init; }
        public int? LastSelectedHole { get; set; }

        public void Deconstruct(out int PlayerNumber, out string PlayerName, out ConsoleColor ConsoleColor, out int? LastSelectedHole)
        {
            PlayerNumber = this.PlayerNumber;
            PlayerName = this.PlayerName;
            ConsoleColor = this.ConsoleColor;
            LastSelectedHole = this.LastSelectedHole;
        }
    }
}
