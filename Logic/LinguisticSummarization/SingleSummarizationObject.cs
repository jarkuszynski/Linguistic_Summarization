using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.LinguisticSummarization
{
    public class SingleSummarizationObject
    {
        public SingleSummarizationObject(List<Summarizator> summarizators, Quantifier quantifier, string attributeName, Qualifier qualifier )
        {
            Summarizators = summarizators;
            Qualifier = qualifier;
            Quantifier = quantifier;
            AttributeName = attributeName;
            isAbsolute = qualifier != null ? true : false;
            
        }
        public bool isAbsolute { get; set; }
        public List<Summarizator> Summarizators { get; set; }
        public Qualifier Qualifier { get; set; }
        public Quantifier Quantifier { get; set; }
        public string AttributeName { get; set; }

    }
}
