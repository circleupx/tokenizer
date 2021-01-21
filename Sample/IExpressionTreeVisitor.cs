using System.Linq.Expressions;

namespace Tokenizer
{
    public interface IExpressionTreeVisitor
    {
        public IExpression LeftChildNode { get; set; }
        public IExpression RightChildNode { get; set; }
        public IExpression ParentNode { get; set; }

        void VisitLeafNode(Number integer);
        void VisitParentNode(Multiplication addition);
        void VisitParentNode(Addition multiplication);
    }
}
