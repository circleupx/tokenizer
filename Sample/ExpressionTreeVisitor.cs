using System;
using System.Linq.Expressions;

namespace Tokenizer
{
    public class ExpressionTreeVisitor : IExpressionTreeVisitor
    {
        public IExpression LeftChildNode { get; set; }
        public IExpression RightChildNode { get ; set ; }
        public IExpression ParentNode { get; set; }

        private Expression _leftChildExpressionValue;
        private Expression _rightChildExpressionValue;

        public void VisitLeafNode(Number number)
        {
            if (_leftChildExpressionValue == null)
            {
                LeftChildNode = number;
                _leftChildExpressionValue = Expression.Constant(number.Value);
            }
            else
            {
                RightChildNode = number;
                _rightChildExpressionValue = Expression.Constant(number.Value);
            }
        }

        public void VisitParentNode(Multiplication multiplication)
        {
            if (ParentNode == null)
            {
                ParentNode = multiplication;
                return;
            }

            var expression = Expression.Multiply(_leftChildExpressionValue, _rightChildExpressionValue);
            ParentNode = null;
            _leftChildExpressionValue = expression;
        }

        public void VisitParentNode(Addition addition)
        {
            if (ParentNode == null)
            {
                ParentNode = addition;
                return;
            }

            var expression = Expression.Add(_leftChildExpressionValue, _rightChildExpressionValue);
            ParentNode = null;
            _leftChildExpressionValue = expression;
        }

        public Expression<Func<double>> GetCompletedExpression()
        {
            return Expression.Lambda<Func<double>>(_leftChildExpressionValue);
        }
    }
}
