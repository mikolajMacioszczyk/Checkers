using Checkers.Enums;

namespace Checkers.Models
{
    public abstract class King : Figure
    {
        protected King(FigureColor color) : base(color) { }

        public override List<MoveBase> GetAvailableMoves(Board board)
        {
            var moves = new List<MoveBase>();

            //TryAddKillMoves(board, moves, new List<KillMove>(), CurrentPosition.Row, CurrentPosition.Column);
            if (!moves.Any())
            {
                TryAddNormalMoves(board, moves);
            }

            return moves;
        }

        private void TryAddNormalMoves(Board board, List<MoveBase> moves)
        {
            // move left top
            TryAddNormalMovesFromPath(row => row + 1, column => column - 1, board, moves);

            // move right top
            TryAddNormalMovesFromPath(row => row + 1, column => column + 1, board, moves);

            // move left down
            TryAddNormalMovesFromPath(row => row - 1, column => column - 1, board, moves);

            // move right down
            TryAddNormalMovesFromPath(row => row - 1, column => column + 1, board, moves);
        }

        private void TryAddNormalMovesFromPath(Func<int, int> rowChange, Func<int, int> columnChange, Board board, List<MoveBase> moves)
        {
            var nextRow = rowChange(CurrentPosition.Row);
            var nextColumn = columnChange(CurrentPosition.Column);

            while (nextRow <= board.Size - 1
                && nextRow >= 0
                && nextColumn <= board.Size - 1
                && nextColumn >= 0
                && board.Positions[nextRow, nextColumn].IsEmpty())
            {
                moves.Add(new NormalMove(CurrentPosition, board.Positions[nextRow, nextColumn]));
                nextRow = rowChange(nextRow);
                nextColumn = columnChange(nextColumn);
            }
        }
    }
}
