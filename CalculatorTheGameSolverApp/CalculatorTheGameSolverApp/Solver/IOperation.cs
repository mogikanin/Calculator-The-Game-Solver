namespace CalculatorTheGameSolverApp.Solver
{
    internal interface IOperation
    {
        int Apply(int value);
        bool CanApplyTo(int value);
    }

    internal interface IChangeableOperation : IOperation
    {
        void Change(ChangerOperation operation);
        IChangeableOperation Clone();
    }
}