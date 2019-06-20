using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvDataGetter;
using Logic.MembershipFunctions;

namespace Logic.LinguisticSummarization
{
    public class Quantifier : FuzzySet
    {
        public QuantifierType Type { get; set; }
        
        public Quantifier(string description, IMembershipFunction membershipFunction, QuantifierType type) : base(description, membershipFunction)
        {
            Type = type;
            IsAbsolute = Type == QuantifierType.Absolute ? true : false;
            if (IsAbsolute)
            {
                    X = ReadAllData.numberOfRows;
            } else
            {
                X = 1.0;
            }
        }

        public bool IsAbsolute { get; set; }

        public enum QuantifierType
        {
            Relative,
            Absolute
        }
    }
}
