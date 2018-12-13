using System;
using System.Linq;

namespace CalculatorTheGameSolverApp.Solver
{
    internal class ReverseOperation : IOperation
    {
        public int Apply(int value)
        {
            var sign = Math.Sign(value);
            value = Math.Abs(value);
            var str = value.ToString();
            str = new string(str.Reverse().ToArray());
            return sign * Convert.ToInt32(str);
        }

        public bool CanApplyTo(int value)
        {
            return Math.Abs(value) >= 10;
        }

        public override string ToString()
        {
            return "Reverse";
        }
    }
}