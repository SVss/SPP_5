using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace CalcClientLib
{
    public class Parser
    {
        private readonly Stack<ExpressionItem> _evalStack = new Stack<ExpressionItem>();
        private readonly string _expression;
        int _validationCounter = 0;
        private List<ExpressionItem> _result = null;

        // Public

        public Parser(string expression)
        {
            _expression = expression;
        }

        /// <summary>
        /// Parse string expression to List of <code>ExpressionItems</code> in postfix notation.
        /// </summary>
        /// <returns>List of <code>ExpressionItems</code> in postfix notation</returns>
        /// <exception cref="InvalidOperationException">Invalid expression</exception>
        public List<ExpressionItem> GetPostfixNotation()
        {
            _result = new List<ExpressionItem>();

            using (var reader = new StringReader(_expression))
            {
                int peek;
                ExpressionItem previous = null;
                while ((peek = reader.Peek()) > -1)
                {
                    char next = (char) peek;

                    if (char.IsDigit(next))
                    {
                        if (previous is Operand)
                            throw new InvalidExprException("Unexpexted token.");

                        previous = ReadOperand(reader);
                        AddItem(previous);
                    }
                    else if (Operation.IsOperator(next))
                    {
                        var nextOp = ReadOperator(reader, previous);
                        while ( (_evalStack.Count > 0) && (_evalStack.Peek().StackPriority > nextOp.Priority))
                        {
                            AddItem(_evalStack.Pop());
                        }
                        _evalStack.Push(nextOp);
                        previous = nextOp;
                    }
                    else if (Divisor.IsDivisor(next))
                    {
                        var bracket = ReadDivisor(reader);
                        previous = bracket;

                        if (bracket == Divisor.OpenBracket)
                        {
                            _evalStack.Push(bracket);
                        }
                        else if (bracket == Divisor.CloseBracket)
                        {
                            try
                            {
                                while (_evalStack.Peek() != Divisor.OpenBracket)
                                {
                                    AddItem(_evalStack.Pop());
                                }
                                _evalStack.Pop();
                            }
                            catch (InvalidOperationException)
                            {
                                throw new InvalidBracketsException();
                            }
                        }
                    }
                    else if (char.IsWhiteSpace(next))
                    {
                        reader.Read();
                    }
                    else
                    {
                        throw new InvalidExprException("Unknown symbols detected.");
                    }
                }

                while (_evalStack.Count > 0)
                {
                    var tmp = _evalStack.Pop();
                    if (!(tmp is Divisor))
                        AddItem(tmp);
                    else
                        throw new InvalidBracketsException();
                }

                if (!IsValidExpression(_result))
                {
                    throw new InvalidExprException();
                }

                return _result;
            }
        }

        public static string ExpressionItemsListToString(List<ExpressionItem> exprList)
        {
            var builder = new StringBuilder();
            foreach (var itm in exprList)
            {
                builder.Append(string.Format("{0} ", itm.ToString()));
            }

            return builder.ToString().TrimEnd();
        }

        // Internal

        private void AddItem(ExpressionItem itm)
        {
            if (itm is Operand) ++_validationCounter;
            else if (itm is Operation)
            {
                if (itm.isUnary)
                {
                    --_validationCounter;
                    if (_validationCounter < 0)
                        throw new InvalidExprException();
                    ++_validationCounter;
                }
                else
                {
                    _validationCounter -= 2;
                    if (_validationCounter < 0)
                        throw new InvalidExprException();
                    ++_validationCounter;
                }
            }

            _result.Add(itm);
        }

        private bool IsValidExpression(List<ExpressionItem> expr)
        {
            return (_validationCounter == 1);
        }

        private static Operand ReadOperand(StringReader reader)
        {
            var valueStr = new StringBuilder();

            int peek = reader.Peek();
            bool isOk = (peek > -1);
            while (isOk)
            {
                char next = (char) peek;
                if (char.IsDigit(next) || next == CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator.ToCharArray()[0])
                {
                    valueStr.Append(next);
                    reader.Read();

                    peek = reader.Peek();
                    isOk = (peek > -1);
                }
                else
                {
                    isOk = false;
                }
            }
            try
            {
                double value = double.Parse(valueStr.ToString());
                return new Operand(value);
            }
            catch (FormatException)
            {
                throw new InvalidExprException();
            }
        }

        private static Operation ReadOperator(StringReader reader, ExpressionItem prevItem)
        {
            char sign = (char) reader.Read();
            var result = Operation.BySign[sign];
            if (result == Operation.Substract)
            {
                if (prevItem is Divisor || prevItem == null)    // to use negation after operator brackets are necessary
                {
                    result = Operation.Negation;
                }
            }
            return result;
        }

        private static Divisor ReadDivisor(StringReader reader)
        {
            char sign = (char)reader.Read();
            return Divisor.BySign[sign];
        }
    }

}
