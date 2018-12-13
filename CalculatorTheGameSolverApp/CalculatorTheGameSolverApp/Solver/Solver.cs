using System.Collections.Generic;

namespace CalculatorTheGameSolverApp.Solver
{
    internal static class Consts
    {
        public const int MAX_NUMBER = 999999;
        public const int MAX_NUMBER_LENGTH = 6;
    }

    internal class Solver
    {
        private readonly List<IOperation> _availableOperations;
        private readonly int _goal;

        public Solver(List<IOperation> availableOperations, int goal)
        {
            _availableOperations = availableOperations;
            _goal = goal;
        }

        public List<IOperation> Solve(int current, int moves)
        {
            var currentOperations = new List<IOperation>(moves);
            return Solve(current, moves, currentOperations);
        }

        private List<IOperation> Solve(int current, int moves, List<IOperation> currentOperations)
        {
            if (moves < 0) return null;
            if (moves == 0)
            {
                return current == _goal ? currentOperations : null;
            }

            foreach (var availableOperation in _availableOperations)
            {
                if (availableOperation.CanApplyTo(current))
                {
                    var nextCurrent = availableOperation.Apply(current);
                    currentOperations.Add(availableOperation);
                    var res = Solve(nextCurrent, moves - 1, currentOperations);
                    if (res != null) return res;

                    currentOperations.RemoveAt(currentOperations.Count - 1);
                }
            }

            return null;
        }
    }
}
