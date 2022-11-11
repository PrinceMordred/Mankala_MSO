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
	public virtual    Player? GetWinner()
    {
		//check if one of the players has no stones left
		bool Pred1 = _board.GetHoles(1).All(x => x == 0) && CurrentPlayer.PlayerNumber == 1;
		bool Pred2 = _board.GetHoles(2).All(x => x == 0) && CurrentPlayer.PlayerNumber == 2;
		if (Pred1 || Pred2)
		{
			//if so, the player with the most stones in the main holes wins
			return _board.GetMainHole(1) > _board.GetMainHole(2) ? GetP1 : GetP2;
		}
		return null;
	}
	protected abstract Player  NextPlayer(Player player, int lastHoleIndex);
	
	public GameLogic(Board board, Player[] pList)
	{
		_board        = board;
		_playerList   = pList;
		CurrentPlayer = GetP1;
	}

	/// <summary> Performs the move and all logics related </summary>
	/// <returns> The player who is up next, and the index of the last hole manipulated </returns>
	public void MakeMove(int holeIndex) // Template method
	{
		NotifyObserversSuccess($"{CurrentPlayer.PlayerName} chose to spread the stones in hole {holeIndex}");
		
		if (!CheckValidMove(CurrentPlayer, holeIndex)) return;
		var lastHoleAffected = PerformMove(CurrentPlayer, holeIndex);
		
		var previousPlayerNumber   = CurrentPlayer.PlayerNumber;
		CurrentPlayer = NextPlayer(CurrentPlayer, lastHoleAffected);
		
		if (previousPlayerNumber != CurrentPlayer.PlayerNumber) NotifyObserversSuccess(
			$"Next player is {CurrentPlayer.PlayerName} (with playerNumber {CurrentPlayer.PlayerNumber})");
	}

	/// <returns> The index of the hole in which the last stone was spread </returns>
	public abstract int PerformMove(Player player, int holeIndex);

	public virtual float DetermineScore(Player p)
    {
		return _board.GetMainHole(p.PlayerNumber);
	}


	
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