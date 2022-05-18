using Checkers.Interfaces;
using Checkers.Models;

namespace Checkers.Eveluation
{
    /// <summary>
    /// Gives bonus points for friends on your bag and minus points for enemy on back
    /// </summary>
    public class ClustersEvaluation : IBoardEvaluation
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

        public int GetFigureEvaluation(Figure figure, int row, int column, Board board)
        {
            switch (figure.Color)
            {
                case Enums.FigureColor.White:
                    return GetWhiteFigurePositionEvaluation(row, column, board);
                default:
                    return GetBlackFigurePositionEvaluation(row, column, board);
            }
        }

        public int GetWhiteFigurePositionEvaluation(int row, int column, Board board)
        {
            var bonus = 0;
            if (row - 1 >= 0 && column - 1 >= 0)
            {
                var positionBeforeLeft = board.Positions[row - 1, column - 1];
                if (positionBeforeLeft.Figure != null && positionBeforeLeft.Figure.Color == Enums.FigureColor.White)
                {
                    bonus += 2;
                }
                else
                {
                    bonus -= 2;
                }
            }
            if (row - 1 >= 0 && column + 1 < board.Size)
            {
                var positionBeforeRight = board.Positions[row - 1, column + 1];
                if (positionBeforeRight.Figure != null && positionBeforeRight.Figure.Color == Enums.FigureColor.White)
                {
                    bonus += 2;
                }
                else
                {
                    bonus -= 2;
                }
            }
            return bonus;
        }

        public int GetBlackFigurePositionEvaluation(int row, int column, Board board)
        {
            var bonus = 0;
            if (row + 1 < board.Size - 1 && column - 1 >= 0)
            {
                var positionBeforeLeft = board.Positions[row + 1, column - 1];
                if (positionBeforeLeft.Figure != null && positionBeforeLeft.Figure.Color == Enums.FigureColor.Black)
                {
                    bonus += -2;
                }
                else
                {
                    bonus += 2;
                }
            }
            if (row + 1 < board.Size - 1 && column + 1 < board.Size)
            {
                var positionBeforeRight = board.Positions[row + 1, column + 1];
                if (positionBeforeRight.Figure != null && positionBeforeRight.Figure.Color == Enums.FigureColor.Black)
                {
                    bonus += -2;
                }
                else
                {
                    bonus += 2;
                }
            }
            return bonus;
        }
    }
}
