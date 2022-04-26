using Checkers.Enums;

namespace Checkers.Models
{
    public abstract class Figure
    {
        public FigureColor Color { get; private set; }
        public Position CurrentPosition { get; set; }

        public Figure(FigureColor color)
        {
            Color = color;
        }

        public abstract List<MoveBase> GetAvailableMoves(Board board);
    }
}
