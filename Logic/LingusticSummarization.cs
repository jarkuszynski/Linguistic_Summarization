using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvDataGetter.Model;
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
        }

        public List<Qualifier> Qualifiers { get; set; }
        public List<Quantifier> Quantifiers { get; set; }
        public List<Summarizator> Summarizators { get; set; }
        public List<SingleCrimeInfo> CrimesList { get; set; }
        public List<SingleSummarizationObject> AllSummarizationScenario { get; set; }

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
        public List<SingleSummarizationObject> generateMixedSummarizationObjects()
        {
            //zmiksowanie wszystkich list ze sobą. Raz z kwalifikatorami, raz bez 
            throw new NotImplementedException();
        }
        
    }
}
