namespace Checkers.Models
{
    public class TransformMove : NormalMove
    {
        public TransformMove(Position from, Position target) : base(from, target) { }

        public override void MakeMove(Board board)
        {
            base.MakeMove(board);
            Figure newFigure = Target.Figure.Color == Enums.FigureColor.White ? new WhiteKing() : new BlackKing();
            board.Positions[Target.Row, Target.Column].Figure = newFigure;
        }

        public override void UndoMove(Board board)
        {
            Figure oldFigure = board.Positions[Target.Row, Target.Column].Figure.Color == Enums.FigureColor.White ? new WhiteMan() : new BlackMan();
            board.Positions[Target.Row, Target.Column].Figure = oldFigure;
            base.UndoMove(board);
        }

        public override bool Equals(object? obj)
        {
            return obj is TransformMove && base.Equals(obj);
        }

        public override string Print(Board board)
        {
            return $"Move {board.Positions[From.Row, From.Column].Figure} from {From} to {Target}, tranforming to King";
        }
    }
}
