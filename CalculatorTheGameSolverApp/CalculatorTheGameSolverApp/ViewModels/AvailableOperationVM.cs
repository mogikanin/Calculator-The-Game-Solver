using System;
using CalculatorTheGameSolverApp.Solver;
using Xamarin.Forms;

namespace CalculatorTheGameSolverApp.ViewModels
{
    internal class AvailableOperationVM
    {
        public AvailableOperationVM(string title, OperationType operationType, Func<View> editorFunc = null)
        {
            Title = title;
            OperationType = operationType;
            EditorFunc = editorFunc;
        }

        private string Title { get; }
        private OperationType OperationType { get; }
        public Func<View> EditorFunc { get; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }

        public IOperation CreateOperation()
        {
            int value1;
            switch (OperationType)
            {
                case OperationType.Add:
                    if (!TryParseValue1(out value1)) return null;
                    return new ArithmeticOperation(value1, OperationType.Add);
                case OperationType.Subtract:
                    if (!TryParseValue1(out value1)) return null;
                    return new ArithmeticOperation(value1, OperationType.Subtract);
                case OperationType.Multiply:
                    if (!TryParseValue1(out value1)) return null;
                    return new ArithmeticOperation(value1, OperationType.Multiply);
                case OperationType.Divide:
                    if (!TryParseValue1(out value1)) return null;
                    return new ArithmeticOperation(value1, OperationType.Divide);
                case OperationType.Pow:
                    if (!TryParseValue1(out value1)) return null;
                    return new ArithmeticOperation(value1, OperationType.Pow);
                case OperationType.Append:
                    if (!TryParseValue1(out value1)) return null;
                    return new AppendOperation(value1);
                case OperationType.Replace:
                    if (!TryParseValue1(out value1)) return null;
                    if (!TryParseValue2(out var value2)) return null;
                    return new ReplaceOperation(value1, value2);
                case OperationType.InvertSign:
                    return new InvertSignOperation();
                case OperationType.RemoveLast:
                    return new RemoveLastOperation();
                case OperationType.Reverse:
                    return new ReverseOperation();
                case OperationType.Sum:
                    return new SumOperation();
                case OperationType.ShiftLeft:
                    return new ShiftOperation(true);
                case OperationType.ShiftRight:
                    return new ShiftOperation(false);
                case OperationType.Mirror:
                    return new MirrorOperation();
                case OperationType.Changer:
                    if (!TryParseValue1(out value1)) return null;
                    return new ChangerOperation(value1);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool TryParseValue1(out int value)
        {
            value = 0;
            if (Value1 == null || !int.TryParse(Value1, out value)) return false;
            return true;
        }

        private bool TryParseValue2(out int value)
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