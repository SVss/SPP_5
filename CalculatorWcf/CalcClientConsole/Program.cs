using System;
using System.Collections.Generic;
using CalcClientConsole.CalcServiceRef;
using CalcClientLib;

namespace CalcClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = PromptReadResult();

            while (input != "q")
            {
                try
                {
                    Parser parser = new Parser(input);
                    List<ExpressionItem> expr = parser.GetPostfixNotation();

                    // Usual Expressions

                    var defaultDelegateBuilder = new DefaultDelegateBuilder();
                    Delegate del = defaultDelegateBuilder.GetDelegate(expr);

                    object result = del.DynamicInvoke();
                    WriteResult(result, "Local result");

                    // WCF = Expression.Call(...)

                    var wcfDelegateBuilder = new WcfDelegateBuilder(new CalcServiceClient());
                    del = wcfDelegateBuilder.GetDelegate(expr);

                    result = del.DynamicInvoke();
                    WriteResult(result, "WCF result");
                }
                catch (InvalidExprException)
                {
                    Console.WriteLine(Messages.IncorrectExpression);
                }
                catch (InvalidBracketsException)
                {
                    Console.WriteLine(Messages.IncorrectBrackets);
                }

                Console.WriteLine();
                input = PromptReadResult();
            }
        }

        private static string PromptReadResult(string prompt = "Enter expression:")
        {
            Console.WriteLine("Enter your expression (or 'q' to exit):");
            return Console.ReadLine();
        }

        private static void WriteResult(object result, string msg = "Result")
        {
            if (result is Double)
            {
                double val = (double) result;
                if (double.IsNaN(val) || double.IsInfinity(val))
                {
                    Console.WriteLine($"{msg}: {Messages.ForbiddenOperation}");
                }
                else
                {
                    Console.WriteLine($"{msg}: {result}");
                }
            }
        }

        private static class Messages
        {
            public static string ForbiddenOperation => "Forbidden operation"; // zero divide
            public static string IncorrectBrackets => "Incorrect brackets";
            public static string IncorrectExpression => "Incorrect expression";
        }
    }
}
