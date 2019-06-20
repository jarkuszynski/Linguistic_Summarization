using CsvDataGetter.Model;
using GUI.Base;
using GUI.Model;
using GUI.Utility;
using Logic.LinguisticSummarization;
using Logic.MembershipFunctions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModels
{
    public class SummarizersViewModel: BindableBase
    {
        public ObservableCollection<CheckableSummarizator> Summarizators { get; }

        public ObservableCollection<Checkable> Attributes { get; }

        public MembershipFunctionViewModel MembershipFunctionView { get; }

        public RelayCommand SelectAll { get; }
        public RelayCommand DeselectAll { get; }
        public RelayCommand CreateSummarizator { get; }

        private string _description;

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public SummarizersViewModel()
        {
            MembershipFunctionView = new MembershipFunctionViewModel();
            Summarizators = new ObservableCollection<CheckableSummarizator>
                (SummaryContext.Instance.Summarizators);

            List<string> excludedProperties = new List<string>() { "Id", "Name" };
            Attributes = new ObservableCollection<Checkable>(SingleCrimeInfo.GetAvailableProperties().Select(p => new Checkable() { Name = p }));

            SelectAll = new RelayCommand(() => SetAllCheckables(true));
            DeselectAll = new RelayCommand(() => SetAllCheckables(false));
            CreateSummarizator = new RelayCommand(() => CreateSummarizatorFromForm());
        }

        private void CreateSummarizatorFromForm()
        {
            if (string.IsNullOrEmpty(Description))
            {
                Messanger.DisplayError("Unable to create quantifier withour label");
                return;
            }

            var selected = Attributes.Where(a => a.IsChecked).ToList();

            foreach (var at in selected)
            {
                IMembershipFunction memFun = MembershipFunctionView.GetMembershipFunction();
                if (memFun == null)
                {
                    Messanger.DisplayError("Unable to create membership function with the given parameters");
                    return;
                }

                var summ = new Summarizator(Description, at.Name, memFun);
                var checkableSumm = new CheckableSummarizator(summ, true);


                SummaryContext.Instance.Summarizators.Add(checkableSumm);
                Summarizators.Add(checkableSumm);
            }


        }

        public void SetAllCheckables(bool isChecked)
        {
            foreach (var summ in Summarizators)
            {
                summ.IsChecked = isChecked;
            }
        }
    }
}
