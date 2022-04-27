using Checkers.Enums;

namespace Checkers.Models.Player
{
    public interface IPlayer
    {
        public string Name { get; }
        MoveBase ChooseMove(Board currentState);
        void AssignColor(FigureColor color);
    }
}
