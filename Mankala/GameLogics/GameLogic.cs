namespace Mankala.GameLogics;

public abstract class GameLogic
{
	protected Board _board;

    private Player[] _playerList = new Player[2];

	public Player currentPlayer;
    public Player otherPlayer(Player p) => p.PlayerNumber == 1 ? _playerList[1] : _playerList[0];

    protected int _numNormalHolesPerPlayer;
	protected int _numMainHolesPerPlayer;
	protected int _numStartStones;
	
	// factory methods
	protected virtual bool    CheckValidMove(Player player, int selectedHole)
	{
        
    }
	public abstract    Player? GetWinner();
	protected abstract Player  NextPlayer(Player player, int lastHoleIndex);
	
	public GameLogic(Board board, Player[] pList)
	{
		_board   = board;
		_playerList = pList;
	}

	
	/// <summary> Performs the move and all logics related </summary>
	/// <returns> The player who is up next </returns>
	public virtual (Player, int) MakeMove(Player player, int holeIndex) // Template method //returns the player who played and the index of the last hole
	{
		
	}
	
	public int PerformMove(Player player, int holeIndex)
	{
		
	}
}