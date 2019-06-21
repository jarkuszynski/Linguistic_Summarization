using System;
using System.Collections.Generic;
using CsvDataGetter.Model;
using Logic.LinguisticSummarization;
using Logic.MembershipFunctions;
using Logic.ScenarioOperations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicTests
{
    [TestClass]
    public class TValuesTests
    {
        List<SingleCrimeInfo> crimesList;
        BuildScenarioSentence buildScenarioSentence;
        IMembershipFunction membershipFunction;

        [TestInitialize]
        public void Initialize()
        {
            crimesList = new List<SingleCrimeInfo>();
            SingleCrimeInfo c1 = new SingleCrimeInfo();
            c1.Killed = 5;
            SingleCrimeInfo c2 = new SingleCrimeInfo();
            c2.Killed = 6;
            SingleCrimeInfo c3 = new SingleCrimeInfo();
            c2.Killed = 7;
            crimesList.Add(c1);
            crimesList.Add(c2);
            crimesList.Add(c3);
            membershipFunction = new TriangleMembershipFunction(5, 7, 6);
            Summarizator s1 = new Summarizator("duzo zabojstw", "Number of Killed", membershipFunction);
            
        }
        [TestMethod]
        public void Triangle_Support_ShouldBeOk()
        {
            Assert.AreEqual(2.0, membershipFunction.Support);
        }

        [TestMethod]
        public void Triangle_Cardinality_ShouldBeOk()
        {
            Assert.AreEqual(1.0, membershipFunction.Cardinality);
        }

        [TestMethod]
        public void Trapezoidal_Support_ShouldBeOk()
        {
            membershipFunction = new TrapezoidalMembershipFunction(5, 5, 7, 7);
            Assert.AreEqual(2.0, membershipFunction.Support);
        }

        [TestMethod]
        public void Trapezoidal_Cardinality_ShouldBeOk()
        {
            membershipFunction = new TrapezoidalMembershipFunction(5, 5, 7, 7);
            Assert.AreEqual(2.0, membershipFunction.Cardinality);
        }



    }
}
