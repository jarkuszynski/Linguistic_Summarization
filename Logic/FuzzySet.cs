using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.MembershipFunctions;

namespace Logic
{
    public class FuzzySet
    {
        public FuzzySet(string description, IMembershipFunction membershipFunction, double minValueOfColumn, double maxValueOfColumn)
        {
            Description = description;
            MembershipFunction = membershipFunction;
            MinValueOfColumn = minValueOfColumn;
            MaxValueOfColumn = maxValueOfColumn;
        }

        public double MinValueOfColumn { get; set; }
        public double MaxValueOfColumn { get; set; }
        public double X => MaxValueOfColumn - MinValueOfColumn;

        public string Description { get; set; }
        public IMembershipFunction MembershipFunction { get; set; }

        public double DegreeOfFuzziness() => MembershipFunction.Support / X;
    }
}
