namespace CalculatorTheGameSolverApp.ViewModels
{
    public class BusyObject : ViewModelBase
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
    }
}