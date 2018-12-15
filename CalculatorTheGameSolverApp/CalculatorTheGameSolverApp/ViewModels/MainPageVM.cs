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
                new AvailableOperationVM("Add +",
                    vm => !vm.TryParseValue1(out var value1)
                        ? null
                        : new ArithmeticOperation(value1, ArithmeticOperationType.Add), () => new SingleItemView()),
                new AvailableOperationVM("Subtract -",
                    vm => !vm.TryParseValue1(out var value1)
                        ? null
                        : new ArithmeticOperation(value1, ArithmeticOperationType.Subtract),
                    () => new SingleItemView()),
                new AvailableOperationVM("Multiply x",
                    vm => !vm.TryParseValue1(out var value1)
                        ? null
                        : new ArithmeticOperation(value1, ArithmeticOperationType.Multiply),
                    () => new SingleItemView()),
                new AvailableOperationVM("Divide /",
                    vm => !vm.TryParseValue1(out var value1)
                        ? null
                        : new ArithmeticOperation(value1, ArithmeticOperationType.Divide), () => new SingleItemView()),
                new AvailableOperationVM("Power x^n",
                    vm => !vm.TryParseValue1(out var value1)
                        ? null
                        : new ArithmeticOperation(value1, ArithmeticOperationType.Pow), () => new SingleItemView()),
                new AvailableOperationVM("Append",
                    vm => !vm.TryParseValue1(out var value1) ? null : new AppendOperation(value1),
                    () => new SingleItemView()),
                new AvailableOperationVM("Replace =>", delegate(AvailableOperationVM vm)
                {
                    if (!vm.TryParseValue1(out var value1)) return null;
                    if (!vm.TryParseValue2(out var value2)) return null;
                    return new ReplaceOperation(value1, value2);
                }, () => new ReplaceOperationView()),
                new AvailableOperationVM("Invert sign +/-", _ => new InvertSignOperation()),
                new AvailableOperationVM("Remove last <<", _ => new RemoveLastOperation()),
                new AvailableOperationVM("Reverse", _ => new ReverseOperation()),
                new AvailableOperationVM("Sum", _ => new SumOperation()),
                new AvailableOperationVM("< Shift", _ => new ShiftOperation(false)),
                new AvailableOperationVM("Shift >", _ => new ShiftOperation(true)),
                new AvailableOperationVM("Mirror", _ => new MirrorOperation()),
                new AvailableOperationVM("Changer [+]",
                    vm => !vm.TryParseValue1(out var value1) ? null : new ChangerOperation(value1),
                    () => new SingleItemView()),
                new AvailableOperationVM("Store", _ => new StoreOperation()),
            };

            Operations = new ObservableCollection<OperationVM>();

            CommandAddOperation = new Command((Action)delegate
            {
                var operation = CurrentOperation?.CreateOperationFunc(CurrentOperation);
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
