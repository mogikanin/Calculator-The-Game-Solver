using System;
using System.Linq;

namespace CalculatorTheGameSolverApp.Solver
{
    internal class SumOperation : IOperation
    {
        public int Apply(int value)
        {
            var sign = Math.Sign(value);
            return (int)Math.Abs(value).ToString().Sum(_ => char.GetNumericValue(_))*sign;
        }

        public bool CanApplyTo(int value)
        {
            return Math.Abs(value) >= 10;
        }

        public override string ToString()
        {
            return "Sum";
        }
    }
}