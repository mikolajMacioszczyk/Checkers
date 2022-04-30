using Checkers.Enums;

namespace Checkers.Models
{
    public class BlackKing : King
    {
        public BlackKing() : base(FigureColor.Black) { }

        public override Figure Copy()
        {
            var position = CurrentPosition?.Copy();
            var copy = new BlackKing();
            if (position != null)
            {
                position.Figure = copy;
            }
            return copy;
        }
    }
}
