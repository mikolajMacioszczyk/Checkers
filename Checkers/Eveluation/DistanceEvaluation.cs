using Checkers.Interfaces;
using Checkers.Models;

namespace Checkers.Eveluation
{
    /// <summary>
    /// Gives points based on distance from base
    /// </summary>
    public class DistanceEvaluation : IBoardEvaluation
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
                            sum += GetFigureEvaluation(position.Figure, row, column, board.Size);
                        }
                    }
                }
            }

            return sum;
        }

        public int GetFigureEvaluation(Figure figure, int row, int column, int size)
        {
            switch (figure.Color)
            {
                case Enums.FigureColor.White:
                    return GetWhiteFigureEvaluation(row);
                default:
                    return GetBlackFigureEvaluation(row, size);
            }
        }

        public int GetWhiteFigureEvaluation(int row)
        {
            return row + 1;
        }

        public int GetBlackFigureEvaluation(int row, int size)
        {
            return - (size - row);
        }
    }
}
