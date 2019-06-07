using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvDataGetter.Model;
using Logic.LinguisticSummarization;

namespace Logic.Operations
{
    public class FuzzySetOperations
    {
        public double OrOperator(double first, FuzzySet second, double valueToGetMembershipValue)
        {
            double secondMembershipValue =
                second.MembershipFunction.GetMembershipFunctionValue(valueToGetMembershipValue);
            return Math.Max(first, secondMembershipValue);
        }
        public double AndOperator(double first, FuzzySet second, double valueToGetMembershipValue)
        {
            double secondMembershipValue =
                second.MembershipFunction.GetMembershipFunctionValue(valueToGetMembershipValue);
            return Math.Min(first, secondMembershipValue);
        }

        public double PerformCalculationsBetweenGivenSummarizators(List<Summarizator> summarizatorsSet,
            SingleCrimeInfo singleCrime, Operator oOperator)
        {
            List<double> membershipSummarizationValues = summarizatorsSet.Select(s =>
                    s.MembershipFunction.GetMembershipFunctionValue(singleCrime.GetAttributeValue(s.AttributeName)))
                .ToList();
            if (oOperator == Operator.And)
            {
                return membershipSummarizationValues.Min();
            }
            else
            {
                return membershipSummarizationValues.Max();
            }
        }
    }

    public enum Operator
    {
        Or,
        And
    }
}
