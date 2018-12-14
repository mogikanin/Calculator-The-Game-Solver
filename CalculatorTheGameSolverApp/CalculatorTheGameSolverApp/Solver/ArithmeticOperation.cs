using System;

namespace CalculatorTheGameSolverApp.Solver
{
    internal class ArithmeticOperation : IChangeableOperation
    {
        private int _coefficient;
        private readonly ArithmeticOperationType _type;

        public ArithmeticOperation(int coefficient, ArithmeticOperationType type)
        {
            _coefficient = coefficient;
            _type = type;
        }

        public int Apply(int value)
        {
            switch (_type)
            {
                case ArithmeticOperationType.Add:
                    return value + _coefficient;
                case ArithmeticOperationType.Subtract:
                    return value - _coefficient;
                case ArithmeticOperationType.Multiply:
                    return value * _coefficient;
                case ArithmeticOperationType.Divide:
                    return value / _coefficient;
                case ArithmeticOperationType.Pow:
                    return (int)Math.Pow(value, _coefficient);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool CanApplyTo(int value)
        {
            switch (_type)
            {
                case ArithmeticOperationType.Multiply:
                    return value != 0 && value*_coefficient <= Consts.MAX_NUMBER;
                case ArithmeticOperationType.Pow:
                    return value != 0 && Math.Pow(value, _coefficient) <= Consts.MAX_NUMBER;
                case ArithmeticOperationType.Divide:
                    return value != 0 && value % _coefficient == 0;
                default:
                   return true;
            }
        }

        public override string ToString()
        {
            switch (_type)
            {
                case ArithmeticOperationType.Add:
                    return "+" + _coefficient;
                case ArithmeticOperationType.Subtract:
                    return "-" + _coefficient;
                case ArithmeticOperationType.Multiply:
                    return "x" + _coefficient;
                case ArithmeticOperationType.Divide:
                    return "/" + _coefficient;
                case ArithmeticOperationType.Pow:
                    return "x^" + _coefficient;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Change(ChangerOperation operation)
        {
            _coefficient = operation.Change(_coefficient);
        }

        public IChangeableOperation Clone()
        {
            return (IChangeableOperation)MemberwiseClone();
        }
    }

    internal enum ArithmeticOperationType
    {
        Add,
        Subtract,
        Multiply,
        Divide,
        Pow,
    }
}