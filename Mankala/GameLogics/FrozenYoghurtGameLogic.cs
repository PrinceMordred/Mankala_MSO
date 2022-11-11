using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala.GameLogics
{
    internal class FrozenYoghurtGameLogic : GameLogic
    {
        public FrozenYoghurtGameLogic(Board board, Player[] pList) : base(board, pList)
        {
        }

        public override int PerformMove(Player player, int holeIndex)
        {
            throw new NotImplementedException();
        }

        protected override Player NextPlayer(Player player, int lastHoleIndex)
        {
            throw new NotImplementedException();
        }
    }
}
