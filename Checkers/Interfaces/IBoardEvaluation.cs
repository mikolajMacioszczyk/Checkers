using Checkers.Models;

namespace Checkers.Interfaces
{
    public interface IBoardEvaluation
    {
        int Evaluate(Board board);
    }
}
