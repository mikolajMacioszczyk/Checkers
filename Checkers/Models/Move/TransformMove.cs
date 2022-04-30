namespace Checkers.Models
{
    public class TransformMove : NormalMove
    {
        public TransformMove(Position from, Position target) : base(from, target) { }
        public Figure OldFigure { get; set; }

        public override void MakeMove(Board board)
        {
            base.MakeMove(board);
            OldFigure = board.Positions[Target.Row, Target.Column].Figure;
            Figure newFigure = board.Positions[Target.Row, Target.Column].Figure.Color == Enums.FigureColor.White ? new WhiteKing() : new BlackKing();
            board.Positions[Target.Row, Target.Column].Figure = newFigure;
            board.Replace(OldFigure, newFigure);
        }

        public override void UndoMove(Board board)
        {
            var removed = board.Positions[Target.Row, Target.Column].Figure;
            board.Positions[Target.Row, Target.Column].Figure = OldFigure;
            board.Replace(removed, OldFigure);
            OldFigure = null;
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
