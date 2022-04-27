using Checkers.Enums;

namespace Checkers.Models
{
    public class BlackKing : King
    {
        public BlackKing() : base(FigureColor.Black) { }

        public override Figure Copy()
        {
            return new BlackKing();
        }
    }
}
