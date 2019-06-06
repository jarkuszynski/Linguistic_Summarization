﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.MembershipFunctions;

namespace Logic.LinguisticSummarization
{
    public class Summarizator : FuzzySet
    {
        public string AttributeName { get; set; }
        public Summarizator(string description, string attributeName, IMembershipFunction membershipFunction) : base(description, membershipFunction)
        {
            AttributeName = attributeName;
        }
    }
}