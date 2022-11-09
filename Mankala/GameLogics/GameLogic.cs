namespace Mankala.GameLogics;

public abstract class GameLogic : IObservable<string>
{
	protected Board _board;
	private Player[] _playerList = new Player[2];
	public Player CurrentPlayer;
	
	public Player GetP1 => _playerList[0];
	public Player GetP2 => _playerList[1];
    public Player OtherPlayer(Player p) => p.PlayerNumber == 1 ? GetP2 : GetP1;

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
	public abstract    Player? GetWinner(); //todo: what if it's a tie? (Not properly implemented everywhere)
	protected abstract Player  NextPlayer(Player player, int lastHoleIndex);
	
	public GameLogic(Board board, Player[] pList)
	{
		_board      = board;
		_playerList = pList;
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
	}
	
	/// <returns> The index of the hole in which the last stone was spread </returns>
	public int PerformMove(Player player, int holeIndex)
	{
		// make the move && change current player
		var holesCycle = _board.GetHolesCycle(holeIndex);
        
		holesCycle.MoveNext();
        var currentHole = holesCycle.Current;
		var stonesToSpread = currentHole.StoneCount;
        currentHole.StoneCount =  0;

        while (holesCycle.MoveNext() && stonesToSpread > 0)
        {
            currentHole = holesCycle.Current;
			if (_board.IsMainHoleOf(OtherPlayer(player).PlayerNumber, currentHole.HoleIndex))
				continue; // Don't affect other player's main hole

			currentHole.StoneCount += 1;
			stonesToSpread         -= 1;
        }

        return currentHole.HoleIndex;
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

	public IDisposable Subscribe(IObserver<string> observer)
	{
		_observers.Add(observer);
		return null; // No unsubscribing :)
	}
}