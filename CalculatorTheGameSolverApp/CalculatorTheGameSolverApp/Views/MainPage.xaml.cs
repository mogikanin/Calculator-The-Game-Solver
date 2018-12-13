using System;
using System.ComponentModel;
using CalculatorTheGameSolverApp.Solver;
using CalculatorTheGameSolverApp.ViewModels;
using Xamarin.Forms;

namespace CalculatorTheGameSolverApp.Views
{
    public partial class MainPage
    {
        private readonly MainPageVM _dataContext;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = _dataContext = new MainPageVM();
            _dataContext.DisplayAlert = async delegate(string title, string message, string cancel)
            {
                await DisplayAlert(title, message, cancel);
            };
                
            _dataContext.PropertyChanged += delegate(object sender, PropertyChangedEventArgs args)
            {
                switch (args.PropertyName)
                {
                    case nameof(MainPageVM.CurrentOperation):
                        OnCurrentOperationChanged();
                        break;
                }
            };
        }

        private void OnCurrentOperationChanged()
        {
            var current = _dataContext.CurrentOperation;
            if (current == null)
            {
                OperationOptions.Content = null;
                return;
            }

            View content;
            switch (current.OperationType)
            {
               case OperationType.Replace:
                   content = new ReplaceOperationView();
                   break;
                case OperationType.InvertSign:
                case OperationType.RemoveLast:
                case OperationType.Reverse:
                case OperationType.Sum:
                case OperationType.ShiftLeft:
                case OperationType.ShiftRight:
                    content = null;
                    break;
                case OperationType.Add:
                case OperationType.Append:
                case OperationType.Divide:
                case OperationType.Multiply:
                case OperationType.Pow:
                case OperationType.Subtract:
                    content = new SingleItemView();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (content != null)
            {
                content.BindingContext = current;
            }

            OperationOptions.Content = content;
        }
    }
}
