using Checkers.Interfaces;
using Checkers.Models;

namespace Checkers.Eveluation
{
    /// <summary>
    /// positions on the edges receive double bonuses
    /// </summary>
    public class EdgePositionEvaluation : IBoardEvaluation
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
                            sum += GetFigureEvaluation(position.Figure, row, column, board);
                        }
                    }
                }
            }

            return sum;
        }

        private int GetFigureEvaluation(Figure figure, int row, int column, Board board)
        {
            switch (figure.Color)
            {
                case Enums.FigureColor.White:
                    return GetWhiteFigureEvaluation(figure, row, column, board);
                default:
                    return GetBlackFigureEvaluation(figure, row, column, board);
            }
        }

        public int GetWhiteFigureEvaluation(Figure figure, int row, int column, Board board)
        {
            if (row == 0 || column == 0 || column == board.Size - 1)
            {
                return 10;
            }

            return 5;
        }

        public int GetBlackFigureEvaluation(Figure figure, int row, int column, Board board)
        {
            if (row == board.Size - 1 || column == 0 || column == board.Size - 1)
            {
                return -10;
            }

            return -5;
        }
    }
}
