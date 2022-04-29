using Checkers.Enums;
using Checkers.Interfaces;
using Checkers.Models;

namespace Checkers.Managers
{
    public class GameManager
    {
        public static readonly int BoardSize = 8;
        public static readonly int FiguresCount = 12;
        
        private readonly IUserInterface _userInterface;

        private Board Board;
        private IPlayer WhitePlayer;
        private IPlayer BlackPlayer;

        public GameManager(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

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
            _userInterface.GameOver(WhitePlayer, BlackPlayer, result, counter);
        }

        private GameResult NextMove(IPlayer player, ref int counter, int onlyKingsMove)
        {
            _userInterface.ShowBoard(Board);
            _userInterface.ShowNextMove(player);

            var move = player.ChooseMove(Board);

            while (!ValidateCanMove(move, player.Color))
            {
                move = player.ChooseMove(Board);
                _userInterface.ShowMessage("Move not permitted. Try again");
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
