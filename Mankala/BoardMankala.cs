using System.Diagnostics;
using System.Text;

namespace Mankala;

public class BoardMankala : Board
{
	public BoardMankala(byte stonesPerHole, byte holesPerPlayer) : base(stonesPerHole, holesPerPlayer)
	{
	}

	public BoardMankala()
	{
	}

	public override void MakeMove(int holeIndex, int otherPlayerIndex)
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
			stonesToSpread      -= 1;
		}
	}
}