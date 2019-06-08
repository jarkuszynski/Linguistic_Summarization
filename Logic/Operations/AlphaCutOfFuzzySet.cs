using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Operations
{
    public class AlphaCutOfFuzzySet : FuzzySetProperties
    {
        public AlphaCutOfFuzzySet(FuzzySet single) : base(single)
        {
        }

        public override double Calculate(double value)
        {
            throw new NotImplementedException();
        }
    }
}
