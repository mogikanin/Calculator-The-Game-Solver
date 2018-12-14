using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CalculatorTheGameSolverApp.Solver;
using CalculatorTheGameSolverApp.Views;
using Xamarin.Forms;

namespace CalculatorTheGameSolverApp.ViewModels
{
    internal class MainPageVM : ViewModelBase
    {
        private AvailableOperationVM _currentOperation;
        private string _moves;
        private string _initial;
        private string _goal;
        private bool _isSolutionFound;

        public MainPageVM()
        {
            AvailableOperations = new List<AvailableOperationVM>
            {
                new AvailableOperationVM("Add +", OperationType.Add, () => new SingleItemView()),
                new AvailableOperationVM("Subtract -", OperationType.Subtract, () => new SingleItemView()),
                new AvailableOperationVM("Multiply x", OperationType.Multiply, () => new SingleItemView()),
                new AvailableOperationVM("Divide /", OperationType.Divide, () => new SingleItemView()),
                new AvailableOperationVM("Power x^n", OperationType.Pow, () => new SingleItemView()),
                new AvailableOperationVM("Append", OperationType.Append, () => new SingleItemView()),
                new AvailableOperationVM("Replace =>", OperationType.Replace, () => new ReplaceOperationView()),
                new AvailableOperationVM("Invert sign +/-", OperationType.InvertSign),
                new AvailableOperationVM("Remove last <<", OperationType.RemoveLast),
                new AvailableOperationVM("Reverse", OperationType.Reverse),
                new AvailableOperationVM("Sum", OperationType.Sum),
                new AvailableOperationVM("< Shift", OperationType.ShiftRight),
                new AvailableOperationVM("Shift >", OperationType.ShiftLeft),
                new AvailableOperationVM("Mirror", OperationType.Mirror),
                new AvailableOperationVM("Changer [+]", OperationType.Changer, () => new SingleItemView()),
            };

            Operations = new ObservableCollection<OperationVM>();

            CommandAddOperation = new Command((Action)delegate
            {
                var operation = CurrentOperation?.CreateOperation();
                if (operation != null)
                {
                    Operations.Add(new OperationVM(operation));
                }
            });
            CommandClear = new Command((Action) delegate
            {
                Moves = null;
                Initial = null;
                Goal = null;
                Operations.Clear();
                CurrentOperation = null;
                IsSolutionFound = false;
            });
            CommandSolveIt = new Command((Action) async delegate
            {
                if (!int.TryParse(Moves, out var moves) || moves <= 0) return;
                if (!int.TryParse(Goal, out var goal)) return;
                if (!int.TryParse(Initial, out var current)) return;
                if (Operations.Count == 0) return;

                Busy.IsBusy = true;
                var res = await Task.Run(delegate
                {
                    var solver = new Solver.Solver(Operations.Select(_ => _.Operation).ToList(), goal);
                    var result = solver.Solve(current, moves);
                    return result;
                });

                if (res != null)
                {
                    Operations.Clear();
                    foreach (var operation in res)
                    {
                        Operations.Add(new OperationVM(operation));
                    }

                    IsSolutionFound = true;
                }
                else
                {
                    DisplayAlert("Error", "Solution is not found!", "OK");
                }
               
                Busy.IsBusy = false;
            });
        }

        public ICommand CommandAddOperation { get; }
        public ICommand CommandClear { get; }
        public ICommand CommandSolveIt { get; }

        public BusyObject Busy { get; } = new BusyObject();
        public List<AvailableOperationVM> AvailableOperations { get; }
        public ObservableCollection<OperationVM> Operations { get; }

        public AvailableOperationVM CurrentOperation
        {
            get => _currentOperation;
            set => SetProperty(ref _currentOperation, value);
        }

        public string Moves
        {
            get => _moves;
            set => SetProperty(ref _moves, value);
        }

        public string Initial
        {
            get => _initial;
            set => SetProperty(ref _initial, value);
        }

        public string Goal
        {
            get => _goal;
            set => SetProperty(ref _goal, value);
        }

        public bool IsSolutionFound
        {
            get => _isSolutionFound;
            private set => SetProperty(ref _isSolutionFound, value);
        }

        public Action<string, string, string> DisplayAlert { get; set; }
    }
}
