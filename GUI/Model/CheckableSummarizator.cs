using Logic.LinguisticSummarization;

namespace GUI.Model
{
    public class CheckableSummarizator : Checkable
    {
        public Summarizator Summarizator { get; }

        public CheckableSummarizator(Summarizator summ, bool isChecked = false)
        {
            Summarizator = summ;
            IsChecked = isChecked;
        }
    }
}
