using System;

namespace CalculatorTheGameSolverApp.Solver
{
    internal class RemoveLastOperation : IOperation
    {
        public int Apply(int value)
        {
            var str = value.ToString();
            return Convert.ToInt32(str.Remove(str.Length - 1, 1));
        }

        public bool CanApplyTo(int value)
        {
            return Math.Abs(value) >= 10;
        }

        public override string ToString()
        {
            return "<<";
        }
    }
}