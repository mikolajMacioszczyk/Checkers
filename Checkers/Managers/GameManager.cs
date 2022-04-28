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
            whitePlayer.AssignColor(FigureColor.White);
            BlackPlayer = blackPlayer;
            blackPlayer.AssignColor(FigureColor.Black);
            
            int counter = 0;

            Board = new Board();
            Board.Reset(8, 12);

            var result = NextMove(WhitePlayer, ref counter, 0);
            Console.WriteLine("Game over !!!");
            switch (result)
            {
                case GameResult.WhiteWin:
                    Console.WriteLine($"Player {whitePlayer.Name} (White) won in {counter} moves");
                    break;
                case GameResult.BlackWin:
                    Console.WriteLine($"Player {BlackPlayer.Name} (Black) won in {counter} moves");
                    break;
                default:
                    Console.WriteLine($"Draw after {counter} moves");
                    break;
            }
        }

        private GameResult NextMove(IPlayer player, ref int counter, int onlyKingsMove)
        {
            ConsoleHelper.ShowBoard(Board);
            var color = player.Color == FigureColor.White ? "White" : "Black";
            Console.WriteLine($"Player {player.Name} ({color}) move: ");

            var move = player.ChooseMove(Board);

            while (!ValidateCanMove(move, player.Color))
            {
                move = player.ChooseMove(Board);
                Console.WriteLine("Move not permitted. Try again");
            }

            if (move is null)
            {
                return player.Color == FigureColor.White ? GameResult.BlackWin : GameResult.WhiteWin;
            }

            move.MakeMove(Board);
            counter++;
            if (Board.OnlyKings)
            {
                onlyKingsMove++;
                if (onlyKingsMove >= 15)
                {
                    return GameResult.Draw;
                }
            }

            var nextPlayer = player.Color == FigureColor.White ? BlackPlayer : WhitePlayer;
            return NextMove(nextPlayer, ref counter, onlyKingsMove);
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
            return availableMoves.Any(m => m.Equals(move));
        }
    }
}
