namespace Checkers.Models
{
    public class TransformMove : NormalMove
    {
        public TransformMove(Position from, Position target) : base(from, target) { }

        public override void MakeMove(Board board)
        {
            base.MakeMove(board);
            Figure newFigure = Target.Figure.Color == Enums.FigureColor.White ? new WhiteKing() : new BlackKing();
            Target.Figure = newFigure;
        }
    }
}
