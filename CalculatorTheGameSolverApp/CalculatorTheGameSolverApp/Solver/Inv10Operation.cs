using System;
using System.Linq;

namespace CalculatorTheGameSolverApp.Solver
{
    internal class Inv10Operation : IOperation
    {
        public int Apply(int value)
        {
            var sign = Math.Sign(value);
            var chars = value.ToString().Select(delegate(char c)
            {
                var d = (int) char.GetNumericValue(c);
                return d == 0 ? 0.ToString() : (10 - d).ToString();
            });
            return sign*Convert.ToInt32(string.Join("", chars));
        }

        public bool CanApplyTo(int value)
        {
            return value != 0;
        }

        public override string ToString()
        {
            return "Inv10";
        }
    }
}