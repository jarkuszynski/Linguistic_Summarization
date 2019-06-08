using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Operations
{
    public class KernelOfFuzzySet : FuzzySetProperties
    {
        public KernelOfFuzzySet(FuzzySet single) : base(single)
        {
        }

        public override double Calculate(double value)
        {
            return SingleSet.MembershipFunction.GetMembershipFunctionValue(value) > 1 ? 1.0 : 0.0;
        }
    }
}
