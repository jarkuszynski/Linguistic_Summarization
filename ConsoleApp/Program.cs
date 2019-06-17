using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvDataGetter;
using CsvDataGetter.Model;
using Logic.LinguisticSummarization;
using Logic.MembershipFunctions;
using Logic;


namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            SentenceTuple sentenceTuple = SentenceElementsFileReader.getSentenceElementsFromFile();
            LingusticSummarization lingusticSummarization = new LingusticSummarization(sentenceTuple.Qualifiers, sentenceTuple.Quantifiers, sentenceTuple.Summarizators);
            List<string> d = lingusticSummarization.results();
            foreach (var item in d)
            {
                if(item != string.Empty)
                    Console.WriteLine(item);
            }
            Console.Read();
        }
    }
}