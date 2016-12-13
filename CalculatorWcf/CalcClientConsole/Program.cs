using System;
using System.Collections.Generic;
using System.Reflection;
using CalcClientConsole.CalcServiceRef;
using CalcClientLib;

namespace CalcClientConsole
{
    static class Program
    {
        private static readonly Dictionary<string, Action> PromptDict = new Dictionary<string, Action>()
        {
            { "h", PrintHelp }
        };

        // Public

        public static void Main()
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

                    Console.Write("Local result: ");
                    object result = del.DynamicInvoke();
                    WriteResult(result);


                    // WCF = Expression.Call(...)

                    var wcfDelegateBuilder = new WcfDelegateBuilder(new CalcServiceClient());
                    del = wcfDelegateBuilder.GetDelegate(expr);

                    Console.Write("WCF result: ");
                    try
                    {
                        result = del.DynamicInvoke();
                        WriteResult(result);
                    }
                    catch(TargetInvocationException)
                    {
                        Console.WriteLine("Service not available");
                    }
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

        // Internal

        private static void PrintHelp()
        {
            Console.Write("\n[Expression Caculator help]\n");

            Console.Write("Allowed operations:\n");
            Console.Write("\t+\tsum\n\t-\tnegate\n\t*\tmul\n\t/\tdiv\n\t^\tpow\n\t#\tsqrt\n");

            Console.WriteLine("\nCommands:");
            Console.Write("\tq\tquit\n\th\tshow help\n\n");

            Console.Write("Non-numeric symbols (except allowed operations) are not allowed.\n\n");
            Console.Write("P.S.: Divide by zero and root of negative number are incorrect.\n");
            Console.Write("Also overflow causes Forbidden operation.\n\n");
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

        private static void WriteResult(object result)
        {
            if (result is Double)
            {
                double val = (double) result;
                if (!double.IsNaN(val) && !double.IsInfinity(val))
                {
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine(Messages.ForbiddenOperation);
                }
            }
        }

        private static class Messages
        {
            public static string ForbiddenOperation => "Forbidden operation."; // zero divide, root of negative number, overflow
            public static string IncorrectBrackets => "Incorrect brackets.";
            public static string IncorrectExpression => "Incorrect expression. Type \"h\" for more inforation.";
        }
    }
}
