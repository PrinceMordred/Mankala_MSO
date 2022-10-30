﻿using System.Diagnostics;
using System.Text;

namespace Mankala;

public class Board
{
	private readonly byte _stonesPerHole;
	private readonly byte _holesPerPlayer;
	
	private byte[] _holes; // n holes for p1, 1 base hole for p1, n holes for p2, 1 base hole for p2

	public int IndexOfBaseHoleP1 => _holesPerPlayer;
	public int IndexOfBaseHoleP2 => _holes.Length - 1;
	public Range RangeOfHolesP1 => new(0, IndexOfBaseHoleP1);
	public Range RangeOfHolesP2 => new(IndexOfBaseHoleP1 + 1, IndexOfBaseHoleP2);
	
	public byte   GetBaseP1  => _holes[IndexOfBaseHoleP1];
	public byte   GetBaseP2  => _holes[IndexOfBaseHoleP2];
	public byte[] GetHolesP1 => _holes[RangeOfHolesP1];
	public byte[] GetHolesP2 => _holes[RangeOfHolesP2];

	private bool _IsBaseHoleOf(int player, int i) => player switch
	{
		1 => i == _holesPerPlayer,
		2 => i == _holes.Length - 1,
		_ => throw new ArgumentOutOfRangeException(
			"A player was referenced with an incorrect index, which should be 1 or 2: " + player.ToString())
	};

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

	/// <returns>A tuple containing: board ASCII art, width of the art, width of a single hole, offset of the first player hole</returns>
	public (string, int, int, int) PrintBoard()
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

	public IBoardState GetBoardState() =>
		new MankalaBoardState(GetBaseP1, GetBaseP2, GetHolesP1, GetHolesP2);

	/// <param name="holeIndex">the hole from which the move is performed, AKA the hole which stones are spread</param>
	/// <param name="otherPlayerIndex">The index of the OTHER player</param>
	/// <returns>The new state after performing the move</returns>
	public IBoardState MakeMove(int holeIndex, int otherPlayerIndex)
	{
		// pick up all stones
		var stonesToSpread = _holes[holeIndex];
		_holes[holeIndex] = 0;
		
		// spread stones
		while (stonesToSpread > 0)
		{
			var currentHole = ++holeIndex % _holes.Length;
			
			if (_IsBaseHoleOf(otherPlayerIndex, currentHole))
				continue; // Don't move over opponents base hole TODO: make this dependent on IRuleSet
			
			_holes[currentHole] += 1;
			stonesToSpread  -= 1;
		}
		
		// return new state
		return GetBoardState();
	}
}