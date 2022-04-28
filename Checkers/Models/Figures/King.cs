using Checkers.Enums;

namespace Checkers.Models
{
    public abstract class King : Figure
    {
        protected King(FigureColor color) : base(color) { }

        public override List<MoveBase> GetAvailableMoves(Board board)
        {
            var moves = new List<MoveBase>();

            TryAddKillMoves(board, moves);
            if (!moves.Any())
            {
                TryAddNormalMoves(board, moves);
            }

            return moves;
        }

        public override string ToString()
        {
            return "King";
        }

        #region Normal move

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

        #endregion

        #region Kill move

        private void TryAddKillMoves(Board board, List<MoveBase> moves)
        {
            var firstKills = new List<(bool, KillMove)> 
            {
                // move left top
                TryAddFirstKillMove(row => row + 1, column => column - 1, board),
                // move right top
                TryAddFirstKillMove(row => row + 1, column => column + 1, board),
                // move left down
                TryAddFirstKillMove(row => row - 1, column => column - 1, board),
                // move right down
                TryAddFirstKillMove(row => row - 1, column => column + 1, board)
            };

            foreach (var firstKill in firstKills)
            {
                if (firstKill.Item1)
                {
                    TryAddKillMovesWithStrike(board, moves, new List<KillMove> { firstKill.Item2 }, 
                        firstKill.Item2.Target.Row, firstKill.Item2.Target.Column);
                }
            }
        }

        private (bool, KillMove) TryAddFirstKillMove(Func<int, int> rowChange, Func<int, int> columnChange, Board board)
        {
            var enemyRow = rowChange(CurrentPosition.Row);
            var enemyColumn = columnChange(CurrentPosition.Column);
            var nextRow = rowChange(enemyRow);
            var nextColumn = columnChange(enemyColumn);

            while (nextRow <= board.Size - 1
                && nextRow >= 0
                && nextColumn <= board.Size - 1
                && nextColumn >= 0
                && board.Positions[enemyRow, enemyColumn].IsEmpty())
            {
                enemyRow = rowChange(enemyRow);
                enemyColumn = columnChange(enemyColumn);
                nextRow = rowChange(nextRow);
                nextColumn = columnChange(nextColumn);
            }

            if (nextRow >= board.Size
                || nextRow < 0
                || nextColumn >= board.Size
                || nextColumn < 0
                || board.Positions[enemyRow, enemyColumn].IsEmpty()
                || board.Positions[enemyRow, enemyColumn].Figure.Color == Color
                || !board.Positions[nextRow, nextColumn].IsEmpty())
            {
                return (false, null);
            }

            return (true, new KillMove(CurrentPosition, board.Positions[nextRow, nextColumn], board.Positions[enemyRow, enemyColumn]));
        }

        private void TryAddKillMovesWithStrike(Board board, List<MoveBase> moves, List<KillMove> strike, int currentRow, int currentColumn)
        {
            bool isStopped = true;
            var currentPosition = board.Positions[currentRow, currentColumn];

            // try leftTop
            TryAddKillMoveWithStrike(currentRow + 1, currentColumn - 1, currentRow + 2, currentColumn - 2, ref isStopped,
                currentPosition, (nextColumn, nextRow) => nextColumn >= 0 && nextRow < board.Size, board, moves, strike);

            // try rightTop
            TryAddKillMoveWithStrike(currentRow + 1, currentColumn + 1, currentRow + 2, currentColumn + 2, ref isStopped,
                currentPosition, (nextColumn, nextRow) => nextColumn < board.Size && nextRow < board.Size, board, moves, strike);

            // try leftDown
            TryAddKillMoveWithStrike(currentRow - 1, currentColumn - 1, currentRow - 2, currentColumn - 2, ref isStopped,
                currentPosition, (nextColumn, nextRow) => nextColumn >= 0 && nextRow >= 0, board, moves, strike);

            // try rightDown
            TryAddKillMoveWithStrike(currentRow - 1, currentColumn + 1, currentRow - 2, currentColumn + 2, ref isStopped,
                currentPosition, (nextColumn, nextRow) => nextColumn < board.Size && nextRow >= 0, board, moves, strike);

            // if movement is finished
            StopKillMove(isStopped, moves, strike);
        }

        private void StopKillMove(bool isStopped, List<MoveBase> moves, List<KillMove> strike)
        {
            if (isStopped && strike.Any())
            {
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

        private void TryAddKillMoveWithStrike(int enemyRow, int enemyColumn, int nextRow, int nextColumn, ref bool isStopped,
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
                    if (nextPosition != null && (nextPosition.Figure == null || nextPosition.Figure == this))
                    {
                        // move is continued
                        isStopped = false;
                        strike.Add(new KillMove(currentPosition, nextPosition, enemyPosition));
                        TryAddKillMovesWithStrike(board, moves, strike, nextRow, nextColumn);
                        strike.RemoveAt(strike.Count - 1);
                    }
                }
            }
        }

        #endregion
    }
}
