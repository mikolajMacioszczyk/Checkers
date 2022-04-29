using Checkers.Enums;
using Checkers.Interfaces;
using Checkers.Models;
using System;
using System.Linq;

namespace DesktopCheckers
{
    public class DesktopPlayer : IPlayer
    {
        private static Random _random = new Random();
        public string Name { get; set; }

        public FigureColor Color { get; set; }

        public void AssignColor(FigureColor color)
        {
            Color = color;
        }

        public MoveBase ChooseMove(Board currentState)
        {
            var availableMoves = currentState.GetAllAvailableMoves(Color).OrderBy(_ => _random.Next());
            return availableMoves.FirstOrDefault();
        }
    }
}
