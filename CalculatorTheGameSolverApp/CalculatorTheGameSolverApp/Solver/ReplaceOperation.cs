using System;

namespace CalculatorTheGameSolverApp.Solver
{
    internal class ReplaceOperation : IOperation
    {
        private readonly int _src;
        private readonly int _dest;

        public ReplaceOperation(int src, int dest)
        {
            _src = src;
            _dest = dest;
        }

        public int Apply(int value)
        {
            return Convert.ToInt32(value.ToString().Replace(_src.ToString(), _dest.ToString()));
        }

        public bool CanApplyTo(int value)
        {
            return Math.Abs(value).ToString().Replace(_src.ToString(), _dest.ToString()).Length <= Consts.MAX_NUMBER_LENGTH;
        }

        public override string ToString()
        {
            return $"{_src}=>{_dest}";
        }
    }
}