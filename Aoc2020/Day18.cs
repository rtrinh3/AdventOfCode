using System.Collections.Frozen;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Aoc2020
{
    // https://adventofcode.com/2020/day/18
    // --- Day 18: Operation Order ---
    public class Day18 : IAocDay
    {
        private readonly string[][] expressions;

        public Day18(string input)
        {
            string[] lines = input.TrimEnd().Split('\n');
            expressions = lines.Select(line =>
            {
                var matches = Regex.Matches(line, @"(\d+|\+|\*|\(|\))");
                var tokens = matches.Cast<Match>().Select(m => m.Value);
                return tokens.ToArray();
            }).ToArray();
        }

        public string Part1()
        {
            Dictionary<string, int> partOnePrecedence = new Dictionary<string, int>()
            {
                ["+"] = 1,
                ["*"] = 1,
            };
            var answer = expressions.Sum(expr => EvaluateRpn(ShuntingYardAlgorithm(expr, partOnePrecedence)));
            return answer.ToString();
        }

        public string Part2()
        {
            Dictionary<string, int> partTwoPrecedence = new Dictionary<string, int>()
            {
                ["+"] = 2,
                ["*"] = 1,
            };
            var answer = expressions.Sum(expr => EvaluateRpn(ShuntingYardAlgorithm(expr, partTwoPrecedence)));
            return answer.ToString();
        }

        // https://en.wikipedia.org/wiki/Shunting_yard_algorithm
        private static IEnumerable<string> ShuntingYardAlgorithm(IEnumerable<string> infixExpression, IReadOnlyDictionary<string, int> precedence)
        {
            Stack<string> operatorStack = new();
            foreach (string token in infixExpression)
            {
                if (token.All(char.IsAsciiDigit))
                {
                    yield return token;
                }
                else if (precedence.Keys.Contains(token))
                {
                    var tokenPrecedence = precedence[token];
                    while (operatorStack.Count > 0 && operatorStack.Peek() != "(" && precedence[operatorStack.Peek()] >= tokenPrecedence)
                    {
                        yield return operatorStack.Pop();
                    }
                    operatorStack.Push(token);
                }
                else if (token == "(")
                {
                    operatorStack.Push(token);
                }
                else if (token == ")")
                {
                    Debug.Assert(operatorStack.Count >= 1, "Mismatched parentheses");
                    while (operatorStack.Peek() != "(")
                    {
                        yield return operatorStack.Pop();
                    }
                    Debug.Assert(operatorStack.Count >= 1 && operatorStack.Peek() == "(", "Mismatched parentheses");
                    operatorStack.Pop();
                }
            }
            while (operatorStack.Count > 0)
            {
                yield return operatorStack.Pop();
            }
        }

        private static long EvaluateRpn(IEnumerable<string> rpnExpression)
        {
            Stack<long> stack = new();
            foreach (string token in rpnExpression)
            {
                if (token.All(char.IsAsciiDigit))
                {
                    stack.Push(long.Parse(token));
                }
                else if (token == "+")
                {
                    var operandA = stack.Pop();
                    var operandB = stack.Pop();
                    var newValue = operandA + operandB;
                    stack.Push(newValue);
                }
                else if (token == "*")
                {
                    var operandA = stack.Pop();
                    var operandB = stack.Pop();
                    var newValue = operandA * operandB;
                    stack.Push(newValue);
                }
            }
            Debug.Assert(stack.Count >= 1);
            return stack.Peek();
        }
    }
}
