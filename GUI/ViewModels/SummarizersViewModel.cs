﻿using CsvDataGetter.Model;
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
        public ObservableCollection<CheckableSummarizator> Summarizers { get; }

        public ObservableCollection<Checkable> Attributes { get; }

        public MembershipFunctionViewModel MembershipViewModel { get; }

        public RelayCommand SelectAll { get; }
        public RelayCommand DeselectAll { get; }
        public RelayCommand CreateSummarizer { get; }

        private string _label;

        public string Description
        {
            get => _label;
            set => SetProperty(ref _label, value);
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


        public SummarizersViewModel()
        {
            MembershipViewModel = new MembershipFunctionViewModel();
            Summarizers = new ObservableCollection<CheckableSummarizator>
                (SummaryContext.Instance.Summarizators);

            List<string> excludedProperties = new List<string>() { "Id", "Name" };
            Attributes = new ObservableCollection<Checkable>(typeof(SingleCrimeInfo).GetProperties().Where(p => !excludedProperties.Contains(p.Name)).Select(p => new Checkable() { Name = p.Name }));

            SelectAll = new RelayCommand(() => SetAllCheckables(true));
            DeselectAll = new RelayCommand(() => SetAllCheckables(false));
            CreateSummarizer = new RelayCommand(() => CreateSummarizerFromForm());
        }

        private void CreateSummarizerFromForm()
        {
            if (string.IsNullOrEmpty(Description))
            {
                Messanger.DisplayError("Unable to create quantifier withour label");
                return;
            }

            var selected = Attributes.Where(a => a.IsChecked).ToList();

            foreach (var at in selected)
            {
                IMembershipFunction memFun = MembershipViewModel.GetMembershipFunction();
                if (memFun == null)
                {
                    Messanger.DisplayError("Unable to create membership function with the given parameters");
                    return;
                }

                var summ = new Summarizator(Description, at.Name, memFun, XMin, XMax);
                var checkableSumm = new CheckableSummarizator(summ, true);


                SummaryContext.Instance.Summarizators.Add(checkableSumm);
                Summarizers.Add(checkableSumm);
            }


        }

        public void SetAllCheckables(bool isChecked)
        {
            foreach (var summ in Summarizers)
            {
                summ.IsChecked = isChecked;
            }
        }
    }
}
