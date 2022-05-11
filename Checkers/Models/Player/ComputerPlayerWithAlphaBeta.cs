using Checkers.Enums;
using Checkers.Helpers;
using Checkers.Interfaces;

namespace Checkers.Models.Player
{
    public class ComputerPlayerWithAlphaBeta : IPlayer
    {
        private readonly IBoardEvaluation _evaluation;
        private readonly int _depth;

        public ComputerPlayerWithAlphaBeta(IBoardEvaluation evaluation, int depth)
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

            // alpha best for max (start with min)
            int alpha = int.MinValue;
            // betha best for min (start with max)
            int betha = int.MaxValue;

            foreach (var testedMove in allMoves)
            {
                testedMove.MakeMove(currentState);
                int result = 0;
                if (Color == FigureColor.White)
                {
                    // minimazing on second step 
                    result = Min(currentState, 1, alpha, betha, CheckersHelper.Opposite(Color));
                    if (result > alpha)
                    {
                        // but maximizing on this
                        alpha = result;
                    }
                }
                else
                {
                    // maximizing on second step
                    result = Max(currentState, 1, alpha, betha, CheckersHelper.Opposite(Color));
                    if (result < betha)
                    {
                        // but minimizing on this
                        betha = result;
                    }
                }
                moveResults.Add((result, testedMove));
                testedMove.UndoMove(currentState);
            }
            int selectedEvaluation = Color == FigureColor.White ?
                moveResults.Max(m => m.Item1) : moveResults.Min(m => m.Item1);

            var choice = moveResults.First(m => m.Item1 == selectedEvaluation).Item2;

            return choice;
        }

        private int Max(Board state, int currentDepth, int alpha, int betha, FigureColor currentColor)
        {
            if (currentDepth == _depth)
            {
                return _evaluation.Evaluate(state);
            }

            var allMoves = state.GetAllAvailableMoves(currentColor);
            int maxEvaluation = int.MinValue;

            foreach (var testedMove in allMoves)
            {
                testedMove.MakeMove(state);
                var result = Min(state, currentDepth + 1, alpha, betha, CheckersHelper.Opposite(currentColor));
                testedMove.UndoMove(state);

                // algorithm has seen bether option already
                if (result > betha)
                {
                    return result;
                }
                // better option for maximizer
                if (result > alpha)
                {
                    alpha = result;
                }
                maxEvaluation = Math.Max(maxEvaluation, result);
            }

            return maxEvaluation;
        }

        private int Min(Board state, int currentDepth, int alpha, int betha, FigureColor currentColor)
        {
            if (currentDepth == _depth)
            {
                return _evaluation.Evaluate(state);
            }

            var allMoves = state.GetAllAvailableMoves(currentColor);
            int minEvaluation = int.MaxValue;

            foreach (var testedMove in allMoves)
            {
                testedMove.MakeMove(state);
                var result = Max(state, currentDepth + 1, alpha, betha, CheckersHelper.Opposite(currentColor));
                testedMove.UndoMove(state);

                // algorithm has seen bether option already
                if (result < alpha)
                {
                    return result;
                }
                // better option for minimizer
                if (result < betha)
                {
                    betha = result;
                }
                minEvaluation = Math.Min(minEvaluation, result);
            }

            return minEvaluation;
        }
    }
}
