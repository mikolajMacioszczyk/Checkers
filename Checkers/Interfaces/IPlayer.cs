using Checkers.Enums;
using Checkers.Models;

namespace Checkers.Interfaces
{
    public interface IPlayer
    {
        public string Name { get; }
        public FigureColor Color { get; }

        MoveBase ChooseMove(Board currentState);
        void AssignColor(FigureColor color);
    }
}
