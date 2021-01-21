namespace Tokenizer
{
    public class Number : IExpression
    {
        public double Value { get; }

        public Number(double value)
        {
            Value = value;
        }

        public void Accept(IExpressionTreeVisitor visitor)
        {
            visitor.VisitLeafNode(this);
            if (visitor.ParentNode == null)
            {
                return;
            }

            visitor.ParentNode.Accept(visitor);
        }
    }
}
