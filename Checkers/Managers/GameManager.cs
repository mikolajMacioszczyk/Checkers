using Checkers.Enums;
using Checkers.Models;
using Checkers.Models.Player;

namespace Checkers.Managers
{
    public class GameManager
    {
        public static readonly int BoardSize = 8;
        public static readonly int FiguresCount = 12;

        private Board Board;
        private IPlayer WhitePlayer;
        private IPlayer BlackPlayer;

        public void StartGame(IPlayer whitePlayer, IPlayer blackPlayer)
        {
            WhitePlayer = whitePlayer;
            BlackPlayer = blackPlayer;

            Board = new Board();
            Board.Reset(8, 12);

            var result = WhiteMove();
        }

        private GameResult WhiteMove()
        {
            Console.WriteLine("White player move: ");
            ConsoleHelper.ShowBoard(Board);

            var move = WhitePlayer.ChooseMove(Board);
            if (move is null)
            {
                return GameResult.BlackWin;
            }

            move.MakeMove(Board);

            return BlackMove();
        }

        private GameResult BlackMove()
        {
            Console.WriteLine("Black player move: ");
            ConsoleHelper.ShowBoard(Board);

            var move = BlackPlayer.ChooseMove(Board);
            if (move is null)
            {
                return GameResult.WhiteWin;
            }

            move.MakeMove(Board);

            return WhiteMove();
        }
    }
}
