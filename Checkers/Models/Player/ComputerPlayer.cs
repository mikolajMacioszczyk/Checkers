using Checkers.Enums;

namespace Checkers.Models.Player
{
    public class ComputerPlayer : IPlayer
    {
        private static readonly Random random = new Random();
        public string Name => throw new NotImplementedException();

        public FigureColor MyColor { get; set; }

        public void AssignColor(FigureColor color)
        {
            MyColor = color;
        }

        public MoveBase ChooseMove(Board currentState)
        {
            var allMoves = currentState.GetAllAvailableMoves(MyColor);

            var choice = allMoves.OrderBy(_ => random.Next()).FirstOrDefault();

            return choice;
        }
    }
}
