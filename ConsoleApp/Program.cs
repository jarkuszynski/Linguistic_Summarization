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
            string filepath = Directory.GetParent(workingDirectory).Parent.Parent.FullName + "\\ksr.csv";
            var example = ReadAllData.ReadData(filepath);
            var result = example.OrderBy(i => i.GetAverageParticipantsAge()).ToList();
            Console.WriteLine(result[0]);
            double dateFullPeriod = (SingleCrimeInfo.EndDate - SingleCrimeInfo.BeginDate).TotalDays;
            SentenceTuple sentenceTuple = SentenceElementsFileReader.getSentenceElementsFromFile();
            LingusticSummarization lingusticSummarization = new LingusticSummarization(sentenceTuple.Qualifiers, sentenceTuple.Quantifiers, sentenceTuple.Summarizators, example);
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