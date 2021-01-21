namespace Tokenizer
{
    public class Multiplication : IExpression
    {
        public Multiplication()
        {

        }
        public void Accept(IExpressionTreeVisitor visitor)
        {
            visitor.VisitParentNode(this);
        }
    }
}
