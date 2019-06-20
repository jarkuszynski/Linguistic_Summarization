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
        public FuzzySet(string description, IMembershipFunction membershipFunction)
        {
            Description = description;
            MembershipFunction = membershipFunction;
        }
        public double X { get; set; }

        public string Description { get; set; }
        public IMembershipFunction MembershipFunction { get; set; }

        public double DegreeOfFuzziness() => MembershipFunction.Support / X;
        protected Tuple<double, double> getSummarizatorOrQualifierMinAndMaximalValue(string attributeName)
        {
            switch (attributeName)
            {
                case "Number of Killed":
                    return new Tuple<double, double>(0, 8);
                case "Number of Injured":
                    return new Tuple<double, double>(0, 14);
                case "Gun status":
                    return new Tuple<double, double>(0, 0.9);
                case "Average Age":
                    return new Tuple<double, double>(0, 80);
                case "Age Group":
                    return new Tuple<double, double>(0, 0.9);
                case "Gender Fraction":
                    return new Tuple<double, double>(0, 0.9);
                case "Participant Type":
                    return new Tuple<double, double>(0, 0.9);
                case "Horizontal":
                    return new Tuple<double, double>(0, 67.2711);
                case "Vertical":
                    return new Tuple<double, double>(0, 71.3);
                case "Period":
                    return new Tuple<double, double>(0, 8);
                default:
                    throw new ArgumentNullException("property name must be provided or given is invalid");
            }
        }
    }
}
