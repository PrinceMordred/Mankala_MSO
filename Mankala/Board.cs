﻿using System.Text;

namespace Mankala;

public abstract class Board
{
	protected readonly byte _stonesPerHole;
	protected readonly byte _holesPerPlayer;
	public IRuleset ruleset;
	
	protected byte[] _holes; // n holes for p1, 1 base hole for p1, n holes for p2, 1 base hole for p2
	public byte[] GetHoles => _holes;
	
	public int   IndexOfBaseHoleP1 => _holesPerPlayer;
	public int   IndexOfBaseHoleP2 => _holes.Length - 1;
	public Range RangeOfHolesP1    => new(0, IndexOfBaseHoleP1);
	public Range RangeOfHolesP2    => new(IndexOfBaseHoleP1 + 1, IndexOfBaseHoleP2);
	
	public virtual byte   GetBaseP1  => _holes[IndexOfBaseHoleP1];
	public virtual byte   GetBaseP2  => _holes[IndexOfBaseHoleP2];
	public virtual byte[] GetHolesP1 => _holes[RangeOfHolesP1];
	public virtual byte[] GetHolesP2 => _holes[RangeOfHolesP2];

	protected bool _IsBaseHoleOf(int player, int i) => player switch
	{
		1 => i == _holesPerPlayer,
		2 => i == _holes.Length - 1,
		_ => throw new ArgumentOutOfRangeException(
			"A player was referenced with an incorrect index, which should be 1 or 2: " + player.ToString())
	};

	public Board(byte stonesPerHole, byte holesPerPlayer, IRuleset ruleset)
	{
		_stonesPerHole = stonesPerHole;
		_holesPerPlayer = holesPerPlayer;
		this.ruleset = ruleset;
		InitializeBoard();
	}
	public Board(IRuleset ruleset)
	{
		_stonesPerHole = prompt("How many stones should every hole have?");
		_holesPerPlayer = prompt("How many holes should evey player have?");
		this.ruleset = ruleset;
		InitializeBoard();
	}

	protected void InitializeBoard()
	{
		_holes = new byte[2 + _holesPerPlayer * 2]; // 2 base-holes and every player hole
		Array.Fill(_holes, _stonesPerHole, 0,                   _holesPerPlayer);
		Array.Fill(_holes, _stonesPerHole, _holesPerPlayer + 1, _holesPerPlayer);
	}

	/// <returns>A tuple containing: board ASCII art, width of the art, width of a single hole, offset of the first player hole</returns>
	public virtual (string, int, int, int) PrintBoard()
	{
		// Calculate widths to base printing on
		var holeDisplayWidth = _holes.Max().ToString().Length + 2;
		var width = _holesPerPlayer * holeDisplayWidth + 4; // room for base-holes and margin
		StringBuilder sb = new();

		// Print
		printFilledLine();
		printHolesLine(GetHolesP2.Reverse());
		printBaseHoleLine(GetBaseP2, GetBaseP1);
		printHolesLine(GetHolesP1);
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
	public struct HoleReference
	{
		private readonly int _holeIndex;
		private readonly Board _board;
		
		public readonly HoleKind HoleKind;
		public ref byte StoneCount => ref _board._holes[_holeIndex];

		internal HoleReference(int holeIndex, HoleKind holeKind, Board board)
		{
			_holeIndex  = holeIndex;
			HoleKind    = holeKind;
			_board		= board;
		}
	}

	public enum HoleKind { MainHoleP1, MainHoleP2, NormalHoleP1, NormalHoleP2 }
}