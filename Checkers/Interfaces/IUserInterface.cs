using Checkers.Enums;
using Checkers.Models;

namespace Checkers.Interfaces
{
    public interface IUserInterface
    {
        void ShowNextMove(IPlayer player);
        void ShowMessage(string message);

        void GameOver(IPlayer whitePlayer, IPlayer blackPlayer, GameResult result, int moveCount);

        void ShowBoard(Board board);
    }
}
