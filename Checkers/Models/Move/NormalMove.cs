namespace Checkers.Models
{
    public class NormalMove : MoveBase
    {

        public NormalMove(Position from, Position target) : base(from, target) {}

        public override bool IsKillMove => false;

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

        public override void UndoMove(Board board)
        {
            From.Figure = Target.Figure;
            Target.Figure = null;
        }
    }
}
