using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeCalculator.Interfaces
{
    /// <summary>
    /// Base class for a calculation result
    /// </summary>
    public abstract class CalculationResult
    {
        protected CalculationResult(ECalculationResultType resultType, string name)
        {
            ResultType = resultType;
            Name = name;
        }

        public string Name { get; }
        public ECalculationResultType ResultType { get; }
        public abstract object Result { get; }

        public override string ToString()
        {
            return Name + " (" + ResultType + ") = " + Result;
        }
    }

    /// <summary>
    /// Calculation result representing an integer.
    /// </summary>
    public class CalculationResultInt : CalculationResult
    {
        public int Value { get; }

        public override object Result => Value;

        public CalculationResultInt(string name, int value) : base(ECalculationResultType.tInt, name)
        {
            Value = value;
        }
    }

    /// <summary>
    /// Calculation result representing a string.
    /// </summary>
    public class CalculationResultString : CalculationResult
    {
        public string Value { get; }

        public override object Result => Value;

        public CalculationResultString(string name, string value) : base(ECalculationResultType.tString, name)
        {
            Value = value;
        }
    }


    public enum ECalculationResultType
    {
        tString,
        tInt
    }
}