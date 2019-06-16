using Logic.LinguisticSummarization;

namespace GUI.Model
{
    public class CheckableQuantifier : Checkable
    {
        public Quantifier Quantifier { get; }

        public CheckableQuantifier(Quantifier quan, bool isChecked = false)
        {
            Quantifier = quan;
            IsChecked = isChecked;
        }
    }
}
