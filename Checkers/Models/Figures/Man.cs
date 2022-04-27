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

            TryAddKillMoves(board, moves, new List<KillMove>(), CurrentPosition.Row, CurrentPosition.Column);
            if (!moves.Any())
            {
                TryAddNormalMoves(board, moves);
            }

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

        protected void TryAddKillMoves(Board board, List<MoveBase> moves, List<KillMove> strike, int row, int column)
        {
            bool isStopped = true;
            var currentPosition = board.Positions[row, column];

            // try leftTop
            TryAddKillMove(row + 1, column - 1, row + 2, column - 2, ref isStopped,
                currentPosition, (nextColumn, nextRow) => nextColumn >= 0 && nextRow < board.Size, board, moves, strike);

            // try rightTop
            TryAddKillMove(row + 1, column + 1, row + 2, column + 2, ref isStopped,
                currentPosition, (nextColumn, nextRow) => nextColumn < board.Size && nextRow < board.Size, board, moves, strike);

            // try leftDown
            TryAddKillMove(row - 1, column - 1, row - 2, column - 2, ref isStopped,
                currentPosition, (nextColumn, nextRow) => nextColumn >= 0 && nextRow >= 0, board, moves, strike);

            // try rightDown
            TryAddKillMove(row - 1, column + 1, row - 2, column + 2, ref isStopped,
                currentPosition, (nextColumn, nextRow) => nextColumn < board.Size && nextRow >= 0, board, moves, strike);

            // if movement is finished
            StopKillMove(isStopped, board, moves, strike);
        }

        private void StopKillMove(bool isStopped, Board board, List<MoveBase> moves, List<KillMove> strike)
        {
            if (isStopped && strike.Any())
            {
                var last = strike.Last();
                if (last.Target.Row == board.Size - 1 || last.Target.Row == 0)
                {
                    // should also transform to King
                    strike.Remove(last);
                    strike.Add(new KillTransformMove(last.From, last.Target, last.Killed));
                }

                var resultMove = strike.First().Copy();
                var current = resultMove;
                
                foreach (var nextMove in strike.Skip(1))
                {
                    current.InnerMove = nextMove.Copy();
                    current = current.InnerMove;
                }
                moves.Add(resultMove);
            }
        }

        private void TryAddKillMove(int enemyRow, int enemyColumn, int nextRow, int nextColumn, ref bool isStopped,
            Position currentPosition, Func<int, int, bool> predicate, Board board, List<MoveBase> moves, List<KillMove> strike)
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
                        strike.Add(new KillMove(currentPosition, nextPosition, enemyPosition));
                        TryAddKillMoves(board, moves, strike, nextRow, nextColumn);
                        strike.RemoveAt(strike.Count - 1);
                    }
                }
            }
        }
    }
}
