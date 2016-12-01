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
            Stack<ExpressionItem> unaryStack = new Stack<ExpressionItem>();

            for (int i = 0; i < exprList.Count; i++)
            {
                ExpressionItem itm = exprList[i];
                if (itm is Operand)
                {
                    helpStack.Push(Expression.Constant(
                        ((Operand) itm).Value));
                }
                else if (itm is Operation)
                {
                    Expression tmp;
                    if (itm.isUnary)
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

        protected virtual Expression GetBinaryExpressionForOperator(Operation operation, Expression leftOperand, Expression rightOperand)
        {
            throw new NotImplementedException();
        }

        protected virtual Expression GetUnaryExpressionForOperator(Operation operation, Expression operand)
        {
            throw new NotImplementedException();
        }

    }
}
