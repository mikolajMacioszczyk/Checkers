namespace Checkers.Models
{
    public class TransformMove : NormalMove
    {
        public override void MakeMove(Board board)
        {
            base.MakeMove(board);
            var newFigure = new King(Target.Figure.Color);
            Target.Figure = newFigure;
        }
    }
}
