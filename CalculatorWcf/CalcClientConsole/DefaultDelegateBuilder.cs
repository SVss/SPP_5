using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CalcClientLib;

namespace CalcClientConsole
{
    class DefaultDelegateBuilder: DelegateBuilder
    {
        private readonly Dictionary<Operation, Func<Expression, Expression, Expression>> _operatorMapper = new Dictionary
            <Operation, Func<Expression, Expression, Expression>>()
            {
                { Operation.Add, Expression.Add },
                { Operation.Substract, Expression.Subtract },
                { Operation.Multiply, Expression.Multiply },
                { Operation.Divide, Expression.Divide }
            };

        // Internal

        protected override Expression GetExpressionForOperator(Operation operation, Expression leftOperand, Expression rightOperand)
        {
            return _operatorMapper[operation].Invoke(leftOperand, rightOperand);
        }
    }
}
