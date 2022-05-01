using Checkers.Enums;
using Checkers.Interfaces;
using Checkers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DesktopCheckers
{
    public class DesktopPlayer : IPlayer
    {
        private static Random _random = new Random();
        public string Name { get; set; }

        public event Action<List<MoveBase>> ChooseStarted;

        public FigureColor Color { get; set; }

        private MoveBase _move;

        public void AssignColor(FigureColor color)
        {
            Color = color;
        }

        public MoveBase ChooseMove(Board currentState)
        {
            var availableMoves = currentState.GetAllAvailableMoves(Color).ToList();
            if (!availableMoves.Any())
            {
                return null;
            }
            StartChoice(availableMoves);
            while (_move is null)
            {
                Thread.Sleep(100);
            }
            return _move;
        }

        private void StartChoice(List<MoveBase> availableMoves)
        {
            _move = null;
            ChooseStarted?.Invoke(availableMoves);
        }

        public void SetMoveChoice(MoveBase move)
        {
            _move = move;
        }
    }
}
