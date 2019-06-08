using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvDataGetter.Model;
using Logic.LinguisticSummarization;

namespace Logic.Operations
{
    public static class FuzzySetOperations
    {
        public static double OrOperator(double first, FuzzySet second, double valueToGetMembershipValue)
        {
            double secondMembershipValue =
                second.MembershipFunction.GetMembershipFunctionValue(valueToGetMembershipValue);
            return Math.Max(first, secondMembershipValue);
        }
        public static double AndOperator(double first, FuzzySet second, double valueToGetMembershipValue)
        {
            double secondMembershipValue =
                second.MembershipFunction.GetMembershipFunctionValue(valueToGetMembershipValue);
            return Math.Min(first, secondMembershipValue);
        }

        public static double PerformCalculationsBetweenGivenSummarizators(List<Summarizator> summarizatorsSet,
            SingleCrimeInfo singleCrime, OperationType oOperator)
        {
            List<double> membershipSummarizationValues = summarizatorsSet.Select(s =>
                    s.MembershipFunction.GetMembershipFunctionValue(singleCrime.GetAttributeValue(s.AttributeName)))
                .ToList();
            if (oOperator == OperationType.And)
            {
                return membershipSummarizationValues.Min();
            }
            else
            {
                return membershipSummarizationValues.Max();
            }
        }
    }
}
