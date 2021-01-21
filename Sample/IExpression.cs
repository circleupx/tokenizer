namespace Tokenizer
{
    public interface IExpression
    {
        void Accept(IExpressionTreeVisitor visitor);
    }
}
