using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.MembershipFunctions
{
    public class TriangleMembershipFunction : IMembershipFunction
    {
        private readonly double _a;
        private readonly double _b;
        private readonly double _m;

        public TriangleMembershipFunction(double a, double b, double m)
        {
            _a = a;
            _b = b;
            if(m >= a && m <= b)
            {
                _m = m;
            }
            else
            {
                throw new ArgumentException("m must be between range of (a,b)");
            }
        }

        public double GetMembershipFunctionValue(double valueToCalc)
        {
            if (valueToCalc <= _a)
            {
                return 0.0;
            }
            else if (_a < valueToCalc && _a <= _m)
            {
                return (1.0 * (valueToCalc - _a) / (_m - _a));
            }
            else if (_m < valueToCalc && valueToCalc < _b)
            {
                return (1.0 * (_b - valueToCalc) / (_b - _m));
            }
            else if(valueToCalc >= _b)
            {
                return 0.0;
            }
            else
            {
                return 0.0;
            }
        }
    }
}
