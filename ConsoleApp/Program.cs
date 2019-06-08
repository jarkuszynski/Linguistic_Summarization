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

            double dateFullPeriod = (SingleCrimeInfo.EndDate - SingleCrimeInfo.BeginDate).TotalDays;
            List<Summarizator> summarizators = new List<Summarizator>();
            List<Qualifier> qualifiers = new List<Qualifier>();
            List<Quantifier> quantifiers = new List<Quantifier>();
            summarizators.Add(new Summarizator("siema kurwa", "Number of Killed", new TriangleMembershipFunction(2, 7, 4), 0, 20));
            summarizators.Add(new Summarizator("duzo rannych", "Number of Injured", new TrapezoidalMembershipFunction(2, 7, 9, 9), 0, 10));


            //private const double _southEndCoord = 24.520833;
            //private const double _westEndCoord = -124.771667;
            //private const double _northEndCoord = 49.384358;
            //private const double _eastEndCoord = -66.946944;

            qualifiers.Add(new Qualifier("bardziej na polnocy", "Horizontal", new TriangleMembershipFunction(35, 49, 49), 23, 20));
            qualifiers.Add(new Qualifier("bardziej na poludniu", "Vertical", new TrapezoidalMembershipFunction(-80.0, -110.0, -123.0, -123.0), -66, -124.0));

            quantifiers.Add(new Quantifier("malo kurwa", new TriangleMembershipFunction(1, 3, 2), Quantifier.QuantifierType.Relative, 0, 1));

            LingusticSummarization lingusticSummarization = new LingusticSummarization(new List<Qualifier>(), quantifiers, summarizators, example);
        }
    }
}