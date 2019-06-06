using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.LinguisticSummarization
{
    public class SingleSummarizationObject
    {
        public SingleSummarizationObject(Summarizator summarizator, Quantifier quantifier, string attributeName, Qualifier qualifier )
        {
            Summarizator = summarizator;
            Qualifier = qualifier;
            Quantifier = quantifier;
            AttributeName = attributeName;
            isAbsolute = qualifier != null ? true : false;
            
        }
        public bool isAbsolute { get; set; }
        public Summarizator Summarizator { get; set; }
        public Qualifier Qualifier { get; set; }
        public Quantifier Quantifier { get; set; }
        public string AttributeName { get; set; }

    }
}
