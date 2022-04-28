using Checkers.Enums;

namespace Checkers.Models.Player
{
    public class ComputerPlayer : IPlayer
    {
        private static readonly Random random = new Random();
        public string Name => "Computer";

        public FigureColor Color { get; set; }

        public void AssignColor(FigureColor color)
        {
            Color = color;
        }

        public MoveBase ChooseMove(Board currentState)
        {
            var allMoves = currentState.GetAllAvailableMoves(Color);

            var choice = allMoves.OrderBy(_ => random.Next()).FirstOrDefault();

            return choice;
        }
    }
}
