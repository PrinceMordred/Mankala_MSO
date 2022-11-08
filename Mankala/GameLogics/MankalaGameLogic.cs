using Mankala.GameLogics;

namespace Mankala;

public class MankalaGameLogic
{
	public class MankalaGameLogic : GameLogic
	{
		public MankalaGameLogic(Board board) : base(board)
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
}