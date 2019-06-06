using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.MembershipFunctions;

namespace Logic
{
    public abstract class FuzzySet
    {
        public FuzzySet(string description, IMembershipFunction membershipFunction)
        {
            Description = description;
            MembershipFunction = membershipFunction;
        }

        public string Description { get; set; }
        public IMembershipFunction MembershipFunction { get; set; }
    }
}
