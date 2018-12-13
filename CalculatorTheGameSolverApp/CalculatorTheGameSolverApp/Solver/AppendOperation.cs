using System;

namespace CalculatorTheGameSolverApp.Solver
{
    internal class AppendOperation : IOperation
    {
        private readonly int _append;

        public AppendOperation(int append)
        {
            _append = append;
        }

        public int Apply(int value)
        {
            return Convert.ToInt32(value.ToString() + _append);
        }

        public bool CanApplyTo(int value)
        {
            return Math.Abs(value).ToString().Length < Consts.MAX_NUMBER_LENGTH;
        }

        public override string ToString()
        {
            return _append.ToString();
        }
    }
}