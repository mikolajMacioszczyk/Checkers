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

            KilledFigure = Killed.Figure;
            board.MarkKilled(Killed);
            
            InnerMove?.MakeMove(board);
        }

        public override void UndoMove(Board board)
        {
            InnerMove?.UndoMove(board);

            board.RestoreKilled(KilledFigure, Killed);
            KilledFigure = null;
            
            base.UndoMove(board);
        }

        public virtual KillMove Copy()
        {
            return new KillMove(From, Target, Killed) { InnerMove = InnerMove?.Copy() };
        }
    }
}
