namespace Checkers.Models
{
    public class KillTransformMove : KillMove
    {
        public KillTransformMove(Position from, Position target, Position killed) : base(from, target, killed) { }

        public override void MakeMove(Board board)
        {
            base.MakeMove(board);

            Figure newFigure = board.Positions[Target.Row, Target.Column].Figure.Color == Enums.FigureColor.White ? new WhiteKing() : new BlackKing();

            board.Positions[Target.Row, Target.Column].Figure = newFigure;
        }

        public override void UndoMove(Board board)
        {
            Figure oldFigure = board.Positions[Target.Row, Target.Column].Figure.Color == Enums.FigureColor.White ? new WhiteMan() : new BlackMan();

            board.Positions[Target.Row, Target.Column].Figure = oldFigure;

            base.UndoMove(board);
        }

        public override KillMove Copy()
        {
            return new KillTransformMove(From, Target, Killed) { InnerMove = InnerMove?.Copy(), Killed = Killed};
        }

        public override bool Equals(object? obj)
        {
            return obj is KillTransformMove move && base.Equals(obj);
        }
    }
}
