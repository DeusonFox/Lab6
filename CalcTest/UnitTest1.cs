using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalcLibrary;
namespace CalcTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CalculateTestMethod()
        {
            String[] a = Calc.GetOperands("23+4,5");
            Assert.AreEqual("23", a[0]);
            Assert.AreEqual("4,5", a[1]);
        }
        [TestMethod]
        public void CalculateTestMethod2()
        {
            string result = Calc.GetOperation("23+4,5");
            Assert.AreEqual("+", result);
            Assert.AreEqual("+", Calc.GetOperation("23+4,5").ToString());
        }
        [TestMethod]
        public void CalculateTestMethod3()
        {
            string result = Calc.GetOperation("23-4,5");
            Assert.AreEqual("-", result);
            Assert.AreEqual("-", Calc.GetOperation("23-4,5").ToString());
        }
        [TestMethod]
        public void CalculateTestMethod4()
        {
            string result = Calc.GetOperation("23*4,5");
            Assert.AreEqual("*", result);
            Assert.AreEqual("*", Calc.GetOperation("23*4,5").ToString());
        }
        [TestMethod]
        public void CalculateTestMethod5()
        {
            string result = Calc.GetOperation("23/4,5");
            Assert.AreEqual("/", result);
            Assert.AreEqual("/", Calc.GetOperation("23/4,5").ToString());
        }
        [TestMethod]
        public void ResultTestMethod()
        {
            string result = Calc.DoOperation("23+4,5");
            Assert.AreEqual("27,5", result);
            Assert.AreEqual("27,5", Calc.DoubleOperation["+"](23, 4.5).ToString());
        }
        [TestMethod]
        public void ResultTestMethod1()
        {
            string result = Calc.DoOperation("23-4,5");
            Assert.AreEqual("18,5", result);
            Assert.AreEqual("18,5", Calc.DoubleOperation["-"](23, 4.5).ToString());
        }
        [TestMethod]
        public void ResultTestMethod2()
        {
            string result = Calc.DoOperation("25*4");
            Assert.AreEqual("100", result);
            Assert.AreEqual("100", Calc.DoubleOperation["*"](25, 4).ToString());
        }
        [TestMethod]
        public void ResultTestMethod3()
        {
            string result = Calc.DoOperation("31,5/4,5");
            Assert.AreEqual("7", result);
            Assert.AreEqual("7", Calc.DoubleOperation["/"](31.5, 4.5).ToString());
        }
    }
}
