using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logic.LingusticSummarization;

namespace Logic.LinguisticSummarization
{
    public class SingleLingusticObject
    {
        public SingleLingusticObject(List<Summarizator> summarizators, Quantifier quantifier, Qualifier qualifier, OperationType op )
        {
            Summarizators = summarizators;
            Qualifier = qualifier;
            Quantifier = quantifier;
            isAbsolute = ( qualifier == null) ? true : false;
            operation = op;
            
        }
        public OperationType operation { get; set; }
        public bool isAbsolute { get; set; }
        public List<Summarizator> Summarizators { get; set; }
        public Qualifier Qualifier { get; set; }
        public Quantifier Quantifier { get; set; }
        
        public string BuildResultSentence()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("in crime summary");
            stringBuilder.Append(Quantifier.Description);
            for (int i = 0; i < Summarizators.Count; i++)
            { 
                if(i == Summarizators.Count - 1)
                {
                    stringBuilder.Append(' ');
                    stringBuilder.Append(Summarizators[i].Description);

                }
                else
                {
                    stringBuilder.Append(' ');
                    stringBuilder.Append(Summarizators[i].Description);
                    stringBuilder.Append(' ');
                    stringBuilder.Append(operation.ToString());
                }

            }
            if (Qualifier != null)
            {
                stringBuilder.Append(' ');
                stringBuilder.Append(Qualifier.Description);
            }

            return stringBuilder.ToString();

        }

    }
}
