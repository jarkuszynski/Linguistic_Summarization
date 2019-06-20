using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.MembershipFunctions;

namespace LogicTests
{
    /// <summary>
    /// Opis podsumowujący elementu TrapezoidMembershipFunctionTests
    /// </summary>
    [TestClass]
    public class TrapezoidMembershipFunctionTests
    {
        [TestMethod]
        public void Trapezoid1245_ShouldBe0Left()
        {
            IMembershipFunction trapezoidalMembershipFunction = new TrapezoidalMembershipFunction(1, 2, 4, 5);
            double expected = 0;
            double valueToCount = 0.5;
            double result = trapezoidalMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Trapezoid1245_ShouldBe0Right5_5()
        {
            IMembershipFunction trapezoidalMembershipFunction = new TrapezoidalMembershipFunction(1, 2, 4, 5);
            double expected = 0;
            double valueToCount = 5.5;
            double result = trapezoidalMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Trapezoid1245_ShouldBe0RightEdge()
        {
            IMembershipFunction trapezoidalMembershipFunction = new TrapezoidalMembershipFunction(1, 2, 4, 5);
            double expected = 0;
            double valueToCount = 5;
            double result = trapezoidalMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Trapezoid1245_ShouldBe0_5Left()
        {
            IMembershipFunction trapezoidalMembershipFunction = new TrapezoidalMembershipFunction(1, 2, 4, 5);
            double expected = 0.5;
            double valueToCount = 1.5;
            double result = trapezoidalMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Trapezoid1245_ShouldBe0_5Right()
        {
            IMembershipFunction trapezoidalMembershipFunction = new TrapezoidalMembershipFunction(1, 2, 4, 5);
            double expected = 0.5;
            double valueToCount = 4.5;
            double result = trapezoidalMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Trapezoid1245_ShouldBe1_2()
        {
            IMembershipFunction trapezoidalMembershipFunction = new TrapezoidalMembershipFunction(1, 2, 4, 5);
            double expected = 1;
            double valueToCount = 2;
            double result = trapezoidalMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Trapezoid1245_ShouldBe1_3()
        {
            IMembershipFunction trapezoidalMembershipFunction = new TrapezoidalMembershipFunction(1, 2, 4, 5);
            double expected = 1;
            double valueToCount = 3;
            double result = trapezoidalMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Trapezoid1245_ShouldBe1_4()
        {
            IMembershipFunction trapezoidalMembershipFunction = new TrapezoidalMembershipFunction(1, 2, 4, 5);
            double expected = 1;
            double valueToCount = 4;
            double result = trapezoidalMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Trapezoid0134_ShouldBe0LeftEdge()
        {
            IMembershipFunction trapezoidalMembershipFunction = new TrapezoidalMembershipFunction(0, 1, 3, 4);
            double expected = 0;
            double valueToCount = 0;
            double result = trapezoidalMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Trapezoid0134_ShouldBe0Left()
        {
            IMembershipFunction trapezoidalMembershipFunction = new TrapezoidalMembershipFunction(0, 1, 3, 4);
            double expected = 0;
            double valueToCount = -1;
            double result = trapezoidalMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Trapezoid0134_ShouldBe0_5Left()
        {
            IMembershipFunction trapezoidalMembershipFunction = new TrapezoidalMembershipFunction(0, 1, 3, 4);
            double expected = 0.5;
            double valueToCount = 0.5;
            double result = trapezoidalMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Trapezoid0033_ShouldBe1Left()
        {
            IMembershipFunction trapezoidalMembershipFunction = new TrapezoidalMembershipFunction(0, 0, 3, 3);
            double expected = 1;
            double valueToCount = 0;
            double result = trapezoidalMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Trapezoid0033_ShouldBe1Right()
        {
            IMembershipFunction trapezoidalMembershipFunction = new TrapezoidalMembershipFunction(0, 0, 3, 3);
            double expected = 1;
            double valueToCount = 3;
            double result = trapezoidalMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Trapezoid0033_ShouldBe1Middle()
        {
            IMembershipFunction trapezoidalMembershipFunction = new TrapezoidalMembershipFunction(0, 0, 3, 3);
            double expected = 1;
            double valueToCount = 2;
            double result = trapezoidalMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }

    }
}
