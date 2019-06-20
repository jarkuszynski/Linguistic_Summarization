using GUI.Base;
using Logic.MembershipFunctions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModels
{
    public class MembershipFunctionViewModel: BindableBase
    {

        public List<string> MEMBERSHIP_TYPES { get; } = new List<string> { "trapezoidal", "triangular" };

        private string _membershipType;

        public string MembershipType
        {
            get => _membershipType;
            set => SetProperty(ref _membershipType, value);
        }

        private string _parametersString;

        public string ParameterString
        {
            get => _parametersString;
            set => SetProperty(ref _parametersString, value);
        }

        public MembershipFunctionViewModel()
        {

        }

        public IMembershipFunction GetMembershipFunction()
        {
            if (string.IsNullOrEmpty(ParameterString)) return null;
            var memParams = ParameterString.Trim().Split(' ');

            switch (MembershipType)
            {
                case "trapezoidal":
                    if (memParams.Length != 4) return null;
                    return new TrapezoidalMembershipFunction(double.Parse(memParams[0], CultureInfo.InvariantCulture), double.Parse(memParams[1], CultureInfo.InvariantCulture), double.Parse(memParams[2], CultureInfo.InvariantCulture), double.Parse(memParams[3], CultureInfo.InvariantCulture));
                case "triangular":
                    if (memParams.Length != 3) return null;
                    return new TriangleMembershipFunction(double.Parse(memParams[0], CultureInfo.InvariantCulture), double.Parse(memParams[1], CultureInfo.InvariantCulture), double.Parse(memParams[2], CultureInfo.InvariantCulture));
            }

            return null;
        }

    }
}
