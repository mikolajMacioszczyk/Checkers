using Checkers.Enums;

namespace Checkers.Models
{
    public class Man : Figure
    {
        public Man(FigureColor color) : base(color)
        {
        }

        public override List<Move> GetAvailableMoves(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
