using CsvDataGetter;
using GUI.Base;
using GUI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public object _activeViewModel;

        public object ActiveViewModel
        {
            get => _activeViewModel;
            set => SetProperty(ref _activeViewModel, value);
        }

        public RelayCommand<string> Navigate { get; }

        public RelayCommand Save { get; }

        public MainViewModel()
        {

            Navigate = new RelayCommand<string>((dest) => NavigateTo(dest));

            SummaryContext sumContext = SummaryContext.Instance;

            Save = new RelayCommand(SaveFuzzySetsToFile);

            //default file

            SentenceTuple sentenceTuple = SentenceElementsFileReader.getSentenceElementsFromFile();
            SummaryContext.Instance.Quantifiers.AddRange(sentenceTuple.Quantifiers.Select(q => new Model.CheckableQuantifier(q, true)));
            SummaryContext.Instance.Qualifiers.AddRange(sentenceTuple.Qualifiers.Select(q => new Model.CheckableQualifier(q, true)));
            SummaryContext.Instance.Summarizators.AddRange(sentenceTuple.Summarizators.Select(s => new Model.CheckableSummarizator(s, true)));
        }


        public void NavigateTo(string dest)
        {
            switch (dest)
            {

                case "SUMMARIZERS":
                    ActiveViewModel = new SummarizersViewModel();
                    break;
                case "QUALIFIERS":
                    ActiveViewModel = new QualifiersViewModel();
                    break;
                case "QUANTIFIERS":
                    ActiveViewModel = new QuantifiersViewModel();
                    break;
                case "SUMMARY":
                    ActiveViewModel = new LinguisticSummaryViewModel();
                    break;
            }
        }

        public void SaveFuzzySetsToFile()
        {
            string dest = FileSystemHelper.GetSaveFilePath();
            if (string.IsNullOrEmpty(dest)) return;

            SummaryContext sumContext = SummaryContext.Instance;

            SentenceElementsFileReader.SaveFuzzySetsToFile(
                sumContext.Quantifiers.Select(q => q.Quantifier).ToList(),
                sumContext.Qualifiers.Select(q => q.Qualifier).ToList(),
                sumContext.Summarizators.Select(q => q.Summarizator).ToList(),
                sumContext.OperationType,
                dest);
        }
    }
}
