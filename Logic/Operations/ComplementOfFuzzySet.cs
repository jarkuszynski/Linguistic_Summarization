using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.LinguisticSummarization;

namespace Logic.Operations
{
    public class ComplementOfFuzzySet : FuzzySetProperties
    {
        public ComplementOfFuzzySet(FuzzySet singleSet) : base(singleSet)
        {
        }

        public override double Calculate(double value)
        {
            return 1.0 - SingleSet.MembershipFunction.GetMembershipFunctionValue(value);
        }
    }
}
