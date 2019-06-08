using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvDataGetter.Model;
using Logic.ScenarioOperations;
using Logic.LinguisticSummarization;

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
            BuildScenarioSentence buildScenarioSentence = new BuildScenarioSentence();
            foreach (var item in AllSummarizationScenario)
            {
                vs.Add(buildScenarioSentence.GetScenarioResult(item));
            }
            return vs;
        }

        /*
         * 1. stworzenie pomieszanych algorytmow do obliczenia podsumowania
         * 2. W każdym z algorytmów do obliczenia T1[r]
         * 3. Wygenerowanie podsumowania na podstawie zmiennych pol ( tych callych sumaryzatorow )
         * 4. Przejscie do nastepnego podsumowania
         */

        /*
        * Na podstawie odczytanych z pliku pól kwantyfikatorów, kwalifikatorów i sumaryzatorów stworzyć ich ogólną listę do programu
        * W programie przekazać listę do klasy generującej jej kombinację - dwa warianty, z kwalifiktorami i bez nich
        * Wszystkie możliwe kombinacje. 
        * jak będę wczytane wszystkie możliwe kombinacje, należy przeprowadzić operację obliczenia R
        * Następnie trzeba zaimplementować funkcje funkcje obliczające stopień prawdziwości
        */

        public List<string> resultsFromScenaris ()
        {
            List<string> resultsString = new List<string>();
            /*
             * 1. bierze każde ze scenariuszy
             * wrzuca je do funkcji obliczającej wartość scenariusza
             * zwraca wynik
             * łączy wynik z każdym scenariusze,
             * tworzy listę ze zdania scenariuszy
             */
            return new List<string>();
        }

        public void generateMixedLinqusticObjects()
        {
            //zmiksowanie wszystkich list ze sobą. Raz z kwalifikatorami, raz bez 
            List<List<Summarizator>> summarizatorCombinations = new List<List<Summarizator>>();
            List<SingleLingusticObject> allPossibleSingleLingusticObjects = new List<SingleLingusticObject>();
            summarizatorCombinations = allPossibleSummarizatorCombinations();
            List<SingleLingusticObject> singleSummarizationObjects = new List<SingleLingusticObject>();
            foreach (Quantifier quantifier in Quantifiers)
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
                
            foreach (List<Summarizator> summarizators in summarizatorCombinations)
            {
                allPossibleSingleLingusticObjects.Add(new SingleLingusticObject(summarizators, quantifier, null, operationType));
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
