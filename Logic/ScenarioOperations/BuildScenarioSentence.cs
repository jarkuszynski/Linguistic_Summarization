using Logic.LinguisticSummarization;
using Logic.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.ScenarioOperations
{
    public class BuildScenarioSentence
    {
        private List<double> _summarizationResults = new List<double>();
        private List<double> _qualifierResults = new List<double>();

        public BuildScenarioSentence()
        {
        }

        public string GetScenarioResult(SingleLingusticObject singleLingusticObject)
        {
            return singleLingusticObject.BuildResultSentence() + CalculateAllSummarizationValues(singleLingusticObject);
        }
        public double CalculateAllSummarizationValues(SingleLingusticObject singleLingusticObject)
        {
            AllTValues allTValues = new AllTValues();
            allTValues.T1 = CalculateT1(singleLingusticObject);
            //allTValues.T2 = CalculateT2(singleLingusticObject);
            //allTValues.T3 = CalculateT3(singleLingusticObject);
            //allTValues.T4 = CalculateT4(singleLingusticObject);
            //allTValues.T5 = CalculateT5(singleLingusticObject);
            //allTValues.T6 = CalculateT6(singleLingusticObject);
            //allTValues.T7 = CalculateT7(singleLingusticObject);
            //allTValues.T8 = CalculateT8(singleLingusticObject);
            //allTValues.T9 = CalculateT9(singleLingusticObject);
            //allTValues.T10 = CalculateT10(singleLingusticObject);
            //allTValues.T11 = CalculateT11(singleLingusticObject);
            return allTValues.T1;

        }

        private double CalculateT1(SingleLingusticObject singleLingusticObject)
        {
            return singleLingusticObject.isAbsolute == true ? CalculateT1AbsoluteSummarization(singleLingusticObject) : CalculateT1RelativeSummarization(singleLingusticObject);
        }
        public double CalculateT1AbsoluteSummarization(SingleLingusticObject singleLingusticObject)
        {
            double fullSum = 0.0;
            OperationType operation = singleLingusticObject.operation;
            foreach (var singleDataCrime in LingusticSummarization.CrimesList)
            {
                double bestValue = FuzzySetOperations.PerformCalculationsBetweenGivenSummarizators(singleLingusticObject.Summarizators, singleDataCrime, operation);
                _summarizationResults.Add(bestValue);
                fullSum += bestValue;
            }
            double r = fullSum;
            return singleLingusticObject.Quantifier.IsAbsolute ?
                singleLingusticObject.Quantifier.MembershipFunction.GetMembershipFunctionValue(r) :
                singleLingusticObject.Quantifier.MembershipFunction.GetMembershipFunctionValue(r / LingusticSummarization.CrimesList.Count);
        }
        public double CalculateT1RelativeSummarization(SingleLingusticObject singleLingusticObject)
        {
            double numerator = 0.0;
            double denumertor = 0.0;
            OperationType operation = singleLingusticObject.operation;
            foreach (var singleDataCrime in LingusticSummarization.CrimesList)
            {
                double bestValue = FuzzySetOperations.PerformCalculationsBetweenGivenSummarizators(singleLingusticObject.Summarizators, singleDataCrime, operation);
                _summarizationResults.Add(bestValue);
                double qualifierValue = singleLingusticObject.Qualifier.MembershipFunction.GetMembershipFunctionValue(singleDataCrime.GetAttributeValue(singleLingusticObject.Qualifier.AttributeName));
                _qualifierResults.Add(qualifierValue);

                numerator += Math.Min(bestValue, qualifierValue);
                denumertor += qualifierValue;
            }
            double r;
            r = numerator / denumertor;
            return singleLingusticObject.Quantifier.MembershipFunction.GetMembershipFunctionValue(r);
        }

        private double CalculateT2(SingleLingusticObject singleLingusticObject)
        {
            throw new NotImplementedException();
        }

        private double CalculateT3(SingleLingusticObject singleLingusticObject)
        {
            throw new NotImplementedException();
        }

        private double CalculateT4(SingleLingusticObject singleLingusticObject)
        {
            throw new NotImplementedException();
        }

        private double CalculateT5(SingleLingusticObject singleLingusticObject)
        {
            throw new NotImplementedException();
        }

        private double CalculateT6(SingleLingusticObject singleLingusticObject)
        {
            throw new NotImplementedException();
        }

        private double CalculateT7(SingleLingusticObject singleLingusticObject)
        {
            throw new NotImplementedException();
        }

        private double CalculateT8(SingleLingusticObject singleLingusticObject)
        {
            throw new NotImplementedException();
        }

        private double CalculateT9(SingleLingusticObject singleLingusticObject)
        {
            throw new NotImplementedException();
        }

        private double CalculateT10(SingleLingusticObject singleLingusticObject)
        {
            throw new NotImplementedException();
        }

        private double CalculateT11(SingleLingusticObject singleLingusticObject)
        {
            throw new NotImplementedException();
        }


    }
}
