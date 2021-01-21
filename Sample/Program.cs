using System;
using System.Collections.Generic;

namespace Tokenizer
{
    class Program
    {
        static void Main(string[] args)
        {
            var testExpression = "2*3+8";
            var precedenceBasedRegexTokenizer = new PrecedenceBasedRegexTokenizer();
            var tokens = precedenceBasedRegexTokenizer.Tokenize(testExpression);
            var listOfExpressions = new List<IExpression>();
            foreach (var token in tokens)
            {
                Console.WriteLine($"Token type {token.TokenType} has value {token.Value}");
                switch (token.TokenType)
                {
                    case TokenType.Addition:
                        var sumNode = new Addition();
                        listOfExpressions.Add(sumNode);
                        break;
                    case TokenType.NumberLiteral:
                        var numberNode = new Number(Convert.ToDouble(token.Value));
                        listOfExpressions.Add(numberNode);
                        break;
                    case TokenType.Multiplication:
                        var multiplicationNode = new Multiplication();
                        listOfExpressions.Add(multiplicationNode);
                        break;
                    case TokenType.SequenceTerminator:
                        break;
                    default:
                        break;
                }
            }

            var visitor = new ExpressionTreeVisitor();
            foreach (var item in listOfExpressions)
            {
                item.Accept(visitor);
            }

            var completedExpression = visitor.GetCompletedExpression();
            var result =  completedExpression.Compile();
            Console.WriteLine($"Expression {testExpression} is equal to {result()}");
        }
    }
}