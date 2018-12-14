using System;
using CalculatorTheGameSolverApp.Solver;
using Xamarin.Forms;

namespace CalculatorTheGameSolverApp.ViewModels
{
    internal class AvailableOperationVM
    {
        public AvailableOperationVM(string title, Func<AvailableOperationVM, IOperation> createOperationFunc, Func<View> editorFunc = null)
        {
            Title = title;
            EditorFunc = editorFunc;
            CreateOperationFunc = createOperationFunc;
        }

        private string Title { get; }
        public Func<View> EditorFunc { get; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public Func<AvailableOperationVM, IOperation> CreateOperationFunc { get; }

        public bool TryParseValue1(out int value)
        {
            value = 0;
            if (Value1 == null || !int.TryParse(Value1, out value)) return false;
            return true;
        }

        public bool TryParseValue2(out int value)
        {
            value = 0;
            if (Value2 == null || !int.TryParse(Value2, out value)) return false;
            return true;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}