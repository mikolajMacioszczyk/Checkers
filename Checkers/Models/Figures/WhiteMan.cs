using Checkers.Enums;

namespace Checkers.Models
{
    public class WhiteMan : Figure
    {
        public WhiteMan() : base(FigureColor.White) {}

        public override List<MoveBase> GetAvailableMoves(Board board)
        {
            var moves = new List<MoveBase>();

            TryAddNormalMoves(board, moves);
            TryAddKillMoves(board, moves, new Stack<KillMove>(), CurrentPosition.Row, CurrentPosition.Column);

            return moves;
        }

        private void TryAddNormalMoves(Board board, List<MoveBase> moves)
        {
            var nextRow = CurrentPosition.Row + 1;

            // Man cannot stand at the edge of board so CurrentPosition.Row + 1 must be in the board

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

        private void TryAddNormalMove(int nextRow, int nextColumn, Board board, List<MoveBase> moves)
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

        private void TryAddKillMoves(Board board, List<MoveBase> moves, Stack<KillMove> strike, int row, int column)
        {
            bool isStopped = true;

            // try leftTop
            var enemyRow = row + 1;
            var enemyColumn = column - 1;
            var nextRow = row + 2;
            var nextColumn = column - 2;

            if (enemyColumn >= 1 && nextRow < board.Size)
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

            // try rightTop

            // try leftDown

            // try rightDown

            // if movement is finished
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
    }
}
