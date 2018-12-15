using System;
using System.Collections.Generic;
using System.Linq;

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
        private readonly int _portalIn;
        private readonly int _portalOut;

        public Solver(List<IOperation> availableOperations, int goal, int portalIn, int portalOut)
        {
            _availableOperations = availableOperations;
            _goal = goal;
            _portalIn = portalIn;
            _portalOut = portalOut;
        }

        public List<IOperation> Solve(int current, int moves)
        {
            var currentOperations = new List<IOperation>(moves);
            return Solve(current, moves, currentOperations, _availableOperations);
        }

        private List<IOperation> Solve(int current, int moves, List<IOperation> currentOperations, List<IOperation> availableOperations)
        {
            if (moves < 0) return null;
            if (moves == 0)
            {
                return current == _goal ? currentOperations : null;
            }

            foreach (var availableOperation in availableOperations)
            {
                if (!availableOperation.CanApplyTo(current)) continue;

                int nextCurrent;
                List<IOperation> nextIterationAvailableOperations;
                var decMoves = false;
                if (availableOperation is ChangerOperation operation)
                {
                    var changer = operation;
                    nextIterationAvailableOperations = new List<IOperation>(availableOperations.Count);
                    foreach (var aOp in availableOperations)
                    {
                        if (aOp is IChangeableOperation changeableOperation)
                        {
                            var clone = changeableOperation.Clone();
                            nextIterationAvailableOperations.Add(clone);
                            clone.Change(changer);
                        }
                        else
                        {
                            nextIterationAvailableOperations.Add(aOp);
                        }
                    }

                    nextCurrent = current;
                }
                else if (availableOperation is StoreOperation)
                {
                    // Check previous operation
                    if (currentOperations.LastOrDefault() is StoreOperation)
                    {
                        continue;
                    }

                    nextCurrent = current;
                    moves++;
                    decMoves = true;
                    var previousStorePop = availableOperations.OfType<StorePopOperation>().FirstOrDefault(); 
                    nextIterationAvailableOperations = new List<IOperation>(availableOperations)
                    {
                        new StorePopOperation(current)
                    };
                    if (previousStorePop != null)
                    {
                        nextIterationAvailableOperations.Remove(previousStorePop);
                    }
                }
                else
                {
                    nextIterationAvailableOperations = availableOperations;
                    nextCurrent = availableOperation.Apply(current);
                    ApplyPortals(ref nextCurrent);
                }

                currentOperations.Add(availableOperation);
                var res = Solve(nextCurrent, moves - 1, currentOperations, nextIterationAvailableOperations);
                if (res != null) return res;

                currentOperations.RemoveAt(currentOperations.Count - 1);
                if (decMoves) moves--;
            }

            return null;
        }

        private void ApplyPortals(ref int value)
        {
            if (_portalOut == 0 && _portalIn == 0) return;
            var digits = value.ToString();

            var outOfPortal = _portalOut > 0? digits.Substring(digits.Length - _portalOut) : string.Empty;
            while (digits.Length > _portalIn)
            {
                var @out = (int)char.GetNumericValue(digits[digits.Length - _portalIn - 1]);
                digits = digits.Remove(digits.Length - _portalIn - 1, 1);
                var inPortal = Convert.ToInt32(digits.Substring(0, digits.Length - outOfPortal.Length)) + @out;
                digits = inPortal + outOfPortal;
            }

            value = Convert.ToInt32(digits);
        }
    }
}
