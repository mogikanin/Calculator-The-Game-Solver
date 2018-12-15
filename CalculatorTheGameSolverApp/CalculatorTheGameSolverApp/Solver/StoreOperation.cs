using System;

namespace CalculatorTheGameSolverApp.Solver
{
    internal class StoreOperation : IOperation
    {
        public int Apply(int value)
        {
            return value;
        }

        public bool CanApplyTo(int value)
        {
            return true;
        }

        public override string ToString()
        {
            return "Store";
        }
    }

    internal class StorePopOperation : IOperation
    {
        private readonly int _value;

        public StorePopOperation(int value)
        {
            _value = value;
        }

        public int Apply(int value)
        {
            return Convert.ToInt32(value + _value.ToString());
        }

        public bool CanApplyTo(int value)
        {
            var pop = value + _value.ToString();
            return int.TryParse(pop, out var res) && res <= Consts.MAX_NUMBER;
        }

        public override string ToString()
        {
            return $"Store Pop {_value}";
        }
    }
}