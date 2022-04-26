using Checkers.Enums;

namespace Checkers.Models
{
    public class King : Figure
    {
        public King(FigureColor color) : base(color)
        {
        }

        public override List<Move> GetAvailableMoves(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
