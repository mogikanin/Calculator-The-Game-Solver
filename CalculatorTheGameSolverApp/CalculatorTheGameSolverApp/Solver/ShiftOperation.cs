using System;

namespace CalculatorTheGameSolverApp.Solver
{
    internal class ShiftOperation : IOperation
    {
        private readonly bool _left;

        public ShiftOperation(bool left)
        {
            _left = left;
        }

        public int Apply(int value)
        {
            var sign = Math.Sign(value);
            var str = Math.Abs(value).ToString();
            str = !_left ? (str + str[0]).Remove(0, 1) : (str[str.Length - 1] + str).Remove(str.Length, 1);
            return sign * Convert.ToInt32(str);
        }

        public bool CanApplyTo(int value)
        {
            return Math.Abs(value) >= 10;
        }

        public override string ToString()
        {
            return !_left ? "< Shift" : "Shift >";
        }
    }
}