﻿using Checkers.Enums;
using Checkers.Interfaces;

namespace Checkers.Models.Player
{
    public class ComputerPlayer : IPlayer
    {
        private readonly IBoardEvaluation _evaluation;
        private readonly int _depth;

        public ComputerPlayer(IBoardEvaluation evaluation, int depth)
        {
            _evaluation = evaluation;
            _depth = depth;
        }

        public string Name { get; set; } = "Computer";

        public FigureColor Color { get; set; }

        public void AssignColor(FigureColor color)
        {
            Color = color;
        }

        public MoveBase ChooseMove(Board currentState)
        {
            var allMoves = currentState.GetAllAvailableMoves(Color);

            if (!allMoves.Any())
            {
                return null;
            }

            var moveResults = new List<(int, MoveBase)>();

            foreach (var testedMove in allMoves)
            {
                testedMove.MakeMove(currentState);
                int result = Color == FigureColor.White ? 
                    Min(currentState, 1) : Max(currentState, 1);
                moveResults.Add((result, testedMove));
                testedMove.UndoMove(currentState);
            }
            int selectedEvaluation = Color == FigureColor.White ? 
                moveResults.Max(m => m.Item1) : moveResults.Min(m => m.Item1);

            var choice = moveResults.First(m => m.Item1 == selectedEvaluation).Item2;

            return choice;
        }

        private int Max(Board state, int currentDepth)
        {
            if (currentDepth == _depth)
            {
                return _evaluation.Evaluate(state);
            }

            var allMoves = state.GetAllAvailableMoves(Color);
            int maxEvaluation = int.MinValue;

            foreach (var testedMove in allMoves)
            {
                testedMove.MakeMove(state);
                var result = Min(state, currentDepth + 1);
                maxEvaluation = Math.Max(maxEvaluation, result);
                testedMove.UndoMove(state);
            }

            return maxEvaluation;
        }

        private int Min(Board state, int currentDepth)
        {
            if (currentDepth == _depth)
            {
                return _evaluation.Evaluate(state);
            }

            var allMoves = state.GetAllAvailableMoves(OppositeColor(Color));
            int minEvaluation = int.MaxValue;

            foreach (var testedMove in allMoves)
            {
                testedMove.MakeMove(state);
                var result = Max(state, currentDepth + 1);
                minEvaluation = Math.Min(minEvaluation, result);
                testedMove.UndoMove(state);
            }

            return minEvaluation;
        }

        private FigureColor OppositeColor(FigureColor color) => 
            color == FigureColor.White ? FigureColor.Black : FigureColor.White;
    }
}
