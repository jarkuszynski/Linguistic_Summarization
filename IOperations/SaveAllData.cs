using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.ScenarioOperations;

namespace IOperations
{
    public static class SaveAllData
    {
        public static void SaveToFile(List<string> sentences, string filePath, double threshold)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (string sentence in sentences)
                {
                    sw.WriteLine(sentence);
                }
            }
        }
    }
}
