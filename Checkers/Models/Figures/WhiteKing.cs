using Checkers.Enums;

namespace Checkers.Models
{
    public class WhiteKing : King
    {
        public WhiteKing() : base(FigureColor.White)
        {
        }

        public override Figure Copy()
        {
            return new WhiteKing() { CurrentPosition = CurrentPosition};
        }
    }
}
