using Checkers.Enums;

namespace Checkers.Models.Player
{
    public class UserPlayer : IPlayer
    {
        public string Name => throw new NotImplementedException();
        public FigureColor MyColor { get; set; }

        public void AssignColor(FigureColor color)
        {
            MyColor = color;
        }

        public MoveBase ChooseMove(Board currentState)
        {
            var available = currentState.GetAllAvailableMoves(MyColor);
            if (!available.Any())
            {
                return null;
            }
            
            Console.WriteLine("Available moves: ");

            for (int i = 0; i < available.Count; i++)
            {
                Console.WriteLine($"{i + 1} {available[i]}");
            }

            MoveBase move = null;
            while (move is null)
            {
                int choice = TryGetUserIntInput("Select move: ", 1, available.Count);
                move = available[choice - 1];
            }

            return move;
        }

        private int TryGetUserIntInput(string message, int min, int max)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();

            try
            {
                var result = int.Parse(input);
                if (result < min)
                {
                    Console.WriteLine($"Value should be at least {min}");
                    return TryGetUserIntInput(message, min, max);
                }
                if (result > max)
                {
                    Console.WriteLine($"Value should not be grather than {max}");
                    return TryGetUserIntInput(message, min, max);
                }
                return result;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Cannot convert {input} to integer. Please try again");
                return TryGetUserIntInput(message, min, max);
            }
        }
    }
}
