using Checkers.Enums;

namespace Checkers.Models
{
    public class WhiteMan : Figure
    {
        public WhiteMan() : base(FigureColor.White) {}

        public override List<Move> GetAvailableMoves(Board board)
        {
            var moves = new List<Move>();

            TryAddNormalMoves(board, moves);
            TryAddKillMoves(board, moves);

            return moves;
        }

        private void TryAddNormalMoves(Board board, List<Move> moves)
        {
            var nextRow = CurrentPosition.Row + 1;

            var nextColumn = CurrentPosition.Column - 1;
            if (nextColumn >= 0)
            {
                TryAddNormalMove(nextRow, nextColumn, board, moves);
            }

            nextColumn = CurrentPosition.Column + 1;
            if (nextColumn < board.Size)
            {
                TryAddNormalMove(nextRow, nextColumn, board, moves);
            }
        }

        private void TryAddNormalMove(int nextRow, int nextColumn, Board board, List<Move> moves)
        {
            var nextPosition = board.Positions[nextRow, nextColumn];
            if (nextPosition != null && nextPosition.Figure == null)
            {
                if (nextRow == board.Size - 1)
                {
                    moves.Add(new TransformMove(CurrentPosition, nextPosition));
                }
                else
                {
                    moves.Add(new NormalMove(CurrentPosition, nextPosition));
                }
            }
        }

        private void TryAddKillMoves(Board board, List<Move> moves)
        {
            throw new NotImplementedException();
        }
    }
}
