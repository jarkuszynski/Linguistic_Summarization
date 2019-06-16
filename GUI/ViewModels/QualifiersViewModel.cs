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
    public class QualifiersViewModel: BindableBase
    {
        public ObservableCollection<CheckableQualifier> Qualifiers { get; }

        public ObservableCollection<Checkable> Attributes { get; }

        public MembershipFunctionViewModel MembershipFunctionView { get; }

        public RelayCommand SelectAll { get; }
        public RelayCommand DeselectAll { get; }
        public RelayCommand CreateQualifier { get; }

        private string _description;

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private double _xMin = 0d;

        public double XMin
        {
            get => _xMin;
            set => SetProperty(ref _xMin, value);
        }

        private double _xMax = 100d;

        public double XMax
        {
            get => _xMax;
            set => SetProperty(ref _xMax, value);
        }


        public QualifiersViewModel()
        {
            MembershipFunctionView = new MembershipFunctionViewModel();
            Qualifiers = new ObservableCollection<CheckableQualifier>
                (SummaryContext.Instance.Qualifiers);

            List<string> excludedProperties = new List<string>() { "Id", "Name" };
            Attributes = new ObservableCollection<Checkable>(SingleCrimeInfo.GetAvailableProperties().Select(p => new Checkable() { Name = p }));

            SelectAll = new RelayCommand(() => SetAllCheckables(true));
            DeselectAll = new RelayCommand(() => SetAllCheckables(false));
            CreateQualifier = new RelayCommand(() => CreateQualifiersFromForm());
        }

        private void CreateQualifiersFromForm()
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

                var summ = new Qualifier(Description, at.Name, memFun, XMin, XMax);
                var checkableSumm = new CheckableQualifier(summ, true);


                SummaryContext.Instance.Qualifiers.Add(checkableSumm);
                Qualifiers.Add(checkableSumm);
            }


        }

        public void SetAllCheckables(bool isChecked)
        {
            foreach (var qual in Qualifiers)
            {
                qual.IsChecked = isChecked;
            }
        }
    }
}
