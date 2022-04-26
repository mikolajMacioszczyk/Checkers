namespace Checkers.Models
{
    public class NormalMove : Move
    {
        public NormalMove(Position from, Position target) : base(from, target) {}

        public override void MakeMove(Board board)
        {
            // TODO: Only Debug
            if (From.Figure == null)
            {
                throw new ArgumentException(nameof(From.Figure));
            }

            Target.Figure = From.Figure;
            From.Figure = null;
        }
    }
}
