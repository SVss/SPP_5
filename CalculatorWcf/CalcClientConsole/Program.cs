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
            PrintHelp();

            string input = ReadInput("Enter expression:");
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
                input = ReadInput("Enter expression:");
            }
        }

        private static readonly Dictionary<string, Action> PromptDict = new Dictionary<string, Action>()
        {
            { "help", PrintHelp }
        };

        private static void PrintHelp()
        {
            Console.Write("\n[Expression Caculator help]\n");

            Console.Write("Allowed operations:\n");
            Console.Write("\t+\tsum\n\t-\tnegate\n\t*\tmul\n\t/\tdiv\n\t^\tpow\n\t#\tsqrt\n");

            Console.Write("\nCommands:\n\tq\tquit\n\n");

            Console.Write("Non-numeric symbols (except allowed operations) are not allowed.\n\n");
        }

        private static string ReadInput(string prompt = ">")
        {
            string input = GetPromptInput(prompt);
            while (input != null && PromptDict.ContainsKey(input))
            {
                PromptDict[input].Invoke();
                input = GetPromptInput(prompt);
            }

            return input;
        }

        private static string GetPromptInput(string prompt)
        {
            Console.Write($"{prompt} ");
            return Console.ReadLine()?.Trim();
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
            public static string ForbiddenOperation => "Forbidden operation. Incorrect expression. Type \"help\" for moer inforation."; // zero divide
            public static string IncorrectBrackets => "Incorrect brackets.";
            public static string IncorrectExpression => "Incorrect expression. Type \"help\" for more inforation.";
        }
    }
}
