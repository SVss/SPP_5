using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalcClientLib;

namespace CalcClientLibTests
{
    [TestClass]
    public class ParserNegativeBracketsTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidBracketsException))]
        public void InvalidBrackets1Test()
        {
            Parser parser = new Parser("(5+3)*3-(8-2)/2)/2");
            var result = parser.GetPostfixNotation();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidBracketsException))]
        public void InvalidBrackets2Test()
        {
            Parser parser = new Parser("((5+3)*3-(8-2)/2)/2)");
            var result = parser.GetPostfixNotation();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidBracketsException))]
        public void InvalidBrackets3Test()
        {
            Parser parser = new Parser(")((5+3)*3-(8-2)/2)/2)");
            var result = parser.GetPostfixNotation();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidBracketsException))]
        public void InvalidBracketsWithExpression1Test()
        {
            Parser parser = new Parser(")lolkek)"); // brackets first
            var result = parser.GetPostfixNotation();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidExprException))]
        public void InvalidBracketsWithExpression2Test()
        {
            Parser parser = new Parser("(l)olkek)"); // expression first
            var result = parser.GetPostfixNotation();
        }
    }
}
