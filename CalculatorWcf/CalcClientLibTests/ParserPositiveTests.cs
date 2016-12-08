using System;
using System.Collections.Generic;
using System.Text;
using CalcClientLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CalcClientLibTests
{
    [TestClass]
    public class ParserPositiveTests
    {
        [TestMethod]
        public void ParserSimpleTest()
        {
            string expectedResult = "84 15 + 4 + 4 3 * 9 * - 24 + 4 + 54 3 / - 5 - 7 - 47 +";

            Parser parser = new Parser("84+15+4-4*3*9+24+4-54/3-5-7+47");
            var result = parser.GetPostfixNotation();

            string actualResult = Parser.ExpressionItemsListToString(result);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ParserDoubleTest()
        {
            string expectedResult = "25 3,3 / 1,34 2,56 * + 1,49 + 2,36 1,48 / +";

            Parser parser = new Parser("25/3,3+1,34*2,56+1,49+2,36/1,48");
            var result = parser.GetPostfixNotation();
            
            string actualResult = Parser.ExpressionItemsListToString(result);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ParserBracketsTest()
        {
            string expectedResult = "5 3 - + - 3 * 8 2 - 2 / - 2 /";

            Parser parser = new Parser("( -(5 + (-3) )*3 - (8-2) / 2 ) / 2");
            var result = parser.GetPostfixNotation();

            string actualResult = Parser.ExpressionItemsListToString(result);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ParserBracketsDoubleTest()
        {
            string expectedResult = "25 3 / 1,34 2,56 1,49 + * + 2,36 1,48 / +";

            Parser parser = new Parser("25/3+1,34*(2,56+1,49)+2,36/1,48");
            var result = parser.GetPostfixNotation();

            string actualResult = Parser.ExpressionItemsListToString(result);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ParserNegation1Test()
        {
            string expectedResult = "84 15 + 4 + 4 - 3 * 9 * -";

            Parser parser = new Parser("84+15+4-(-4)*3*9");
            var result = parser.GetPostfixNotation();

            string actualResult = Parser.ExpressionItemsListToString(result).Replace(',', '.');

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ParserNegation2Test()
        {
            string expectedResult = "2 - - 3 * 2 + - 2 /";

            Parser parser = new Parser("-(-(-2)*3+2)/2");
            var result = parser.GetPostfixNotation();

            string actualResult = Parser.ExpressionItemsListToString(result).Replace(',', '.');

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ParserPowerTest()
        {
            string expectedResult = "3 2 3 4 ^ ^ * 1 +";

            Parser parser = new Parser("3*2^3^4 + 1");
            var result = parser.GetPostfixNotation();

            string actualResult = Parser.ExpressionItemsListToString(result).Replace(',', '.');

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
