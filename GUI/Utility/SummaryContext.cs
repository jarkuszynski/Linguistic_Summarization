using Logic;
using GUI.Model;
using System;
using System.Collections.Generic;
using CsvDataGetter.Model;

namespace GUI.Utility
{
    public class SummaryContext
    {
        private static Lazy<SummaryContext> _lazy = new Lazy<SummaryContext>(() => new SummaryContext());

        public static SummaryContext Instance => _lazy.Value;


        public List<CheckableQuantifier> Quantifiers { get; }
        public List<CheckableSummarizator> Summarizators { get; }
        public List<CheckableQualifier> Qualifiers { get; }

        public List<SingleCrimeInfo> SingleCrimeInfos { get; }

        public OperationType OperationType { get; set; }


        public SummaryContext()
        {
            SingleCrimeInfos = LingusticSummarization.CrimesList;
            Quantifiers = new List<CheckableQuantifier>();
            Summarizators = new List<CheckableSummarizator>();
            Qualifiers = new List<CheckableQualifier>();
        }

    }
}
