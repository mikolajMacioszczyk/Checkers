namespace Checkers.Models
{
    public class KillMove : NormalMove
    {
        public override bool IsKillMove => true;

        public KillMove(Position from, Position target, Position killed) : base(from, target) { 
            Killed = killed;
        }

        public KillMove InnerMove { get; set; }
        public Position Killed { get; set; }

        private Figure KilledFigure;

        public override void MakeMove(Board board)
        {
            base.MakeMove(board);

            KilledFigure = board.Positions[Killed.Row, Killed.Column].Figure;
            board.MarkKilled(board.Positions[Killed.Row, Killed.Column]);
            
            InnerMove?.MakeMove(board);
        }

        public override void UndoMove(Board board)
        {
            InnerMove?.UndoMove(board);

            board.RestoreKilled(KilledFigure, board.Positions[Killed.Row, Killed.Column]);
            KilledFigure = null;
            
            base.UndoMove(board);
        }

        public virtual KillMove Copy()
        {
            return new KillMove(From, Target, Killed) { InnerMove = InnerMove?.Copy() };
        }

        public override bool Equals(object? obj)
        {
            return obj is KillMove move &&
                   base.Equals(obj) &&
                   EqualityComparer<KillMove>.Default.Equals(InnerMove, move.InnerMove) &&
                   EqualityComparer<Position>.Default.Equals(Killed, move.Killed) &&
                   EqualityComparer<Figure>.Default.Equals(KilledFigure, move.KilledFigure);
        }
    }
}
