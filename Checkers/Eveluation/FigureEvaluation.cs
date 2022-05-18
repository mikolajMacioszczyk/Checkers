using Checkers.Interfaces;
using Checkers.Models;

namespace Checkers.Eveluation
{
    /// <summary>
    /// King add 10 points, Man 3 points
    /// Position does not impact overall score
    /// </summary>
    public class FigureEvaluation : IBoardEvaluation
    {
        public int Evaluate(Board board)
        {
            int sum = 0;

            for (int row = 0; row < board.Size; row++)
            {
                for (int column = 0; column < board.Size; column++)
                {
                    if (board.IsPositionEnabled(row, column))
                    {
                        var position = board.Positions[row, column];
                        if (position.Figure != null)
                        {
                            sum += GetFigureEvaluation(position.Figure);
                        }
                    }
                }
            }

            return sum;
        }

        public int GetFigureEvaluation(Figure figure)
        {
            switch (figure.Color)
            {
                case Enums.FigureColor.White:
                    return GetWhiteFigureEvaluation(figure);
                default:
                    return GetBlackFigureEvaluation(figure);
            }
        }

        public int GetWhiteFigureEvaluation(Figure figure)
        {
            return figure is WhiteKing ? 10 : 3;
        }

        public int GetBlackFigureEvaluation(Figure figure)
        {
            return figure is BlackKing ? -10 : -3;
        }
    }
}
