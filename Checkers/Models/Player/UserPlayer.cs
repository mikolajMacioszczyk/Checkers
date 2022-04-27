using Checkers.Enums;

namespace Checkers.Models.Player
{
    public class UserPlayer : IPlayer
    {
        public string Name => throw new NotImplementedException();

        public void AssignColor(FigureColor color)
        {
            throw new NotImplementedException();
        }

        public MoveBase ChooseMove(Board currentState)
        {
            throw new NotImplementedException();
        }
    }
}
