using Checkers.Enums;

namespace Checkers.Models
{
    public class BlackMan : Figure
    {
        public BlackMan() : base(FigureColor.Black)
        {
        }

        public override List<Move> GetAvailableMoves(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
