﻿using Checkers.Enums;

namespace Checkers.Models
{
    public class BlackMan : Man
    {
        public BlackMan() : base(FigureColor.Black) { }

        public override Figure Copy()
        {
            return new BlackMan() { CurrentPosition = CurrentPosition};
        }

        protected override void TryAddNormalMoves(Board board, List<MoveBase> moves)
        {
            var nextRow = CurrentPosition.Row - 1;

            // Man cannot stand at the edge of board so CurrentPosition.Row - 1 must be in the board
            TryAddNormalMove(nextRow, CurrentPosition.Column - 1, board, moves, nextColumn => nextColumn >= 0);

            TryAddNormalMove(nextRow, CurrentPosition.Column + 1, board, moves, nextColumn => nextColumn < board.Size);
        }
    }
}
