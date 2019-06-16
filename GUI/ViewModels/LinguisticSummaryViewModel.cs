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

        private string _generationStep;

        public string GenerationStep
        {
            get => _generationStep;
            set => SetProperty(ref _generationStep, value);
        }

        public int SummarizatorsCount { get; }
        public int QualifiersCount { get; }
        public int QuantifiersCount { get; }


        public AsyncCommand GenerateLingusiticSummaryFirstForm { get; }
        public AsyncCommand GenerateLingusiticSummarySecondForm { get; }


        public LinguisticSummaryViewModel()
        {
            SummarizatorsCount = SummaryContext.Instance.Summarizators.Count(sum => sum.IsChecked);
            QualifiersCount = SummaryContext.Instance.Qualifiers.Count(qual => qual.IsChecked);
            QuantifiersCount = SummaryContext.Instance.Quantifiers.Count(quan => quan.IsChecked);

            GenerateLingusiticSummaryFirstForm = new AsyncCommand(async () =>
                await Task.Run(() => GenerateFirstFormSummary()));
        }

        private void GenerateFirstFormSummary()
        {
            var destPath = FileSystemHelper.GetSaveFilePath();
            if (string.IsNullOrEmpty(destPath)) return;

            var quants = SummaryContext.Instance.Quantifiers.Where(q => q.IsChecked).Select(q => q.Quantifier).ToList();
            var quals = SummaryContext.Instance.Qualifiers.Where(q => q.IsChecked).Select(q => q.Qualifier).ToList();
            var summs = SummaryContext.Instance.Summarizators.Where(q => q.IsChecked).Select(s => s.Summarizator).ToList();
            var logicalOperation = _summarizersOperation;
            var singleCrimeInfos = SummaryContext.Instance.SingleCrimeInfos;

            GenerationStep = "Generating summaries";

            LingusticSummarization linguisticSummaries = new LingusticSummarization(quals, quants, summs, singleCrimeInfos);

            var results = linguisticSummaries.results();
            string workingDirectory = Environment.CurrentDirectory;
            string filepath = Directory.GetParent(workingDirectory).Parent.Parent.FullName + "\\results.txt";
            SaveAllData.SaveToFile(results, filepath, _t1Threshold);
        }
    }
}
