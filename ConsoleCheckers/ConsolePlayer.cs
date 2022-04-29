using Checkers.Enums;
using Checkers.Interfaces;
using Checkers.Models;

namespace ConsoleCheckers
{
    public class ConsolePlayer : IPlayer
    {
        public string Name { get; set; }

        public FigureColor Color { get; set; }

        public void AssignColor(FigureColor color)
        {
            Color = color;
        }

        public MoveBase ChooseMove(Board currentState)
        {
            var available = currentState.GetAllAvailableMoves(Color);
            if (!available.Any())
            {
                return null;
            }

            Console.WriteLine("Available moves: ");

            for (int i = 0; i < available.Count; i++)
            {
                Console.WriteLine($"{i + 1} {available[i].Print(currentState)}");
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
