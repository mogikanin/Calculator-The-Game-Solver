using System;
using System.Linq;

namespace CalculatorTheGameSolverApp.Solver
{
    internal class MirrorOperation : IOperation
    {
        public int Apply(int value)
        {
            var sign = Math.Sign(value);
            var str = Math.Abs(value).ToString();
            str += new string(str.Reverse().ToArray());
            return sign*Convert.ToInt32(str);
        }

        public bool CanApplyTo(int value)
        {
            return value != 0 && value.ToString().Length <= Consts.MAX_NUMBER_LENGTH / 2;
        }

        public override string ToString()
        {
            return "Mirror";
        }
    }
}