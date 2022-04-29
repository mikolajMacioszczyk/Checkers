using Checkers.Interfaces;
using Checkers.Models;

namespace Checkers.Eveluation
{
    public class TestEvaluation : IBoardEvaluation
    {
        public int Evalueate(Board board)
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
                    return GetWhiteFigureEvaluation(figure, row, column, size);
                default:
                    return GetBlackFigureEvaluation(figure, row, column, size);
            }
        }

        public int GetWhiteFigureEvaluation(Figure figure, int row, int column, int size)
        {
            if (figure is WhiteKing)
            {
                return 10;
            }

            if (row < size / 3)
            {
                return 2;
            }
            else if (row < 2 * size / 3)
            {
                return 3;
            }
            return 5;
        }

        public int GetBlackFigureEvaluation(Figure figure, int row, int column, int size)
        {
            if (figure is BlackKing)
            {
                return -10;
            }

            if (row < size / 3)
            {
                return 5;
            }
            else if (row < 2 * size / 3)
            {
                return 3;
            }
            return 2;
        }
    }
}
