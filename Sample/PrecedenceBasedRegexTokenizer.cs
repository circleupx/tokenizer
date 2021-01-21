using System.Collections.Generic;
using System.Linq;

namespace Tokenizer
{
    public class PrecedenceBasedRegexTokenizer
    {
        private List<TokenDefinition> _tokenDefinitions;

        public PrecedenceBasedRegexTokenizer()
        {
            _tokenDefinitions = new List<TokenDefinition>
            {
                new TokenDefinition(TokenType.Multiplication, @"[*]", 1),
                new TokenDefinition(TokenType.Addition, @"[+]", 1),
                new TokenDefinition(TokenType.NumberLiteral, "\\d+", 2)
            };
        }

        public IEnumerable<Token> Tokenize(string lqlText)
        {
            var tokenMatches = FindTokenMatches(lqlText);

            var groupedByIndex = tokenMatches.GroupBy(x => x.StartIndex)
                .OrderBy(x => x.Key)
                .ToList();

            TokenMatch lastMatch = null;
            for (int i = 0; i < groupedByIndex.Count; i++)
            {
                var bestMatch = groupedByIndex[i].OrderBy(x => x.Precedence).First();
                if (lastMatch != null && bestMatch.StartIndex < lastMatch.EndIndex)
                    continue;

                yield return new Token(bestMatch.TokenType, bestMatch.Value);

                lastMatch = bestMatch;
            }

            yield return new Token(TokenType.SequenceTerminator);
        }

        private List<TokenMatch> FindTokenMatches(string lqlText)
        {
            var tokenMatches = new List<TokenMatch>();

            foreach (var tokenDefinition in _tokenDefinitions)
                tokenMatches.AddRange(tokenDefinition.FindMatches(lqlText).ToList());

            return tokenMatches;
        }
    }
}
