namespace Checkers.Models
{
    public class NormalMove : MoveBase
    {

        public NormalMove(Position from, Position target) : base(from, target) {}

        public override bool IsKillMove => false;

        public override void MakeMove(Board board)
        {
            board.Positions[Target.Row, Target.Column].Figure = board.Positions[From.Row, From.Column].Figure;
            board.Positions[From.Row, From.Column].Figure = null;
        }

        public override void UndoMove(Board board)
        {
            board.Positions[From.Row, From.Column].Figure = board.Positions[Target.Row, Target.Column].Figure;
            board.Positions[Target.Row, Target.Column].Figure = null;
        }

        public override bool Equals(object? obj)
        {
            return obj is NormalMove move &&
                   EqualityComparer<Position>.Default.Equals(From, move.From) &&
                   EqualityComparer<Position>.Default.Equals(Target, move.Target);
        }

        public override string Print(Board board)
        {
            return $"Move {board.Positions[From.Row, From.Column].Figure} from {From} to {Target}";
        }
    }
}
