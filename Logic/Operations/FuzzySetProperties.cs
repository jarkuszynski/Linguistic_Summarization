using Logic.LinguisticSummarization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Operations
{
    public abstract class FuzzySetProperties
    {
        public FuzzySet SingleSet { get; }
        public FuzzySetProperties(FuzzySet single)
        {
            SingleSet = single ?? throw new ArgumentNullException(nameof(single));
        }
        public abstract double Calculate(double value);
    }
}
