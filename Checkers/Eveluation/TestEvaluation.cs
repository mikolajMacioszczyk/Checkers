using Checkers.Interfaces;
using Checkers.Models;

namespace Checkers.Eveluation
{
    public class TestEvaluation : IBoardEvaluation
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
                    var whiteValue = GetWhiteFigureEvaluation(figure, row, column, board);
                    whiteValue += GetWhiteFigurePositionEvaluation(figure, row, column, board);
                    return whiteValue;
                default:
                    var blackValue = GetBlackFigureEvaluation(figure, row, column, board);
                    blackValue += GetBlackFigurePositionEvaluation(figure, row, column, board);
                    return blackValue;
            }
        }

        public int GetWhiteFigureEvaluation(Figure figure, int row, int column, Board board)
        {
            if (figure is WhiteKing)
            {
                return 30;
            }

            if (row == 0 || column == 0 || column == board.Size - 1)
            {
                return 10;
            }
            
            return 8;
        }

        public int GetWhiteFigurePositionEvaluation(Figure figure, int row, int column, Board board)
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

        public int GetBlackFigurePositionEvaluation(Figure figure, int row, int column, Board board)
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

        public int GetBlackFigureEvaluation(Figure figure, int row, int column, Board board)
        {
            if (figure is BlackKing)
            {
                return -30;
            }

            if (row == board.Size - 1 || column == 0 || column == board.Size - 1)
            {
                return -10;
            }

            return -8;
        }
    }
}
