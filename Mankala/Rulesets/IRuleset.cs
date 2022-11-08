namespace Mankala.Rulesets
{
    public enum Ruleset
    {
        Mankala
    }
    
    public static class RulesetSimpleFactory
    {
        public static IRuleset CreateRuleSet(Ruleset ruleset) => ruleset switch
        {
            Ruleset.Mankala => new MankalaRuleset(),
            _               => throw new Exception("Cannot create non-existing IRuleSet")
        };
    }
    
    public interface IRuleset
    {
        public int numHolesPerPlayer { get; set; }
        public int numStartStones    { get; set; }
        
        
        public bool CheckValidMove(Board state, int clickedHole);
        public bool CheckWin      (Board state);
        public Player NextPlayer  (Player lastPlayer, Board.HoleReference lastHole);
    }
}
