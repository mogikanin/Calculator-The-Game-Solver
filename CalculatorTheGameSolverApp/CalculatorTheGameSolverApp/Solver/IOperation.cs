namespace CalculatorTheGameSolverApp.Solver
{
    internal interface IOperation
    {
        int Apply(int value);
        bool CanApplyTo(int value);
    }
}