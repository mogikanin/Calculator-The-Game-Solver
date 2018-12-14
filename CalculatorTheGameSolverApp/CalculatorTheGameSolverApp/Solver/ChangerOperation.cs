namespace CalculatorTheGameSolverApp.Solver
{
    internal class ChangerOperation : IOperation
    {
        private readonly int _coefficient;

        public ChangerOperation(int coefficient)
        {
            _coefficient = coefficient;
        }

        public int Apply(int value)
        {
            return value;
        }

        public bool CanApplyTo(int value)
        {
            return true;
        }

        public int Change(int value)
        {
            return value + _coefficient;
        }

        public override string ToString()
        {
            return $"[+]{_coefficient}";
        }
    }
}