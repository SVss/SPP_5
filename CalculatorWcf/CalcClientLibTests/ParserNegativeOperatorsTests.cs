using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalcClientLib;

namespace CalcClientLibTests
{
    [TestClass]
    public class ParserNegativeTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidExprException))]
        public void InvalidOperators1Test()
        {
            Parser parser = new Parser("*84+15+4-4*3*9");
            var result = parser.GetPostfixNotation();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidExprException))]
        public void InvalidOperators2Test()
        {
            Parser parser = new Parser("84+15+4-4*3*9+");
            var result = parser.GetPostfixNotation();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidExprException))]
        public void InvalidOperators3Test()
        {
            Parser parser = new Parser("84+15+4--4*3*9");
            var result = parser.GetPostfixNotation();
        }
    }
}
