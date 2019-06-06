using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvDataGetter;
using CsvDataGetter.Model;

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
        }
    }
}
