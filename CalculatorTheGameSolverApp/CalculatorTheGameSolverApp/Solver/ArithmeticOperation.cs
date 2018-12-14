using System;

namespace CalculatorTheGameSolverApp.Solver
{
    internal class ArithmeticOperation : IChangeableOperation
    {
        private int _coefficient;
        private readonly OperationType _type;

        public ArithmeticOperation(int coefficient, OperationType type)
        {
            _coefficient = coefficient;
            _type = type;
        }

        public int Apply(int value)
        {
            switch (_type)
            {
                case OperationType.Add:
                    return value + _coefficient;
                case OperationType.Subtract:
                    return value - _coefficient;
                case OperationType.Multiply:
                    return value * _coefficient;
                case OperationType.Divide:
                    return value / _coefficient;
                case OperationType.Pow:
                    return (int)Math.Pow(value, _coefficient);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool CanApplyTo(int value)
        {
            switch (_type)
            {
                case OperationType.Multiply:
                    return value != 0 && value*_coefficient <= Consts.MAX_NUMBER;
                case OperationType.Pow:
                    return value != 0 && Math.Pow(value, _coefficient) <= Consts.MAX_NUMBER;
                case OperationType.Divide:
                    return value != 0 && value % _coefficient == 0;
                default:
                   return true;
            }
        }

        public override string ToString()
        {
            switch (_type)
            {
                case OperationType.Add:
                    return "+" + _coefficient;
                case OperationType.Subtract:
                    return "-" + _coefficient;
                case OperationType.Multiply:
                    return "x" + _coefficient;
                case OperationType.Divide:
                    return "/" + _coefficient;
                case OperationType.Pow:
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
}