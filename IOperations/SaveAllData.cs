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
        public static void SaveToFile(List<BuildScenarioSentence> sentences, string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (BuildScenarioSentence sentence in sentences)
                {
                    sw.WriteLine(sentence.GetScenarioResult());
                }
            }
        }
    }
}
