namespace Mankala
{
    public class Player
    {
        public int PlayerNumber;
        public ConsoleColor cColor;
        public int Score;
        public bool IsAi;

        public Player(int _PlayerNumber, ConsoleColor _cColor, int _score, bool _isAi)
        {
            PlayerNumber = _PlayerNumber;
            cColor = _cColor;
            Score = _score;
            IsAi = _isAi;
        }

        public override string ToString()
        {
            return "Player " + PlayerNumber;
        }
    }
}
