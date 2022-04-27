using Checkers.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CheckersTests.MovesTests
{
    public class KingGetAvailableMovesTests
    {
        private Board board;
        private static readonly int Size = 8;
        private static readonly int FiguresCount = 12;

        public KingGetAvailableMovesTests()
        {
            board = new Board();
            board.Reset(Size, FiguresCount);
        }

        [Fact]
        public void GetMoves_King_OneDirection()
        {
            // arrange
            var kingPosition = board.Positions[2, 7];
            var king = new WhiteKing();
            kingPosition.Figure = king;

            var expectedMoves = new List<MoveBase> {
                new NormalMove(kingPosition, board.Positions[3, 6]),
                new NormalMove(kingPosition, board.Positions[4, 5]),
            };

            // act
            var resultMoves = king.GetAvailableMoves(board);

            // assert
            Assert.Equal(expectedMoves.Count, resultMoves.Count);
            Assert.True(resultMoves.All(r => expectedMoves.Any(e => e.GetType() == r.GetType() && e.From == r.From && e.Target == r.Target)));
        }

        [Fact]
        public void GetMoves_King_MultipleDirection()
        {
            // arrange
            board.Positions[1, 6].Figure = null;
            var kingPosition = board.Positions[2, 5];
            var king = new WhiteKing();
            kingPosition.Figure = king;

            var expectedMoves = new List<MoveBase> {
                new NormalMove(kingPosition, board.Positions[3, 4]),
                new NormalMove(kingPosition, board.Positions[4, 3]),
                new NormalMove(kingPosition, board.Positions[3, 6]),
                new NormalMove(kingPosition, board.Positions[4, 7]),
                new NormalMove(kingPosition, board.Positions[1, 6]),
            };

            // act
            var resultMoves = king.GetAvailableMoves(board);

            // assert
            Assert.Equal(expectedMoves.Count, resultMoves.Count);
            Assert.True(resultMoves.All(r => expectedMoves.Any(e => e.GetType() == r.GetType() && e.From == r.From && e.Target == r.Target)));
        }

        [Fact]
        public void GetMoves_King_NoMoves()
        {
            // arrange
            var kingPosition = board.Positions[1, 2];
            var king = new WhiteKing();
            kingPosition.Figure = king;

            var expectedMoves = new List<MoveBase> {};

            // act
            var resultMoves = king.GetAvailableMoves(board);

            // assert
            Assert.Equal(expectedMoves.Count, resultMoves.Count);
            Assert.True(resultMoves.All(r => expectedMoves.Any(e => e.GetType() == r.GetType() && e.From == r.From && e.Target == r.Target)));
        }
    }
}
