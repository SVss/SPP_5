using System.Collections.Generic;
using System.Linq.Expressions;
using CalcClientConsole.CalcServiceRef;
using CalcClientLib;

namespace CalcClientConsole
{
    class WcfDelegateBuilder: DelegateBuilder
    {
        private readonly CalcServiceClient _client;

        private readonly Dictionary<Operation, string> _binaryOpsMapper = new Dictionary<Operation, string>()
        {
            { Operation.Add, "Add" },
            { Operation.Substract, "Substract" },
            { Operation.Multiply, "Multiply" },
            { Operation.Divide, "Divide" },
            { Operation.Power, "Power" },
        };

        private readonly Dictionary<Operation, string> _unaryOpsMapper = new Dictionary<Operation, string>()
        {
            { Operation.Negation, "Negate" },
            { Operation.Sqrt, "Sqrt" }
        };

        // Public

        public WcfDelegateBuilder(CalcServiceClient client)
        {
            _client = client;
        }

        // Internal

        protected override Expression GetBinaryExpressionForOperator(Operation operation, Expression leftOperand, Expression rightOperand)
        {
            string name = _binaryOpsMapper[operation];
            var method = _client.GetType().GetMethod(name);
            return Expression.Call(Expression.Constant(_client), method, leftOperand, rightOperand);
        }

        protected override Expression GetUnaryExpressionForOperator(Operation operation, Expression operand)
        {
            string name = _unaryOpsMapper[operation];
            var method = _client.GetType().GetMethod(name);
            return Expression.Call(Expression.Constant(_client), method, operand);
        }
    }
}
