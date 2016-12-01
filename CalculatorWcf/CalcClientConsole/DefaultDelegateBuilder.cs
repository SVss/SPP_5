using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CalcClientLib;

namespace CalcClientConsole
{
    class DefaultDelegateBuilder: DelegateBuilder
    {
        private readonly Dictionary<Operation, Func<Expression, Expression, Expression>> _binaryOpsMapper = new Dictionary
            <Operation, Func<Expression, Expression, Expression>>()
            {
                { Operation.Add, Expression.Add },
                { Operation.Substract, Expression.Subtract },
                { Operation.Multiply, Expression.Multiply },
                { Operation.Divide, Expression.Divide },
                { Operation.Power, Expression.Power }
            };

        private readonly Dictionary<Operation, Func<Expression, Expression>> _unaryOpsMapper = new Dictionary
            <Operation, Func<Expression, Expression>>()
            {
                { Operation.Negation, Expression.Negate }
            };

        // Internal

        protected override Expression GetBinaryExpressionForOperator(Operation operation, Expression leftOperand, Expression rightOperand)
        {
            return _binaryOpsMapper[operation].Invoke(leftOperand, rightOperand);
        }

        protected override Expression GetUnaryExpressionForOperator(Operation operation, Expression operand)
        {
            return _unaryOpsMapper[operation].Invoke(operand);
        }
    }
}
