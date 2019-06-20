using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.MembershipFunctions
{
    public class TrapezoidalMembershipFunction : IMembershipFunction
    {
        private readonly double _a;
        private readonly double _b;
        private readonly double _c;
        private readonly double _d;

        public string Name { get; }

        public TrapezoidalMembershipFunction(double a, double b, double c, double d)
        {
            _a = a;
            _b = b;
            _c = c;
            _d = d;
            Name = $"trapezoidal({_a}, {_b}, {_c}, {_d})";
        }

        public double GetMembershipFunctionValue(double valueToCalc)
        {
            if (valueToCalc < _a || valueToCalc > _d)
            {
                return 0.0;
            }
            else if(_a == _b || _c == _d)
            {
                return 1.0;
            }

            else if (_a <= valueToCalc && valueToCalc <= _b)
            {
                return (1.0 * (valueToCalc - _a) / (_b - _a));
            }

            else if (_b <= valueToCalc && valueToCalc <= _c)
            {
                return 1.0;
            }

            else if (_c <= valueToCalc && valueToCalc <= _d)
            {
                return (1.0 * (_d - valueToCalc) / (_d - _c));
            }
            else
            {
                return 0.0;
            }
        }

        public double Cardinality => (Math.Abs(_a - _d) + Math.Abs(_c - _b)) / 2d;
        public double Support => Math.Abs(_d - _a);
    }
}
