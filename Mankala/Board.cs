using System.Diagnostics;
using System.Text;

namespace Mankala;

public class Board
{
	private readonly byte _stonesPerHole;
	private readonly byte _holesPerPlayer;
	[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
	private byte[] _holes; // n holes for p1, 1 base hole for p1, n holes for p2, 1 base hole for p2

	public byte   GetBaseP1  => _holes[_holesPerPlayer];
	public byte   GetBaseP2  => _holes.Last();
	public byte[] GetHolesP1 => _holes[.._holesPerPlayer];
	public byte[] GetHolesP2 => _holes[(_holesPerPlayer + 1) .. ^1];
	
	public Board(byte stonesPerHole, byte holesPerPlayer)
	{
		_stonesPerHole = stonesPerHole;
		_holesPerPlayer = holesPerPlayer;
		InitializeBoard();
	}

	private void InitializeBoard()
	{
		_holes = new byte[2 + _holesPerPlayer * 2]; // 2 base-holes and every player hole
		Array.Fill(_holes, _stonesPerHole, 0,                   _holesPerPlayer);
		Array.Fill(_holes, _stonesPerHole, _holesPerPlayer + 1, _holesPerPlayer);
	}

	public void Print()
	{
		// Calculate widths to base printing on
		var holeDisplayWidth = _holes.Max().ToString().Length + 2;
		var width = _holesPerPlayer * holeDisplayWidth + 4; // room for base-holes and margin
		StringBuilder sb = new();

		// Print
		printFilledLine();
		printHolesLine(GetHolesP1);
		printBaseHoleLine(GetBaseP1, GetBaseP2);
		printHolesLine(GetHolesP2);
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

		Console.WriteLine(sb.ToString());
	}

	public IBoardState GetBoardState() => new MankalaBoardState(GetBaseP1, GetBaseP2, GetHolesP1, GetHolesP2);
}