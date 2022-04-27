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
        public void GetMoves_Man_InitialPosition_Edge()
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
        public void GetMoves_Man_InitialPosition_Center()
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
        public void GetMoves_Man_NoMoves()
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
        public void GetMoves_Man_Transition()
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

        [Fact]
        public void GetMoves_Man_SingleKill()
        {
            // arrange
            board.Positions[3, Size - 2].Figure = board.Positions[Size - 3, 4].Figure;

            var figure = board.Positions[2, Size - 1].Figure;
            var killedPosition = board.Positions[3, Size - 2];
            var expectedMoves = new List<MoveBase> {
                new KillMove(figure.CurrentPosition, board.Positions[4, Size - 3], killedPosition),
            };

            // act
            var resultMoves = figure.GetAvailableMoves(board);

            // assert
            Assert.Equal(expectedMoves.Count, resultMoves.Count);
            Assert.True(resultMoves.All(r => expectedMoves.Any(e => e.GetType() == r.GetType() && e.From == r.From && e.Target == r.Target)));
            Assert.Equal((resultMoves[0] as KillMove).Killed, killedPosition);
            Assert.Null((resultMoves[0] as KillMove).InnerMove);
        }

        [Fact]
        public void GetMoves_Man_StrikeKill_SinglePath()
        {
            // arrange
            board.Positions[3, Size - 2].Figure = board.Positions[Size - 2, 3].Figure;

            var figure = board.Positions[2, Size - 1].Figure;
            var killedPosition1 = board.Positions[3, Size - 2];
            var killedPosition2 = board.Positions[5, 4];
            var killedPosition3 = board.Positions[5, 2];

            var kill2From = board.Positions[4, 5];
            var kill3From = board.Positions[6, 3];

            var kill2Target = board.Positions[6, 3];
            var kill3Target = board.Positions[4, 1];

            var expectedMoves = new List<MoveBase> {
                new KillMove(figure.CurrentPosition, board.Positions[4, Size - 3], killedPosition1){
                    InnerMove = new KillMove(kill2From, kill2Target, killedPosition2)
                    {
                        InnerMove = new KillMove(kill3From, kill3Target, killedPosition3)
                    }
                },
            };

            // act
            var resultMoves = figure.GetAvailableMoves(board);

            // assert
            Assert.Equal(expectedMoves.Count, resultMoves.Count);
            Assert.True(resultMoves.All(r => expectedMoves.Any(e => e.GetType() == r.GetType() && e.From == r.From && e.Target == r.Target)));
            var kill1 = (resultMoves[0] as KillMove);
            Assert.Equal(kill1.Killed, killedPosition1);
            Assert.NotNull(kill1.InnerMove);
            var kill2 = kill1.InnerMove;
            Assert.Equal(kill2.Killed, killedPosition2);
            Assert.Equal(kill2.From, kill2From);
            Assert.Equal(kill2.Target, kill2Target);
            Assert.NotNull(kill2.InnerMove);
            var kill3 = kill2.InnerMove;
            Assert.Equal(kill3.Killed, killedPosition3);
            Assert.Equal(kill3.From, kill3From);
            Assert.Equal(kill3.Target, kill3Target);
            Assert.Null(kill3.InnerMove);
        }

        [Fact]
        public void GetMoves_Man_KillWithTransition()
        {
            // arrange
            board.Positions[5, 0].Figure = board.Positions[1, 0].Figure;
            board.Positions[7, 2].Figure = null;

            var killFrom = board.Positions[5, 0];
            var killedPosition = board.Positions[6, 1];
            var killTarget = board.Positions[7, 2];
            var startFigure = killFrom.Figure;


            var expectedMoves = new List<MoveBase> {
                new KillTransformMove(killFrom, killTarget, killedPosition)
            };

            // act
            var resultMoves = startFigure.GetAvailableMoves(board);

            // assert
            Assert.Equal(expectedMoves.Count, resultMoves.Count);
            Assert.True(resultMoves.All(r => expectedMoves.Any(e => e.GetType() == r.GetType() && e.From == r.From && e.Target == r.Target)));
            var kill1 = (resultMoves[0] as KillTransformMove);
            Assert.Equal(kill1.Killed, killedPosition);
            Assert.Equal(kill1.From, killFrom);
            Assert.Equal(kill1.Target, killTarget);
            Assert.Null(kill1.InnerMove);
        }

        [Fact]
        public void GetMoves_Man_StrikeKillWithTransition()
        {
            // arrange
            board.Positions[4, 1].Figure = board.Positions[5, 0].Figure;
            board.Positions[3, 2].Figure = board.Positions[2, 3].Figure;
            board.Positions[0, 5].Figure = null;

            var startFigure = board.Positions[4, 1].Figure;

            var killedPosition1 = board.Positions[3, 2];
            var killedPosition2 = board.Positions[1, 4];

            var kill2From = board.Positions[2, 3];
            var kill2Target = board.Positions[0, 5];

            var expectedMoves = new List<MoveBase> {
                new KillMove(startFigure.CurrentPosition, board.Positions[2, 3], killedPosition1){
                    InnerMove = new KillTransformMove(kill2From, kill2Target, killedPosition2)
                },
            };

            // act
            var resultMoves = startFigure.GetAvailableMoves(board);

            // assert
            Assert.Equal(expectedMoves.Count, resultMoves.Count);
            Assert.True(resultMoves.All(r => expectedMoves.Any(e => e.GetType() == r.GetType() && e.From == r.From && e.Target == r.Target)));
            var kill1 = (resultMoves[0] as KillMove);
            Assert.Equal(kill1.Killed, killedPosition1);
            var kill2 = kill1.InnerMove as KillTransformMove;
            Assert.NotNull(kill2);
            Assert.Equal(kill2.Killed, killedPosition2);
            Assert.Equal(kill2.From, kill2From);
            Assert.Equal(kill2.Target, kill2Target);
            Assert.Null(kill2.InnerMove);
        }

        [Fact]
        public void GetMoves_Man_StrikeKill_MuliplePath()
        {
            // arrange
            board.Positions[4, 3].Figure = board.Positions[5, 4].Figure;
            board.Positions[3, 2].Figure = board.Positions[2, 1].Figure;
            board.Positions[3, 4].Figure = board.Positions[2, 5].Figure;
            board.Positions[0, 3].Figure = null;

            var startFigure = board.Positions[4, 3].Figure;

            // path 1
            var path1killFrom1 = board.Positions[4, 3];
            var path1killedPosition1 = board.Positions[3, 2];
            var path1killTarget1 = board.Positions[2, 1];

            var path1killFrom2 = board.Positions[2, 1];
            var path1killedPosition2 = board.Positions[1, 2];
            var path1killTarget2 = board.Positions[0, 3];

            var path1killFrom3 = board.Positions[0, 3];
            var path1killedPosition3 = board.Positions[1, 4];
            var path1killTarget3 = board.Positions[2, 5];

            var path1killFrom4 = board.Positions[2, 5];
            var path1killedPosition4 = board.Positions[3, 4];
            var path1killTarget4 = board.Positions[4, 3];

            // path 2
            var path2killFrom1 = board.Positions[4, 3];
            var path2killedPosition1 = board.Positions[3, 4];
            var path2killTarget1 = board.Positions[2, 5];

            var path2killFrom2 = board.Positions[2, 5];
            var path2killedPosition2 = board.Positions[1, 4];
            var path2killTarget2 = board.Positions[0, 3];

            var path2killFrom3 = board.Positions[0, 3];
            var path2killedPosition3 = board.Positions[1, 2];
            var path2killTarget3 = board.Positions[2, 1];

            var path2killFrom4 = board.Positions[2, 1];
            var path2killedPosition4 = board.Positions[3, 2];
            var path2killTarget4 = board.Positions[4, 3];

            var expectedMoves = new List<MoveBase> {
                new KillMove(path1killFrom1, path1killTarget1, path1killedPosition1){
                    InnerMove = new KillMove(path1killFrom2, path1killTarget2, path1killedPosition2)
                    {
                        InnerMove = new KillMove(path1killFrom3, path1killTarget3, path1killedPosition3)
                        {
                            InnerMove = new KillMove(path1killFrom4, path1killTarget4, path1killedPosition4)
                        }
                    }
                },
                new KillMove(path2killFrom1, path2killTarget1, path2killedPosition1){
                    InnerMove = new KillMove(path2killFrom2, path2killTarget2, path2killedPosition2)
                    {
                        InnerMove = new KillMove(path2killFrom3, path2killTarget3, path2killedPosition3)
                        {
                            InnerMove = new KillMove(path2killFrom4, path2killTarget4, path2killedPosition4)
                        }
                    }
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
            var path1kill3 = path1kill2.InnerMove;
            Assert.Equal(path1kill3.From, path1killFrom3);
            Assert.Equal(path1kill3.Target, path1killTarget3);
            Assert.Equal(path1kill3.Killed, path1killedPosition3);
            var path1kill4 = path1kill3.InnerMove;
            Assert.Equal(path1kill4.From, path1killFrom4);
            Assert.Equal(path1kill4.Target, path1killTarget4);
            Assert.Equal(path1kill4.Killed, path1killedPosition4);
            Assert.Null(path1kill4.InnerMove);

            var path2kill1 = (resultMoves[1] as KillMove);
            Assert.Equal(path2kill1.From, path2killFrom1);
            Assert.Equal(path2kill1.Target, path2killTarget1);
            Assert.Equal(path2kill1.Killed, path2killedPosition1);
            var path2kill2 = path2kill1.InnerMove;
            Assert.Equal(path2kill2.From, path2killFrom2);
            Assert.Equal(path2kill2.Target, path2killTarget2);
            Assert.Equal(path2kill2.Killed, path2killedPosition2);
            var path2kill3 = path2kill2.InnerMove;
            Assert.Equal(path2kill3.From, path2killFrom3);
            Assert.Equal(path2kill3.Target, path2killTarget3);
            Assert.Equal(path2kill3.Killed, path2killedPosition3);
            var path2kill4 = path2kill3.InnerMove;
            Assert.Equal(path2kill4.From, path2killFrom4);
            Assert.Equal(path2kill4.Target, path2killTarget4);
            Assert.Equal(path2kill4.Killed, path2killedPosition4);
            Assert.Null(path2kill4.InnerMove);
        }
    }
}
