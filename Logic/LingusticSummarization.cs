using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvDataGetter.Model;
using Logic.ScenarioOperations;
using Logic.LinguisticSummarization;
using CsvDataGetter;

namespace Logic
{
    public class LingusticSummarization
    {
        public LingusticSummarization(List<Qualifier> qualifiers, List<Quantifier> quantifiers, List<Summarizator> summarizators, List<SingleCrimeInfo> crimesList)
        {
            Qualifiers = qualifiers;
            Quantifiers = quantifiers;
            Summarizators = summarizators;
            CrimesList = crimesList;
            AllSummarizationScenario = new List<SingleLingusticObject>();
            generateMixedLinqusticObjects();

        }
        public OperationType operationType { get; set; }

        public List<Qualifier> Qualifiers { get; set; }
        public List<Quantifier> Quantifiers { get; set; }
        public List<Summarizator> Summarizators { get; set; }
        public static List<SingleCrimeInfo> CrimesList { get; set; }
        public List<SingleLingusticObject> AllSummarizationScenario { get; set; }
        public static List<SingleCrimeInfo> data;
        public List<string> results()
        {
            List<string> vs = new List<string>();
            foreach (var item in AllSummarizationScenario)
            {
                BuildScenarioSentence buildScenarioSentence = new BuildScenarioSentence(item);
                vs.Add(buildScenarioSentence.GetScenarioResult());
            }
            return vs;
        }

        public List<string> resultsFromScenaris ()
        {
            List<string> resultsString = new List<string>();
            return new List<string>();
        }

        public void generateMixedLinqusticObjects()
        {
            //zmiksowanie wszystkich list ze sobą. Raz z kwalifikatorami, raz bez 
            List<List<Summarizator>> summarizatorCombinations = new List<List<Summarizator>>();
            List<SingleLingusticObject> allPossibleSingleLingusticObjects = new List<SingleLingusticObject>();
            summarizatorCombinations = allPossibleSummarizatorCombinations();
            foreach (Quantifier quantifier in Quantifiers)
            {
                if (!quantifier.IsAbsolute)
                {
                    if (Qualifiers.Count >= 1)
                    {
                        foreach (Qualifier qualifier in Qualifiers)
                        {
                            foreach (List<Summarizator> summarizators in summarizatorCombinations)
                            {
                                allPossibleSingleLingusticObjects.Add(new SingleLingusticObject(summarizators, quantifier, qualifier, operationType));
                            }
                        }
                    }
                }
                else
                {
                    foreach (List<Summarizator> summarizators in summarizatorCombinations)
                    {
                        allPossibleSingleLingusticObjects.Add(new SingleLingusticObject(summarizators, quantifier, null, operationType));
                    }
                }



            }
            AllSummarizationScenario = allPossibleSingleLingusticObjects;
        }
        public List<List<Summarizator>> allPossibleSummarizatorCombinations()
        {
            List<List<Summarizator>> SummarizatorsCombinations = new List<List<Summarizator>>();
            for (int i = 1; i <= Summarizators.Count; i++)
            {
                Combinatorics.Collections.Combinations<Summarizator> c = new Combinatorics.Collections.Combinations<Summarizator>(Summarizators, i);
                foreach (List<Summarizator> v in c)
                {
                    SummarizatorsCombinations.Add(v);
                }
            }
            return SummarizatorsCombinations;
        }

    }

}
