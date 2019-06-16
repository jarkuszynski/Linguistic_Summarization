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
using static Logic.LinguisticSummarization.Quantifier;

namespace GUI.ViewModels
{
    public class QuantifiersViewModel: BindableBase
    {
        public ObservableCollection<CheckableQuantifier> Quantifiers { get; }

        public MembershipFunctionViewModel MembershipFunctionView { get; }

        public RelayCommand SelectAll { get; }
        public RelayCommand DeselectAll { get; }
        public RelayCommand CreateQuantifier { get; }


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

        private bool _isAbsolute = false;

        public bool IsAbsolute
        {
            get => _isAbsolute;
            set => SetProperty(ref _isAbsolute, value);
        }


        public QuantifiersViewModel()
        {
            MembershipFunctionView = new MembershipFunctionViewModel();
            Quantifiers = new ObservableCollection<CheckableQuantifier>(
                SummaryContext.Instance.Quantifiers);
            SelectAll = new RelayCommand(() => SetAllCheckables(true));
            DeselectAll = new RelayCommand(() => SetAllCheckables(false));
            CreateQuantifier = new RelayCommand(() => CreateQuantifierFromForm());

        }

        public void CreateQuantifierFromForm()
        {
            if (string.IsNullOrEmpty(Description))
            {
                Messanger.DisplayError("Unable to create quantifier withour label");
                return;
            }

            IMembershipFunction memFun = MembershipFunctionView.GetMembershipFunction();
            if (memFun == null)
            {
                Messanger.DisplayError("Unable to create membership function with the given parameters");
                return;
            }

            var quan = new Quantifier(Description, memFun, IsAbsolute ? QuantifierType.Absolute : QuantifierType.Relative, XMin, XMax);
            var checkableQuan = new CheckableQuantifier(quan, true);


            SummaryContext.Instance.Quantifiers.Add(checkableQuan);
            Quantifiers.Add(checkableQuan);

        }

        public void SetAllCheckables(bool isChecked)
        {
            foreach (var quan in Quantifiers)
            {
                quan.IsChecked = isChecked;
            }
        }
    }
}
