namespace Checkers.Models
{
    public abstract class Move
    {
        public Position From { get; set; }
        public Position Target { get; set; }
        public abstract void MakeMove(Board board);
    }
}
