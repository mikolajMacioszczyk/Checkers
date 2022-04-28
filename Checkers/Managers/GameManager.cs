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
            Console.WriteLine($"Player {WhitePlayer.Name} (White) move: ");
            ConsoleHelper.ShowBoard(Board);

            var move = WhitePlayer.ChooseMove(Board.DeepCopy());
            
            while (!ValidateCanMove(move, FigureColor.White))
            {
                move = WhitePlayer.ChooseMove(Board.DeepCopy());
                Console.WriteLine("Move not permitted. Try again");
            }

            if (move is null)
            {
                return GameResult.BlackWin;
            }

            move.MakeMove(Board);

            return BlackMove();
        }

        private GameResult BlackMove()
        {
            Console.WriteLine($"Player {BlackPlayer.Name} (Black) move: ");
            ConsoleHelper.ShowBoard(Board);

            var move = BlackPlayer.ChooseMove(Board.DeepCopy());
            
            while (!ValidateCanMove(move, FigureColor.Black))
            {
                move = BlackPlayer.ChooseMove(Board.DeepCopy());
                Console.WriteLine("Move not permitted. Try again");
            }

            if (move is null)
            {
                return GameResult.WhiteWin;
            }

            move.MakeMove(Board);

            return WhiteMove();
        }

        private bool ValidateCanMove(MoveBase move, FigureColor color)
        {
            if (move is null)
            {
                return true;
            }

            var figure = move.From.Figure;
            if (figure is null || figure.Color != color)
            {
                return false;
            }

            var availableMoves = figure.GetAvailableMoves(Board);
            return availableMoves.Contains(move);
        }
    }
}
