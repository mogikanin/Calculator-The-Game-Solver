using CalculatorTheGameSolverApp.Solver;

namespace CalculatorTheGameSolverApp.ViewModels
{
    internal class OperationVM
    {
        public IOperation Operation { get; }

        public OperationVM(IOperation operation)
        {
            Operation = operation;
            Title = Operation.ToString();
        }
        public string Title { get; }
    }
}