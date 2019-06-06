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
        public List<Qualifier> Qualifiers { get; set; }
        public List<Quantifier> Quantifiers { get; set; }
        public List<Summarizator> Summarizators { get; set; }
        public List<string> chosenPropretiesToSummarize { get; set; }
        public bool IsAbsolute { get; set; }
        public List<SingleCrimeInfo> CrimesList { get; set; }
        

        /*
         * 1. stworzenie pomieszanych algorytmow do obliczenia podsumowania
         * 2. W każdym z algorytmów do obliczenia T1[r]
         * 3. Wygenerowanie podsumowania na podstawie zmiennych pol ( tych callych sumaryzatorow )
         * 4. Przejscie do nastepnego podsumowania
         */

        public void generateMixedSummarizations()
        {
            //
        }
        
    }
}
