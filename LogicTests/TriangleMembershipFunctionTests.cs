using System;
using Logic.MembershipFunctions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicTests
{
    [TestClass]
    public class TriangleMembershipFuctionsTests
    {
        [TestMethod]
        public void Triangle08_ShouldBeZeroLeft()
        {
            IMembershipFunction triangleMembershipFunction = new TriangleMembershipFunction(0, 8, 4);
            double expected = 0;
            double valueToCount = 0;
            double result = triangleMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Triangle08_ShouldBeOne()
        {
            IMembershipFunction triangleMembershipFunction = new TriangleMembershipFunction(0, 8, 4);
            double expected = 1;
            double valueToCount = 4;
            double result = triangleMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Triangle08_ShouldBeZeroRight()
        {
            IMembershipFunction triangleMembershipFunction = new TriangleMembershipFunction(0, 8, 4);
            double expected = 0;
            double valueToCount = 9;
            double result = triangleMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Triangle08_ShouldBeDifferentThan0Or1()
        {
            IMembershipFunction triangleMembershipFunction = new TriangleMembershipFunction(0, 8, 4);
            double expected = 0.5;
            double valueToCount = 2;
            double result = triangleMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Triangle36_ShouldBe0Left()
        {
            IMembershipFunction triangleMembershipFunction = new TriangleMembershipFunction(3, 6, 4);
            double expected = 0;
            double valueToCount = 2;
            double result = triangleMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Triangle36_ShouldBe1()
        {
            IMembershipFunction triangleMembershipFunction = new TriangleMembershipFunction(3, 6, 4);
            double expected = 1;
            double valueToCount = 4;
            double result = triangleMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Triangle36_ShouldBe0Right()
        {
            IMembershipFunction triangleMembershipFunction = new TriangleMembershipFunction(3, 6, 4);
            double expected = 0;
            double valueToCount = 100;
            double result = triangleMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Triangle36_ShouldBe0_5()
        {
            IMembershipFunction triangleMembershipFunction = new TriangleMembershipFunction(3, 6, 4);
            double expected = 0.5;
            double valueToCount = 5;
            double result = triangleMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Triangle36_ShouldBe0LeftOnEdge()
        {
            IMembershipFunction triangleMembershipFunction = new TriangleMembershipFunction(3, 6, 4);
            double expected = 0;
            double valueToCount = 3;
            double result = triangleMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Triangle44_ShouldBe1()
        {
            IMembershipFunction triangleMembershipFunction = new TriangleMembershipFunction(4, 4, 4);
            double expected = 1;
            double valueToCount = 4;
            double result = triangleMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Triangle44_ShouldBe0()
        {
            IMembershipFunction triangleMembershipFunction = new TriangleMembershipFunction(4, 4, 4);
            double expected = 0;
            double valueToCount = 3;
            double result = triangleMembershipFunction.GetMembershipFunctionValue(valueToCount);
            Assert.AreEqual(expected, result);
        }
    }
}
