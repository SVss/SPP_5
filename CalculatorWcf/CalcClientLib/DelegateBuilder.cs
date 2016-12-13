using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CalcClientLib
{
    public abstract class DelegateBuilder
    {
        // Public

        public Delegate GetDelegate(List<ExpressionItem> exprList)
        {
            if (exprList.Count == 0)
                return Expression.Lambda(Expression.Return(Expression.Label(), Expression.Constant(0.0))).Compile();

            Stack<Expression> helpStack = new Stack<Expression>();

            foreach (ExpressionItem itm in exprList)
            {
                if (itm is Operand)
                {
                    helpStack.Push(Expression.Constant(
                        ((Operand) itm).Value));
                }
                else if (itm is Operation)
                {
                    Expression tmp;
                    if (itm.IsUnary)
                    {
                        tmp = helpStack.Pop();
                        tmp = GetUnaryExpressionForOperator((Operation)itm, tmp);
                    }
                    else
                    {
                        Expression right = helpStack.Pop();
                        Expression left = helpStack.Pop();

                        tmp = GetBinaryExpressionForOperator((Operation) itm, left, right);
                    }
                    helpStack.Push(tmp);
                }
            }

            return Expression.Lambda(helpStack.Pop()).Compile();
        }

        // Internal

        protected abstract Expression GetBinaryExpressionForOperator(Operation operation, Expression leftOperand,
            Expression rightOperand);

        protected abstract Expression GetUnaryExpressionForOperator(Operation operation, Expression operand);

    }
}
