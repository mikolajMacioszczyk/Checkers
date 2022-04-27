using Checkers.Managers;
using Checkers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CheckersTests.MovesTests
{
    public class WhiteManGetAvailableMovesTests
    {
        private Board board;
        private static readonly int Size = 8;
        private static readonly int FiguresCount = 12;

        public WhiteManGetAvailableMovesTests()
        {
            board = new Board();
            board.Reset(Size, FiguresCount);
        }

        [Fact]
        public void GetMoves_InitialPosition_Edge()
        {
            // arrange
            var figure = board.Positions[2, Size - 1].Figure;
            var expectedMoves = new List<MoveBase> {
                new NormalMove(board.Positions[2, Size - 1], board.Positions[3, Size - 2])
            };

            // act
            var resultMoves = figure.GetAvailableMoves(board);

            // assert
            Assert.Equal(expectedMoves.Count, resultMoves.Count);
            Assert.True(resultMoves.All(r => expectedMoves.Any(e => e.GetType() == r.GetType() && e.From == r.From && e.Target == r.Target)));
        }

        [Fact]
        public void GetMoves_InitialPosition_Center()
        {
            // arrange
            var figure = board.Positions[Size - 3, 2].Figure;
            var expectedMoves = new List<MoveBase> {
                new NormalMove(board.Positions[Size - 3, 2], board.Positions[Size - 4, 1]),
                new NormalMove(board.Positions[Size - 3, 2], board.Positions[Size - 4, 3])
            };

            // act
            var resultMoves = figure.GetAvailableMoves(board);

            // assert
            Assert.Equal(expectedMoves.Count, resultMoves.Count);
            Assert.True(resultMoves.All(r => expectedMoves.Any(e => e.GetType() == r.GetType() && e.From == r.From && e.Target == r.Target)));
        }

        [Fact]
        public void GetMoves_NoMoves()
        {
            // arrange
            var figure = board.Positions[0, 1].Figure;
            var expectedMoves = new List<MoveBase> {
            };

            // act
            var resultMoves = figure.GetAvailableMoves(board);

            // assert
            Assert.Equal(expectedMoves.Count, resultMoves.Count);
            Assert.True(resultMoves.All(r => expectedMoves.Any(e => e.GetType() == r.GetType() && e.From == r.From && e.Target == r.Target)));
        }

        [Fact]
        public void GetMoves_Transition()
        {
            board.Positions[Size-1, 2].Figure = null;
            board.Positions[Size - 2, 1].Figure = board.Positions[2, 1].Figure;
            board.Positions[Size - 3, 0].Figure = null;
            board.Positions[Size - 3, 2].Figure = null;

            // arrange
            var figure = board.Positions[Size - 2, 1].Figure;
            var expectedMoves = new List<MoveBase> {
                new TransformMove(board.Positions[Size - 2, 1], board.Positions[Size-1, 2]),
            };

            // act
            var resultMoves = figure.GetAvailableMoves(board);

            // assert
            Assert.Equal(expectedMoves.Count, resultMoves.Count);
            Assert.True(resultMoves.All(r => expectedMoves.Any(e => e.GetType() == r.GetType() && e.From == r.From && e.Target == r.Target)));
        }
    }
}
