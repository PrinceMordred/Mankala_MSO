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
		throw new NotImplementedException();
	}
	public abstract Player? GetWinner();
	protected abstract Player NextPlayer(Player player, int lastHoleIndex);
	
	public GameLogic(Board board, Player[] pList)
	{
		_board   = board;
		_playerList = pList;
	}

	/// <summary> Performs the move and all logics related </summary>
	/// <returns> The player who is up next, and the index of the last hole manipulated </returns>
	public virtual (Player, int) MakeMove(int holeIndex) // Template method
	{
		throw new NotImplementedException();
	}
	
	public virtual int PerformMove(Player player, int holeIndex)
	{
		throw new NotImplementedException();
	}

	public abstract float DetermineScore(Player p);
}