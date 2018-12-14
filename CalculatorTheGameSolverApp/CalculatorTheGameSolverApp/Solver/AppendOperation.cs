using System;

namespace CalculatorTheGameSolverApp.Solver
{
    internal class AppendOperation : IChangeableOperation
    {
        private int _append;

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

        public IChangeableOperation Clone()
        {
            return (IChangeableOperation)MemberwiseClone();
        }

        public void Change(ChangerOperation operation)
        {
            _append = operation.Change(_append);
        }

        public override string ToString()
        {
            return _append.ToString();
        }
    }
}