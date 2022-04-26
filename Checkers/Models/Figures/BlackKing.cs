using Checkers.Enums;

namespace Checkers.Models
{
    public class BlackKing : Figure
    {
        public BlackKing() : base(FigureColor.Black)
        {
        }

        public override List<MoveBase> GetAvailableMoves(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
