namespace Checkers.Models
{
    public abstract class MoveBase
    {
        public Position From { get; set; }
        public Position Target { get; set; }

        public MoveBase(Position from, Position target)
        {
            From = from;
            Target = target;
        }

        public abstract void MakeMove(Board board);
    }
}
