﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.MembershipFunctions;

namespace Logic.LinguisticSummarization
{
    public class Quantifier : FuzzySet
    {
        public QuantifierType Type { get; set; }
        
        public Quantifier(string description, IMembershipFunction membershipFunction, QuantifierType type, double minValueOfColumn, double maxValueOfColumn) : base(description, membershipFunction, minValueOfColumn, maxValueOfColumn)
        {
            Type = type;
            IsAbsolute = Type == QuantifierType.Absolute ? true : false;
        }

        public bool IsAbsolute { get; set; }

        public enum QuantifierType
        {
            Relative,
            Absolute
        }
    }
}
