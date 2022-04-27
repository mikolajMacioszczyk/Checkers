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

        [Fact]
        public void GetMoves_King_SingleShortKill()
        {
            // arrange
            board.Positions[3, 2].Figure = board.Positions[5, 0].Figure;
            board.Positions[2, 3].Figure = new WhiteKing();

            var startFigure = board.Positions[2, 3].Figure;

            // path 1
            var path1killFrom1 = board.Positions[2, 3];
            var path1killedPosition1 = board.Positions[3, 2];
            var path1killTarget1 = board.Positions[4, 1];

            var expectedMoves = new List<MoveBase> {
                new KillMove(path1killFrom1, path1killTarget1, path1killedPosition1)
            };

            // act
            var resultMoves = startFigure.GetAvailableMoves(board);

            // assert
            Assert.Equal(expectedMoves.Count, resultMoves.Count);
            Assert.True(resultMoves.All(r => expectedMoves.Any(e => e.GetType() == r.GetType() && e.From == r.From && e.Target == r.Target)));
            var path1kill1 = (resultMoves[0] as KillMove);
            Assert.Equal(path1kill1.From, path1killFrom1);
            Assert.Equal(path1kill1.Target, path1killTarget1);
            Assert.Equal(path1kill1.Killed, path1killedPosition1);
            Assert.Null(path1kill1.InnerMove);
        }

        [Fact]
        public void GetMoves_King_SingleLongKill()
        {
            // arrange
            board.Positions[4, 1].Figure = board.Positions[5, 0].Figure;
            board.Positions[2, 3].Figure = new WhiteKing();

            var startFigure = board.Positions[2, 3].Figure;

            // path 1
            var path1killFrom1 = board.Positions[2, 3];
            var path1killedPosition1 = board.Positions[4, 1];
            var path1killTarget1 = board.Positions[5, 0];

            var expectedMoves = new List<MoveBase> {
                new KillMove(path1killFrom1, path1killTarget1, path1killedPosition1)
            };

            // act
            var resultMoves = startFigure.GetAvailableMoves(board);

            // assert
            Assert.Equal(expectedMoves.Count, resultMoves.Count);
            Assert.True(resultMoves.All(r => expectedMoves.Any(e => e.GetType() == r.GetType() && e.From == r.From && e.Target == r.Target)));
            var path1kill1 = (resultMoves[0] as KillMove);
            Assert.Equal(path1kill1.From, path1killFrom1);
            Assert.Equal(path1kill1.Target, path1killTarget1);
            Assert.Equal(path1kill1.Killed, path1killedPosition1);
            Assert.Null(path1kill1.InnerMove);
        }

        [Fact]
        public void GetMoves_King_SingleKillStrike()
        {
            // arrange
            board.Positions[4, 1].Figure = board.Positions[5, 0].Figure;
            board.Positions[7, 2].Figure = null;
            board.Positions[2, 3].Figure = new WhiteKing();

            var startFigure = board.Positions[2, 3].Figure;

            // path 1
            var path1killFrom1 = board.Positions[2, 3];
            var path1killedPosition1 = board.Positions[4, 1];
            var path1killTarget1 = board.Positions[5, 0];

            var path1killFrom2 = board.Positions[5, 0];
            var path1killedPosition2 = board.Positions[6, 1];
            var path1killTarget2 = board.Positions[7, 2];

            var expectedMoves = new List<MoveBase> {
                new KillMove(path1killFrom1, path1killTarget1, path1killedPosition1)
                {
                    InnerMove = new KillMove(path1killFrom2, path1killTarget2, path1killedPosition2)
                }
            };

            // act
            var resultMoves = startFigure.GetAvailableMoves(board);

            // assert
            Assert.Equal(expectedMoves.Count, resultMoves.Count);
            Assert.True(resultMoves.All(r => expectedMoves.Any(e => e.GetType() == r.GetType() && e.From == r.From && e.Target == r.Target)));
            var path1kill1 = (resultMoves[0] as KillMove);
            Assert.Equal(path1kill1.From, path1killFrom1);
            Assert.Equal(path1kill1.Target, path1killTarget1);
            Assert.Equal(path1kill1.Killed, path1killedPosition1);
            var path1kill2 = path1kill1.InnerMove;
            Assert.Equal(path1kill2.From, path1killFrom2);
            Assert.Equal(path1kill2.Target, path1killTarget2);
            Assert.Equal(path1kill2.Killed, path1killedPosition2);
            Assert.Null(path1kill2.InnerMove);
        }

        [Fact]
        public void GetMoves_King_MultipleKillStrike()
        {
            // arrange
            board.Positions[4, 1].Figure = board.Positions[5, 0].Figure;
            board.Positions[7, 2].Figure = null;
            board.Positions[4, 5].Figure = board.Positions[5, 6].Figure;
            board.Positions[2, 3].Figure = new WhiteKing();

            var startFigure = board.Positions[2, 3].Figure;

            // path 1
            var path1killFrom1 = board.Positions[2, 3];
            var path1killedPosition1 = board.Positions[4, 1];
            var path1killTarget1 = board.Positions[5, 0];

            var path1killFrom2 = board.Positions[5, 0];
            var path1killedPosition2 = board.Positions[6, 1];
            var path1killTarget2 = board.Positions[7, 2];

            // path 2
            var path2killFrom1 = board.Positions[2, 3];
            var path2killedPosition1 = board.Positions[4, 5];
            var path2killTarget1 = board.Positions[5, 6];

            var expectedMoves = new List<MoveBase> {
                new KillMove(path1killFrom1, path1killTarget1, path1killedPosition1)
                {
                    InnerMove = new KillMove(path1killFrom2, path1killTarget2, path1killedPosition2)
                },
                new KillMove(path2killFrom1, path2killTarget1, path2killedPosition1)
            };

            // act
            var resultMoves = startFigure.GetAvailableMoves(board);

            // assert
            Assert.Equal(expectedMoves.Count, resultMoves.Count);
            Assert.True(resultMoves.All(r => expectedMoves.Any(e => e.GetType() == r.GetType() && e.From == r.From && e.Target == r.Target)));
            var path1kill1 = (resultMoves[0] as KillMove);
            Assert.Equal(path1kill1.From, path1killFrom1);
            Assert.Equal(path1kill1.Target, path1killTarget1);
            Assert.Equal(path1kill1.Killed, path1killedPosition1);
            var path1kill2 = path1kill1.InnerMove;
            Assert.Equal(path1kill2.From, path1killFrom2);
            Assert.Equal(path1kill2.Target, path1killTarget2);
            Assert.Equal(path1kill2.Killed, path1killedPosition2);
            Assert.Null(path1kill2.InnerMove);

            var path2kill1 = (resultMoves[1] as KillMove);
            Assert.Equal(path2kill1.From, path2killFrom1);
            Assert.Equal(path2kill1.Target, path2killTarget1);
            Assert.Equal(path2kill1.Killed, path2killedPosition1);
            Assert.Null(path2kill1.InnerMove);
        }
    }
}
