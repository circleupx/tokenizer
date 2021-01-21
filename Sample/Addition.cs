namespace Tokenizer
{
    public class Addition : IExpression
    {
        public Addition()
        {

        }
        public void Accept(IExpressionTreeVisitor visitor)
        {
            visitor.VisitParentNode(this);
        }
    }
}
