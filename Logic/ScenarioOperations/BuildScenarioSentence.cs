using Logic.LinguisticSummarization;
using Logic.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvDataGetter.Model;

namespace Logic.ScenarioOperations
{
    public class BuildScenarioSentence
    {
        private List<double> _summarizationResults = new List<double>();
        private List<double> _qualifierResults = new List<double>();
        private SingleLingusticObject singleLingusticObject;

        public BuildScenarioSentence(SingleLingusticObject singleObject)
        {
            singleLingusticObject = singleObject;
        }

        public string GetScenarioResult()
        {
            AllTValues allTResults = CalculateAllSummarizationValues();
            StringBuilder stringBuilder = new StringBuilder();
            if (allTResults.T1 <= 0)
            {
                return string.Empty;
            }
            stringBuilder.Append(singleLingusticObject.BuildResultSentence() + " ");
            stringBuilder.Append("[T1: " + Math.Truncate( allTResults.T1 * 1000.0) / 1000.0 + "] ");
            stringBuilder.Append("[T2: " + Math.Truncate(allTResults.T2 * 1000.0) / 1000.0 + "] ");
            stringBuilder.Append("[T3: " + Math.Truncate(allTResults.T3 * 1000.0) / 1000.0 + "] ");
            stringBuilder.Append("[T4: " + Math.Truncate(allTResults.T4 * 1000.0) / 1000.0 + "] ");
            stringBuilder.Append("[T5: " + Math.Truncate(allTResults.T5 * 1000.0) / 1000.0 + "] ");
            stringBuilder.Append("[T6: " + Math.Truncate(allTResults.T6 * 1000.0) / 1000.0 + "] ");
            stringBuilder.Append("[T7: " + Math.Truncate(allTResults.T7 * 1000.0) / 1000.0 + "] ");
            stringBuilder.Append("[T8: " + Math.Truncate(allTResults.T8 * 1000.0) / 1000.0 + "] ");
            if (singleLingusticObject.Qualifier != null)
            {
                stringBuilder.Append("[T9: " + Math.Truncate(allTResults.T9 * 1000.0) / 1000.0 + "] ");
                stringBuilder.Append("[T10: " + Math.Truncate(allTResults.T10 * 1000.0) / 1000.0 + "] ");
                stringBuilder.Append("[T11: " + Math.Truncate(allTResults.T11 * 1000.0) / 1000.0 + "] ");
            }
            stringBuilder.Append("[T1T11: " + Math.Truncate(allTResults.T1T11 * 1000.0) / 1000.0 + "] ");

            return stringBuilder.ToString();
        }
        public AllTValues CalculateAllSummarizationValues()
        {
            AllTValues allTValues = new AllTValues();
            allTValues.T1 = CalculateT1();
            allTValues.T2 = CalculateT2();
            allTValues.T3 = CalculateT3();
            allTValues.T4 = CalculateT4(allTValues.T3);
            allTValues.T5 = CalculateT5();
            allTValues.T1T5 = CalculateT1T5(allTValues);
            allTValues.T6 = CalculateT6();
            allTValues.T7 = CalculateT7();
            allTValues.T8 = CalculateT8();
            allTValues.T9 = CalculateT9();
            allTValues.T10 = CalculateT10();
            allTValues.T11 = CalculateT11();
            allTValues.T1T11 = CalculateT1T11(allTValues);
            return allTValues;

        }

        private double CalculateT1()
        {
            return singleLingusticObject.isAbsolute ? CalculateT1AbsoluteSummarization() : CalculateT1RelativeSummarization();
        }
        public double CalculateT1AbsoluteSummarization()
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
        public double CalculateT1RelativeSummarization()
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

        private double CalculateT2()
        {
            double result = 1.0;
            foreach (var summarizer in singleLingusticObject.Summarizators)
            {
                result *= summarizer.DegreeOfFuzziness();
            }
            return 1.0 - Math.Pow(result, 1.0 / singleLingusticObject.Summarizators.Count);
        }

        private double CalculateT3()
        {
            if (singleLingusticObject.Qualifier != null)
            {
                double sumT = _summarizationResults.Zip(_qualifierResults, (a, b) => a > 0.0 && b > 0.0)
                    .Count(t => t);
                double sumH = _qualifierResults.Count(val => val > 0.0);
                return sumH > 0.0 ? sumT / sumH : 0.0;
            }
            else
            {
                double sumT = _summarizationResults.Count(val => val > 0.0);
                double sumH = LingusticSummarization.CrimesList.Count;
                return sumH > 0.0 ? sumT / sumH : 0.0;
            }
        }

        private double CalculateT4(double t3)
        {
            double resultS = 1.0;
            foreach (var summarizer in singleLingusticObject.Summarizators)
            {
                double resultG = 0.0;
                foreach (SingleCrimeInfo singleCrime in LingusticSummarization.CrimesList)
                {
                    resultG += summarizer.MembershipFunction.GetMembershipFunctionValue(singleCrime.GetAttributeValue(summarizer.AttributeName)) > 0.0 ? 1.0 : 0.0;
                }
                resultS *= (resultG / LingusticSummarization.CrimesList.Count);
            }
            return Math.Abs(resultS - t3);
        }

        private double CalculateT5()
        {
            return 2.0 * Math.Pow(1.0 / 2.0, singleLingusticObject.Summarizators.Count);
        }

        private double CalculateT1T5(AllTValues allTValues)
        {
            return 0.2 * allTValues.T1 + 0.2 * allTValues.T2 + 0.2 * allTValues.T3 + 0.2 * allTValues.T4 +
                            0.2 * allTValues.T5;
        }

        private double CalculateT6()
        {
            double resultXq = 1.0;
            if (singleLingusticObject.Quantifier.IsAbsolute)
                resultXq = singleLingusticObject.Quantifier.X;

            return 1.0 - (singleLingusticObject.Quantifier.MembershipFunction.Support / resultXq);

        }

        private double CalculateT7()
        {
            double resultXq = 1.0;
            if (singleLingusticObject.Quantifier.IsAbsolute)
                resultXq = singleLingusticObject.Quantifier.X;

            return 1.0 - (singleLingusticObject.Quantifier.MembershipFunction.Cardinality / resultXq);
        }

        private double CalculateT8()
        {
            double resultS = 1.0;
            foreach (var summarizer in singleLingusticObject.Summarizators)
            {
                resultS *= summarizer.MembershipFunction.Cardinality / summarizer.X;
            }
            return 1.0 - Math.Pow(resultS, 1.0 / singleLingusticObject.Summarizators.Count);
        }

        private double CalculateT9()
        {
            double resultS = 1.0;
            if (singleLingusticObject.Qualifier != null)
            {
                resultS = singleLingusticObject.Qualifier.DegreeOfFuzziness();
            }
            return 1.0 - Math.Pow(resultS, 1.0 / singleLingusticObject.Summarizators.Count);
        }

        private double CalculateT10()
        {
            double resultS = 1.0;
            if (singleLingusticObject.Qualifier != null)
            {
                resultS = singleLingusticObject.Qualifier.MembershipFunction.Cardinality / singleLingusticObject.Qualifier.X;
            }
            return 1.0 - resultS;
        }

        private double CalculateT11()
        {
            if(singleLingusticObject.Qualifier != null)
                return 2.0 * Math.Pow(1.0 / 2.0, 1.0);
            return 0.0;
        }

        private double CalculateT1T11(AllTValues allTValues)
        {
            return
                (1.0 / 11.0) * allTValues.T1 + (1.0 / 11.0) * allTValues.T2 + (1.0 / 11.0) * allTValues.T3 +
                (1.0 / 11.0) * allTValues.T4 + (1.0 / 11.0) * allTValues.T5 + (1.0 / 11.0) * allTValues.T6 +
                (1.0 / 11.0) * allTValues.T7 + (1.0 / 11.0) * allTValues.T8 + (1.0 / 11.0) * allTValues.T9 +
                (1.0 / 11.0) * allTValues.T10 + (1.0 / 11.0) * allTValues.T11;
        }


    }
}
