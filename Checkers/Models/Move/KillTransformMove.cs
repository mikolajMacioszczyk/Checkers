﻿namespace Checkers.Models
{
    public class KillTransformMove : KillMove
    {
        public KillTransformMove(Position from, Position target, Position killed) : base(from, target, killed) { }

        public override void MakeMove(Board board)
        {
            base.MakeMove(board);

            Figure newFigure = Target.Figure.Color == Enums.FigureColor.White ? new WhiteKing() : new BlackKing();

            Target.Figure = newFigure;
        }

        public override KillMove Copy()
        {
            return new KillTransformMove(From, Target, Killed) { InnerMove = InnerMove?.Copy(), Killed = Killed};
        }
    }
}
