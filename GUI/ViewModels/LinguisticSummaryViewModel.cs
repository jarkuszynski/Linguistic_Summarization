using GUI.Base;
using GUI.Utility;
using Logic;
using Logic.LinguisticSummarization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOperations;
using System.IO;

namespace GUI.ViewModels
{
    public class LinguisticSummaryViewModel : BindableBase
    {
        private double _t1Threshold = 0.3d;

        private LingusticSummarization LingusticSummarization;

        public double T1Threshold
        {
            get => _t1Threshold;
            set => SetProperty(ref _t1Threshold, value);
        }


        public string[] SummarizersOperationsList { get; set; } = new[] { "AND", "OR" };

        private OperationType _summarizersOperation;
        private string _currentSummarizersOperation = "AND";

        public string SummarizersOperation
        {
            get { return _currentSummarizersOperation; }
            set
            {
                _summarizersOperation = value == "AND" ? OperationType.And : OperationType.Or;
                SetProperty(ref _currentSummarizersOperation, value);
            }
        }

        public int SummarizatorsCount { get; }
        public int QualifiersCount { get; }
        public int QuantifiersCount { get; }

        private string _isDone;
        public string isDone
        {
            get => _isDone;
            set => SetProperty(ref _isDone, value);
        }

        public AsyncCommand GenerateLingusiticSummary { get; }

        public LinguisticSummaryViewModel()
        {
            SummarizatorsCount = SummaryContext.Instance.Summarizators.Count(sum => sum.IsChecked);
            QualifiersCount = SummaryContext.Instance.Qualifiers.Count(qual => qual.IsChecked);
            QuantifiersCount = SummaryContext.Instance.Quantifiers.Count(quan => quan.IsChecked);
            string workingDirectory = Environment.CurrentDirectory;
            string filepath = Directory.GetParent(workingDirectory).Parent.Parent.FullName + "\\ksr.db";
            //LingusticSummarization = new LingusticSummarization(SummaryContext.Instance.Qualifiers.Where(q => q.IsChecked).Select(q => q.Qualifier).ToList(), SummaryContext.Instance.Quantifiers.Where(q => q.IsChecked).Select(q => q.Quantifier).ToList(), SummaryContext.Instance.Summarizators.Where(q => q.IsChecked).Select(s => s.Summarizator).ToList());

            GenerateLingusiticSummary = new AsyncCommand(async () =>
                await Task.Run(() => GenerateFormSummary()));
        }

        private void GenerateFormSummary()
        {
            isDone = "Liczę...";
            var logicalOperation = _summarizersOperation;
            LingusticSummarization lingusticSummarization = new LingusticSummarization(logicalOperation);
            var quants = SummaryContext.Instance.Quantifiers.Where(q => q.IsChecked).Select(q => q.Quantifier).ToList();
            var quals = SummaryContext.Instance.Qualifiers.Where(q => q.IsChecked).Select(q => q.Qualifier).ToList();
            var summs = SummaryContext.Instance.Summarizators.Where(q => q.IsChecked).Select(s => s.Summarizator).ToList();
            lingusticSummarization.Qualifiers = quals;
            lingusticSummarization.Quantifiers = quants;
            lingusticSummarization.Summarizators = summs;
            lingusticSummarization.generateMixedLinqusticObjects();
            var results = lingusticSummarization.results(_t1Threshold);
            string workingDirectory = Environment.CurrentDirectory;
            string filepath = Directory.GetParent(workingDirectory).Parent.Parent.FullName + "\\results.txt";
            SaveAllData.SaveToFile(results, filepath);
            isDone = "Skonczone";
        }
    }
}
