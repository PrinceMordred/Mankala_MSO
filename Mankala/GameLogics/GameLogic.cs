namespace Mankala.GameLogics;

public abstract class GameLogic : IObservable<string>
{
	protected Board _board;
	private Player[] _playerList = new Player[2];
	public Player CurrentPlayer;
	
	public Player GetP1 => _playerList[0];
	public Player GetP2 => _playerList[1];
    public Player OtherPlayer(Player p) => p.PlayerNumber == 1 ? GetP2 : GetP1;

    // factory methods
	protected virtual bool CheckValidMove(Player player, int selectedHole)
	{
		return _board.GetHole(selectedHole) > 0;
	}
	public abstract    Player? GetWinner(); //todo: what if it's a tie? (Not properly implemented everywhere)
	protected abstract Player  NextPlayer(Player player, int lastHoleIndex);
	
	public GameLogic(Board board, Player[] pList)
	{
		_board        = board;
		_playerList   = pList;
		CurrentPlayer = GetP1;
	}

	/// <summary> Performs the move and all logics related </summary>
	/// <returns> The player who is up next, and the index of the last hole manipulated </returns>
	public void MakeMove(Player p, int holeIndex) // Template method
	{
		NotifyObserversSuccess($"{CurrentPlayer.PlayerName} chose to spread the stones in hole {holeIndex}");
		
		if (!CheckValidMove(CurrentPlayer, holeIndex)) return;
		var lastHoleAffected = PerformMove(CurrentPlayer, holeIndex);
		CurrentPlayer = NextPlayer(p, lastHoleAffected);
		
		if (p != CurrentPlayer) NotifyObserversSuccess(
			$"Next player is {CurrentPlayer.PlayerName} (with playerNumber {CurrentPlayer.PlayerNumber})");
	}

	/// <returns> The index of the hole in which the last stone was spread </returns>
	public abstract int PerformMove(Player player, int holeIndex);

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

	public IDisposable Subscribe(IObserver<string> observer)
	{
		_observers.Add(observer);
		return null; // No unsubscribing :)
	}
}