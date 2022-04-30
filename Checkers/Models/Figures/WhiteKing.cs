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
            var position = CurrentPosition?.Copy();
            var copy = new WhiteKing();
            if (position != null)
            {
                position.Figure = copy;
            }
            return copy;
        }
    }
}
