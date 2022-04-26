namespace Checkers.Models
{
    public class KillMove : NormalMove
    {
        public KillMove(Position from, Position target) : base(from, target) { }

        public Move InnerMove { get; set; }
        public Position Killed { get; set; }

        public override void MakeMove(Board board)
        {
            base.MakeMove(board);

            board.MarkKilled(Killed);
            
            InnerMove.MakeMove(board);
        }
    }
}
