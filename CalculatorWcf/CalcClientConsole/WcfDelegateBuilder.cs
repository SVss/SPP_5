using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CalcClientConsole.CalcServiceRef;
using CalcClientLib;

namespace CalcClientConsole
{
    class WcfDelegateBuilder: DelegateBuilder
    {
        private readonly CalcServiceClient _client;

        private readonly Dictionary<Operation, string> _operatorMapper = new Dictionary<Operation, string>()
        {
            { Operation.Add, "Add" },
            { Operation.Substract, "Substract" },
            { Operation.Multiply, "Multiply" },
            { Operation.Divide, "Divide" }
        };

        // Public

        public WcfDelegateBuilder(CalcServiceClient client)
        {
            this._client = client;
        }

        // Internal

        protected override Expression GetExpressionForOperator(Operation operation, Expression leftOperand, Expression rightOperand)
        {
            string name = _operatorMapper[operation];
            var method = _client.GetType().GetMethod(name);
            return Expression.Call(Expression.Constant(_client), method, leftOperand, rightOperand);
        }
    }
}
