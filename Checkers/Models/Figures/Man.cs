using Checkers.Enums;

namespace Checkers.Models
{
    public abstract class Man : Figure
    {
        protected Man(FigureColor color) : base(color)
        {
        }

        public override List<MoveBase> GetAvailableMoves(Board board)
        {
            var moves = new List<MoveBase>();

            TryAddNormalMoves(board, moves);
            TryAddKillMoves(board, moves, new Stack<KillMove>(), CurrentPosition.Row, CurrentPosition.Column);

            return moves;
        }

        protected abstract void TryAddNormalMoves(Board board, List<MoveBase> moves);

        protected void TryAddNormalMove(int nextRow, int nextColumn, Board board, List<MoveBase> moves, Func<int, bool> predicate)
        {
            if (predicate(nextColumn))
            {
                var nextPosition = board.Positions[nextRow, nextColumn];
                if (nextPosition != null && nextPosition.Figure == null)
                {
                    if (nextRow == board.Size - 1 || nextRow == 0)
                    {
                        moves.Add(new TransformMove(CurrentPosition, nextPosition));
                    }
                    else
                    {
                        moves.Add(new NormalMove(CurrentPosition, nextPosition));
                    }
                }
            }
        }

        protected void TryAddKillMoves(Board board, List<MoveBase> moves, Stack<KillMove> strike, int row, int column)
        {
            bool isStopped = true;

            // try leftTop
            TryAddKillMove(row + 1, column - 1, row + 2, column - 2, ref isStopped,
                (nextColumn, nextRow) => nextColumn >= 0 && nextRow < board.Size, board, moves, strike);

            // try rightTop
            TryAddKillMove(row + 1, column + 1, row + 2, column + 2, ref isStopped,
                (nextColumn, nextRow) => nextColumn < board.Size && nextRow < board.Size, board, moves, strike);

            // try leftDown
            TryAddKillMove(row - 1, column - 1, row - 2, column - 2, ref isStopped,
                (nextColumn, nextRow) => nextColumn >= 0 && nextRow >= 0, board, moves, strike);

            // try rightDown
            TryAddKillMove(row - 1, column + 1, row - 2, column + 2, ref isStopped,
                (nextColumn, nextRow) => nextColumn < board.Size && nextRow >= 0, board, moves, strike);

            // if movement is finished
            StopKillMove(isStopped, board, moves, strike);
        }

        private void StopKillMove(bool isStopped, Board board, List<MoveBase> moves, Stack<KillMove> strike)
        {
            if (isStopped && strike.Any())
            {
                var resultMove = strike.First().Copy();
                var current = resultMove;
                var last = strike.Last();
                foreach (var nextMove in strike.Skip(1))
                {
                    // should also transform to King
                    if (nextMove == last && nextMove.Target.Row == board.Size - 1)
                    {
                        current.InnerMove = new KillTransformMove(nextMove.From, nextMove.Target, nextMove.Killed);
                    }
                    else
                    {
                        current.InnerMove = nextMove.Copy();
                    }
                    current = current.InnerMove;
                }
                moves.Add(resultMove);
            }
        }

        private void TryAddKillMove(int enemyRow, int enemyColumn, int nextRow, int nextColumn, ref bool isStopped,
            Func<int, int, bool> predicate, Board board, List<MoveBase> moves, Stack<KillMove> strike)
        {
            if (predicate(nextColumn, nextRow))
            {
                var enemyPosition = board.Positions[enemyRow, enemyColumn];
                if (enemyPosition != null
                    // there should be figure
                    && enemyPosition.Figure != null
                    // not from my team
                    && enemyPosition.Figure.Color != Color
                    // check if not want to kill the same position second time
                    && !strike.Any(s => s.Killed == enemyPosition))
                {
                    var nextPosition = board.Positions[nextRow, nextColumn];
                    if (nextPosition != null && nextPosition.Figure == null)
                    {
                        // move is continued
                        isStopped = false;
                        strike.Push(new KillMove(enemyPosition, nextPosition, enemyPosition));
                        TryAddKillMoves(board, moves, strike, nextRow, nextColumn);
                        strike.Pop();
                    }
                }
            }
        }
    }
}
