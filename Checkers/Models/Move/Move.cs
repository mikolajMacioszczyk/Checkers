namespace Checkers.Models
{
    public abstract class Move
    {
        public Position From { get; set; }
        public Position Target { get; set; }

        public Move(Position from, Position target)
        {
            From = from;
            Target = target;
        }

        public abstract void MakeMove(Board board);
    }
}
