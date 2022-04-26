namespace Checkers.Models
{
    public class KillMove : NormalMove
    {
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
