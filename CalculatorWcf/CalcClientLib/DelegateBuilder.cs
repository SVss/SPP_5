using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CalcClientLib
{
    public abstract class DelegateBuilder
    {
        // Public

        public virtual Delegate GetDelegate(List<ExpressionItem> exprList)
        {
            if (exprList.Count == 0)
                return Expression.Lambda(Expression.Return(Expression.Label(), Expression.Constant(0.0))).Compile();

            Stack<Expression> helpStack = new Stack<Expression>();

            foreach (ExpressionItem itm in exprList)
            {
                if (itm is Operand)
                {
                    helpStack.Push(Expression.Constant(
                        ((Operand)itm).Value));
                }
                else if (itm is Operation)
                {
                    Expression right = helpStack.Pop();
                    Expression left = helpStack.Pop();

                    Expression tmp = GetExpressionForOperator((Operation)itm, left, right);
                    helpStack.Push(tmp);
                }
            }

            return Expression.Lambda(helpStack.Pop()).Compile();
        }

        // Internal

        protected virtual Expression GetExpressionForOperator(Operation operation, Expression leftOperand, Expression rightOperand)
        {
            throw new NotImplementedException();
        }

    }
}
