namespace Mankala.Rulesets
{
    public sealed class MankalaRuleset : IRuleset
    {
        public int numHolesPerPlayer() => 6;
        public int numStartStones()    => 4;
        public bool CheckValidMove(Board board, int clickedHole)
        {
            return board.GetHoles[clickedHole] != 0;
        }

        public bool CheckWin(Board state)
        {
            IEnumerable<byte> s = state.GetHolesP1;
            foreach (byte b in s)
                if (b != 0)
                    return false;
            return true;
        }
        
        public Player NextPlayer(Player p, Board before, Board after)
        {
            // todo : check if the last stone was dropped in an empty hole on the player's side
           
        }
        
    }
}
