using Checkers.Enums;

namespace Checkers.Helpers
{
    public static class CheckersHelper
    {
        public static FigureColor Opposite(FigureColor color) =>
            color == FigureColor.White ? FigureColor.Black : FigureColor.White;
    }
}
