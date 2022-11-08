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
        // variables for a Ruleset
        public int numHolesPerPlayer();
        public int numStartStones();
        
        // methods for a ruleset
        public bool CheckValidMove(Board state, int clickedHole);
        public bool CheckWin      (Board state);
        public Player NextPlayer     (Player p, Board before, Board after);
    }
}
