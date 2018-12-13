namespace CalculatorTheGameSolverApp.Solver
{
    internal class InvertSignOperation : IOperation
    {
        public int Apply(int value)
        {
            return -value;
        }

        public bool CanApplyTo(int value)
        {
            return value != 0;
        }

        public override string ToString()
        {
            return "+/-";
        }
    }
}