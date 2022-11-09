namespace Mankala.GameLogics;

public abstract class GameLogic
{
	protected Board _board;

    private Player[] _playerList = new Player[2];

	public Player CurrentPlayer;
	public Player GetP1 => _playerList[0];
	public Player GetP2 => _playerList[1];
    public Player otherPlayer(Player p) => p.PlayerNumber == 1 ? GetP1 : GetP2;

    protected int _numNormalHolesPerPlayer;
	protected int _numMainHolesPerPlayer;
	protected int _numStartStones;
	
	// factory methods
	protected virtual bool CheckValidMove(Player player, int selectedHole)
	{
		if (_board.GetHole(selectedHole) > 0)
			return true;
		return false;
		
	}
	public abstract Player? GetWinner();
	protected abstract Player NextPlayer(Player player, int lastHoleIndex);
	
	public GameLogic(Board board, Player[] pList)
	{
		_board   = board;
		_playerList = pList;
		CurrentPlayer = GetP1;
	}

	/// <summary> Performs the move and all logics related </summary>
	/// <returns> The player who is up next, and the index of the last hole manipulated </returns>
	public virtual void MakeMove(Player p, int holeIndex) // Template method
	{
        // check if move valid make move 
        if (CheckValidMove(CurrentPlayer, holeIndex))
            PerformMove(CurrentPlayer, holeIndex);
    }
	
	public virtual void PerformMove(Player player, int holeIndex)
	{
		// make the move && change current player
		var hCycle = _board.GetHolesCycle(holeIndex);
        
		int stonesToUse = _board.GetHole(holeIndex);
        while (hCycle.MoveNext() && stonesToUse > 0)
        {
	        var hCycleCurrent = hCycle.Current;
	        
	        if (_board.IsMainHoleOf(otherPlayer(player).PlayerNumber, hCycleCurrent._holeIndex))
				continue;
			hCycleCurrent.StoneCount += 1;
			--stonesToUse;
			if (stonesToUse == 0)
				CurrentPlayer = NextPlayer(player, hCycleCurrent._holeIndex);
        }
    }

	public abstract float DetermineScore(Player p);
}