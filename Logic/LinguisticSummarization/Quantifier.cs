using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.MembershipFunctions;

namespace Logic.LinguisticSummarization
{
    public class Quantifier : FuzzySet
    {
        public QuantifierType Type { get; set; }
        
        public Quantifier(string description, IMembershipFunction membershipFunction, QuantifierType type) : base(description, membershipFunction)
        {
            Type = type;
        }

        public bool IsAbsolute => Type == QuantifierType.Absolute;

        public enum QuantifierType
        {
            Relative,
            Absolute
        }
    }
}
