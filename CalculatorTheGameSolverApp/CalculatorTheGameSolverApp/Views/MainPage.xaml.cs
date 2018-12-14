using System.ComponentModel;
using CalculatorTheGameSolverApp.ViewModels;

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

            var content = current.EditorFunc?.Invoke();
            if (content != null)
            {
                content.BindingContext = current;
            }

            OperationOptions.Content = content;
        }
    }
}
