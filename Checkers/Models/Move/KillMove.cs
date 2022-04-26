namespace Checkers.Models
{
    public class KillMove : NormalMove
    {
        public KillMove(Position from, Position target, Position killed) : base(from, target) { 
            Killed = killed;
        }

        public KillMove InnerMove { get; set; }
        public Position Killed { get; set; }

        public override void MakeMove(Board board)
        {
            base.MakeMove(board);

            board.MarkKilled(Killed);
            
            InnerMove.MakeMove(board);
        }

        public virtual KillMove Copy()
        {
            return new KillMove(From, Target, Killed) { InnerMove = InnerMove?.Copy() };
        }
    }
}
