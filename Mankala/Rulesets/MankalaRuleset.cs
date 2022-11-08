namespace Mankala.Rulesets
{
    public sealed class MankalaRuleset : IRuleset
    {
        public int numHolesPerPlayer { get; set; } = 6;
        public int numStartStones    { get; set; } = 4;
        public bool CheckValidMove(Board board, int clickedHole)
        {
            return board.GetHoles[clickedHole] != 0;
        }

        public bool CheckWin(Board state) =>
            state.GetHolesP1.All(x => x == 0)
         || state.GetHolesP2.All(x => x == 0);
        //TODO: only checks if the game is OVER, not who won.
        //TODO: Also, if player 1 is up but p2 has no stones, the game ends which should not happen
        
        public Player NextPlayer(Player p, Board before, Board after)
        {
            // todo : check if the last stone was dropped in an empty hole on the player's side
           
        }
        
    }
}
