using System.Text;

namespace Mankala;

public class Board : IObservable<string>
{
	protected readonly byte _numStartStones;
	protected readonly byte _numNormalHolesPerPlayer;
	// TODO: numOfMainHolesPerPlayer
	
	protected byte[] _holes; // n holes for p1, 1 main hole for p1, n holes for p2, 1 main hole for p2
	public byte[] GetHolesCopy => _holes;

	public int GetMainHoleIndex(int playerNumber) => playerNumber is 1
		? _numNormalHolesPerPlayer : _holes.Length - 1;
    
    public Range GetRangeOfHoles(int playerNumber) => playerNumber switch
    {
        1 => new Range(0, GetMainHoleIndex(playerNumber)),
        2 => new Range(GetMainHoleIndex(playerNumber) + 1, _holes.Length - 1),
        _ => throw new ArgumentOutOfRangeException(nameof(playerNumber), playerNumber, "Player number must be 1 or 2")
    };
    public byte   GetMainHole(int playerNumber) => _holes[GetMainHoleIndex(playerNumber)];
    public byte[] GetHoles(int playerNumber)    => _holes[GetRangeOfHoles(playerNumber)];

    public bool IsMainHoleOf(int player, int i) => player switch
	{
		1 => i == _numNormalHolesPerPlayer,
		2 => i == _holes.Length - 1,
		_ => throw new ArgumentOutOfRangeException(
			"A player was referenced with an incorrect index, which should be 1 or 2: " + player.ToString())
	};

	public Board(byte numStartStones, byte numNormalHolesPerPlayer)
	{
		_numStartStones = numStartStones;
		_numNormalHolesPerPlayer = numNormalHolesPerPlayer;
		InitializeBoard();
	}

	protected void InitializeBoard()
	{
		_holes = new byte[2 + _numNormalHolesPerPlayer * 2]; // 2 base-holes and every player hole
		Array.Fill(_holes, _numStartStones, 0,                   _numNormalHolesPerPlayer);
		Array.Fill(_holes, _numStartStones, _numNormalHolesPerPlayer + 1, _numNormalHolesPerPlayer);
	}

	/// <returns>A tuple containing: board ASCII art, width of the art, width of a single hole, offset of the first player hole</returns>
	public (string, int, int, int) PrintBoard()
	{
		// Calculate widths to base printing on
		var holeDisplayWidth = _holes.Max().ToString().Length + 2;
		var width = _numNormalHolesPerPlayer * holeDisplayWidth + 4; // room for base-holes and margin
		StringBuilder sb = new();

		// Print
		printFilledLine();
		printHolesLine(GetHoles(2).Reverse());
		printBaseHoleLine(GetMainHole(2), GetMainHole(1));
		printHolesLine(GetHoles(1));
		printFilledLine();

		void printFilledLine()
		{
			sb.Append('#', width);
			sb.AppendLine();
		}
		
		void printHolesLine(IEnumerable<byte> holeContents)
		{
			sb.Append("##");
			foreach (var hole in holeContents)
				sb.Append(padHole(hole));
			sb.AppendLine("##");
		}

		void printBaseHoleLine(byte holeL, byte holeR)
		{
			sb.Append('#');
			sb.Append(padHole(holeL));
			sb.Append('#', width - 2 - 2 * holeDisplayWidth); // width - edges - holesDisplayed
			sb.Append(padHole(holeR));
			sb.AppendLine("#");
		}

		string padHole(byte hole) => hole.ToString().PadLeft(holeDisplayWidth - 1, ' ') + ' ';

		return (sb.ToString(), width, holeDisplayWidth, 2);
	}

	/// <summary> Infinitely loops through the holes </summary>
	public IEnumerator<HoleReference> GetHolesCycle()
	{
		throw new NotImplementedException();
	}

	/// <summary> Aims to wrap a reference to a hole (which is a value type),
	/// because a tuple with a ref byte is not valid code in .NET 6 </summary>
	public readonly struct HoleReference
	{
		private readonly int _holeIndex;
		private readonly Board _board;
		
		public readonly HoleKind HoleKind;

		public byte StoneCount
		{
			get => _board._holes[_holeIndex];
			set
			{
				_board._holes[_holeIndex] = value;
				_board.NotifyObserversSuccess($"Added a stone on index {_holeIndex}");
			}
		}

		internal HoleReference(int holeIndex, Board board)
		{
			_holeIndex  = holeIndex;
			HoleKind    = holeKind;
			_board		= board;
		}
	}

	public enum HoleKind { MainHoleP1, MainHoleP2, NormalHoleP1, NormalHoleP2 }

	
	// Observer pattern
	private List<IObserver<string>> _observers = new();
	protected void NotifyObserversSuccess(string message)
	{
		foreach (var observer in _observers)
			observer.OnNext(message);
	}
	protected void NotifyObserversError(Exception exception)
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