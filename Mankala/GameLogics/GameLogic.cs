namespace Mankala.GameLogics;

public abstract class GameLogic : IObservable<string>
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
		_board      = board;
		_playerList = pList;
	}

	/// <summary> Performs the move and all logics related </summary>
	/// <returns> The player who is up next, and the index of the last hole manipulated </returns>
	public (Player, int) MakeMove(int holeIndex) // Template method
	{
		NotifyObserversSuccess($"{CurrentPlayer.PlayerName} chose to spread the stones in hole {holeIndex}");
		
		throw new NotImplementedException();
	}
	
	public int PerformMove(Player player, int holeIndex)
	{
		throw new NotImplementedException();
	}

	public abstract float DetermineScore(Player p);
	
	
	
	// Observer pattern
	private List<IObserver<string>> _observers = new();
	private void NotifyObserversSuccess(string message)
	{
		foreach (var observer in _observers)
			observer.OnNext(message);
	}
	private void NotifyObserversError(Exception exception)
	{
		foreach (var observer in _observers)
			observer.OnError(exception);
	}
	
	// Event handling
	// public event EventHandler CurrentPlayerChanged;
	// public event EventHandler CurrentPlayerChoseHole;
	// public event EventHandler BoardStateChanged;
	//
	//
	// protected virtual void OnCurrentPlayerChanged()
	// {
	// 	CurrentPlayerChanged?.Invoke(this, EventArgs.Empty);
	// }
	//
	// protected virtual void OnCurrentPlayerChoseHole()
	// {
	// 	CurrentPlayerChoseHole?.Invoke(this, EventArgs.Empty);
	// }
	//
	// protected virtual void OnBoardStateChanged()
	// {
	// 	BoardStateChanged?.Invoke(this, EventArgs.Empty);
	// }

	public IDisposable Subscribe(IObserver<string> observer)
	{
		_observers.Add(observer);
		return null; // No unsubscribing :)
	}
}