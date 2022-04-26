using Checkers.Enums;

namespace Checkers.Models
{
    public class WhiteKing : Figure
    {
        public WhiteKing() : base(FigureColor.White)
        {
        }

        public override List<Move> GetAvailableMoves(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
