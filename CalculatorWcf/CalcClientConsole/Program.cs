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
            Console.WriteLine("Enter your expression:");
            string input = Console.ReadLine();

            Parser parser = new Parser(input);
            List<ExpressionItem> expr = parser.GetPostfixNotation();

            // Usual Expressions

            var defaultDelegateBuilder = new DefaultDelegateBuilder();
            Delegate del = defaultDelegateBuilder.GetDelegate(expr);

            object result = del.DynamicInvoke();
            if (result is Double)
            {
                Console.WriteLine($"Local result: {result}");
            }

            // WCF = Expression.Call(...)

            var wcfDelegateBuilder = new WcfDelegateBuilder(new CalcServiceClient());
            del = wcfDelegateBuilder.GetDelegate(expr);

            result = del.DynamicInvoke();
            if (result is Double)
            {
                Console.WriteLine($"WCF result: {result}");
            }

            Console.ReadKey();
        }
    }
}
