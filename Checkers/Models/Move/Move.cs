using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Models
{
    public abstract class Move
    {
        public abstract void MakeMove(Board board);
    }
}
