using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CalcClientLib
{
    public class Parser
    {
        private readonly Stack<ExpressionItem> _evalStack = new Stack<ExpressionItem>();
        private readonly string _expression;

        // Public
        
        public Parser(string expression)
        {
            string s = expression.Replace(" ", string.Empty).Replace('.', ',');    // <= to avoid FormatException;
            if (IsValidBrackets(s))
                this._expression = s;
            else 
                throw new InvalidBracketsException();
        }

        /// <summary>
        /// Parse string expression to List of <code>ExpressionItems</code> in postfix notation.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>List of <code>ExpressionItems</code> in postfix notation</returns>
        /// <exception cref="InvalidOperationException">Invalid expression</exception>
        public List<ExpressionItem> GetPostfixNotation()
        {
            List<ExpressionItem> result = new List<ExpressionItem>();

            using (var reader = new StringReader(_expression))
            {
                int peek;
                ExpressionItem previous = null;
                while ((peek = reader.Peek()) > -1)
                {
                    char next = (char) peek;

                    if (char.IsDigit(next))
                    {
                        previous = ReadOperand(reader);
                        result.Add(previous);
                    }
                    else if (Operation.IsOperator(next))
                    {
                        var nextOp = ReadOperator(reader, previous);
                        while ( (_evalStack.Count > 0) && (_evalStack.Peek().StackPriority > nextOp.Priority))
                        {
                            result.Add(_evalStack.Pop());
                        }
                        _evalStack.Push(nextOp);
                        previous = nextOp;
                    }
                    else if (Divisor.IsDivisor(next))
                    {
                        var bracket = Divisor.BySign[next];
                        reader.Read();
                        previous = bracket;

                        if (bracket == Divisor.OpenBracket)
                        {
                            _evalStack.Push(bracket);
                        }
                        else if (bracket == Divisor.CloseBracket)
                        {
                            // never throws InvalidOperationException if _evalStack is empty <= invalid brackets (checked on initialization)
                            while (_evalStack.Peek() != Divisor.OpenBracket)
                            {
                                result.Add(_evalStack.Pop());
                            }
                            _evalStack.Pop();
                        }
                    }
                    else
                    {
                        throw new InvalidExprException("Unknown symbols detected.");
                    }
                }

                while (_evalStack.Count > 0)    // brackets are valid
                {
                    result.Add(_evalStack.Pop());
                }

                if (!IsValidExpression(result))
                {
                    throw new InvalidExprException();
                }

                return result;
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

        private static bool IsValidExpression(List<ExpressionItem> expr)
        {
            int counter = 0;

            foreach (var itm in expr)
            {
                if (itm is Operand) ++counter;
                else if (itm is Operation)
                {
                    if (itm == Operation.Negation)
                    {
                        --counter;
                        if (counter < 0)
                            return false;
                        ++counter;
                    }
                    else
                    {
                        counter -= 2;
                        if (counter < 0)
                            return false;
                        ++counter;
                    }
                }
            }

            return (counter == 1);
        }

        private static bool IsValidBrackets(string s)
        {
            int br = 0;
            foreach (char c in s)
            {
                if (c == '(')
                    ++br;
                else if (c == ')')
                    --br;

                if (br < 0)
                    return false;
            }
            return (br == 0);
        }

        private static Operand ReadOperand(StringReader reader)
        {
            var valueStr = new StringBuilder();

            int peek = reader.Peek();
            bool isOk = (peek > -1);
            while (isOk)
            {
                char next = (char) peek;
                if (char.IsDigit(next) || next == ',')
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

            double value = double.Parse(valueStr.ToString());
            return new Operand(value);
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
