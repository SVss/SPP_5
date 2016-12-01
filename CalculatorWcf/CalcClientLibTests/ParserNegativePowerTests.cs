using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalcClientLib;

namespace CalcClientLibTests
{
    [TestClass]
    public class ParserNegativePowerTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidExprException))]
        public void InvalidPower1Test()
        {
            Parser parser = new Parser("2 ^ ^ 2");
            var result = parser.GetPostfixNotation();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidExprException))]
        public void InvalidPower2Test()
        {
            Parser parser = new Parser("^ 2 + 1 ^ 3");
            var result = parser.GetPostfixNotation();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidExprException))]
        public void InvalidPower3Test()
        {
            Parser parser = new Parser("2 ^ + 3 ^ 2");
            var result = parser.GetPostfixNotation();
        }
    }
}
