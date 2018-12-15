using System;

namespace CalculatorTheGameSolverApp.Solver
{
    internal class ReplaceOperation : IOperation
    {
        private readonly string _src;
        private readonly string _dest;

        public ReplaceOperation(string src, string dest)
        {
            _src = src;
            _dest = dest;
        }

        public int Apply(int value)
        {
            return Convert.ToInt32(value.ToString().Replace(_src, _dest));
        }

        public bool CanApplyTo(int value)
        {
            return Math.Abs(value).ToString().Replace(_src, _dest).Length <= Consts.MAX_NUMBER_LENGTH;
        }

        public override string ToString()
        {
            return $"{_src}=>{_dest}";
        }
    }
}