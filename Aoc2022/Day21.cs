using System.Text.RegularExpressions;

namespace Aoc2022
{
    public class Day21(string input) : IAocDay
    {
        public string Part1()
        {
            Dictionary<string, Func<decimal>> monkeys = new();
            foreach (string line in AocCommon.Parsing.SplitLines(input))
            {
                int colonIndex = line.IndexOf(':');
                var prefix = line[0..colonIndex];
                if (int.TryParse(line[(colonIndex + 2)..], out int value))
                {
                    monkeys[prefix] = () => value;
                }
                else
                {
                    var match = Regex.Match(line[(colonIndex + 2)..], @"(\w+) (.) (\w+)");
                    string left = match.Groups[1].Value;
                    string op = match.Groups[2].Value;
                    string right = match.Groups[3].Value;
                    if (op == "+")
                    {
                        monkeys[prefix] = () => monkeys[left]() + monkeys[right]();
                    }
                    else if (op == "-")
                    {
                        monkeys[prefix] = () => monkeys[left]() - monkeys[right]();
                    }
                    else if (op == "*")
                    {
                        monkeys[prefix] = () => monkeys[left]() * monkeys[right]();
                    }
                    else if (op == "/")
                    {
                        monkeys[prefix] = () => monkeys[left]() / monkeys[right]();
                    }
                }
            }
            var root = monkeys["root"]();
            return root.ToString();
        }

        public string Part2()
        {
            Dictionary<string, Lazy<Expr>> monkeys = new();
            foreach (string line in AocCommon.Parsing.SplitLines(input))
            {
                int colonIndex = line.IndexOf(':');
                var prefix = line[0..colonIndex];
                if (decimal.TryParse(line[(colonIndex + 2)..], out decimal value))
                {
                    if (prefix == "humn")
                    {
                        monkeys[prefix] = new Lazy<Expr>(new Variable { Name = prefix });
                    }
                    else
                    {
                        monkeys[prefix] = new Lazy<Expr>(new Constant { Value = value });
                    }
                }
                else
                {
                    var match = Regex.Match(line[(colonIndex + 2)..], @"(\w+) (.) (\w+)");
                    string left = match.Groups[1].Value;
                    string op = match.Groups[2].Value;
                    string right = match.Groups[3].Value;
                    if (prefix == "root")
                    {
                        monkeys[prefix] = new Lazy<Expr>(() => new EquOp { Left = monkeys[left].Value, Right = monkeys[right].Value });
                    }
                    else if (op == "+")
                    {
                        monkeys[prefix] = new Lazy<Expr>(() => new AddOp { Left = monkeys[left].Value, Right = monkeys[right].Value });
                    }
                    else if (op == "-")
                    {
                        monkeys[prefix] = new Lazy<Expr>(() => new SubOp { Left = monkeys[left].Value, Right = monkeys[right].Value });
                    }
                    else if (op == "*")
                    {
                        monkeys[prefix] = new Lazy<Expr>(() => new MulOp { Left = monkeys[left].Value, Right = monkeys[right].Value });
                    }
                    else if (op == "/")
                    {
                        monkeys[prefix] = new Lazy<Expr>(() => new DivOp { Left = monkeys[left].Value, Right = monkeys[right].Value });
                    }
                }
            }
            var root = (EquOp)monkeys["root"].Value;
            root.Solve();
            var simplified = (EquOp)root.Simplify();
            var simplifiedValue = simplified.Right;
            return simplifiedValue.ToString();
        }
        interface Expr
        {
            Expr Simplify();
        }

        class Variable : Expr
        {
            public string Name { get; init; }
            public Expr Simplify() => this;
            public override string ToString() => Name;
        }

        class Constant : Expr
        {
            public decimal Value { get; init; }
            public Expr Simplify() => this;
            public override string ToString() => Value.ToString();
        }

        class AddOp : Expr
        {
            public Expr Left { get; set; }
            public Expr Right { get; set; }
            public Expr Simplify()
            {
                Left = Left.Simplify();
                Right = Right.Simplify();
                if (Left is Constant leftConstant && Right is Constant rightConstant)
                {
                    decimal value = leftConstant.Value + rightConstant.Value;
                    return new Constant { Value = value };
                }
                else
                {
                    return this;
                }
            }
            public override string ToString() => string.Format("({0} + {1})", Left, Right);
        }

        class SubOp : Expr
        {
            public Expr Left { get; set; }
            public Expr Right { get; set; }
            public Expr Simplify()
            {
                Left = Left.Simplify();
                Right = Right.Simplify();
                if (Left is Constant leftConstant && Right is Constant rightConstant)
                {
                    decimal value = leftConstant.Value - rightConstant.Value;
                    return new Constant { Value = value };
                }
                else
                {
                    return this;
                }
            }
            public override string ToString() => string.Format("({0} - {1})", Left, Right);
        }

        class MulOp : Expr
        {
            public Expr Left { get; set; }
            public Expr Right { get; set; }
            public Expr Simplify()
            {
                Left = Left.Simplify();
                Right = Right.Simplify();
                if (Left is Constant leftConstant && Right is Constant rightConstant)
                {
                    decimal value = leftConstant.Value * rightConstant.Value;
                    return new Constant { Value = value };
                }
                else
                {
                    return this;
                }
            }
            public override string ToString() => string.Format("({0} * {1})", Left, Right);
        }

        class DivOp : Expr
        {
            public Expr Left { get; set; }
            public Expr Right { get; set; }
            public Expr Simplify()
            {
                Left = Left.Simplify();
                Right = Right.Simplify();
                if (Left is Constant leftConstant && Right is Constant rightConstant)
                {
                    decimal value = leftConstant.Value / rightConstant.Value;
                    return new Constant { Value = value };
                }
                else
                {
                    return this;
                }
            }
            public override string ToString() => string.Format("({0} / {1})", Left, Right);
        }

        class EquOp : Expr
        {
            public Expr Left { get; set; }
            public Expr Right { get; set; }
            public Expr Simplify()
            {
                Left = Left.Simplify();
                Right = Right.Simplify();
                if (Left is Constant leftConstant && Right is Constant rightConstant)
                {
                    decimal value = leftConstant.Value == rightConstant.Value ? 1 : 0;
                    return new Constant { Value = value };
                }
                else
                {
                    return this;
                }
            }
            public override string ToString() => string.Format("{0} = {1}", Left, Right);
            public void Solve()
            {
                Simplify();
                if (Right is Constant)
                {
                    while (Left is not Variable)
                    {
                        Constant rightConstant = (Constant)Right;
                        if (Left is AddOp add)
                        {
                            if (add.Left is Constant addLeft)
                            {
                                Left = add.Right;
                                Right = new Constant { Value = rightConstant.Value - addLeft.Value };
                            }
                            else if (add.Right is Constant addRight)
                            {
                                Left = add.Left;
                                Right = new Constant { Value = rightConstant.Value - addRight.Value };
                            }
                            else
                            {
                                throw new NotImplementedException();
                            }
                        }
                        else if (Left is MulOp mul)
                        {
                            if (mul.Left is Constant mulLeft)
                            {
                                Left = mul.Right;
                                Right = new Constant { Value = rightConstant.Value / mulLeft.Value };
                            }
                            else if (mul.Right is Constant mulRight)
                            {
                                Left = mul.Left;
                                Right = new Constant { Value = rightConstant.Value / mulRight.Value };
                            }
                            else
                            {
                                throw new NotImplementedException();
                            }
                        }
                        else if (Left is SubOp sub)
                        {
                            if (sub.Left is Constant subLeft)
                            {
                                Left = sub.Right;
                                Right = new Constant { Value = subLeft.Value - rightConstant.Value };
                            }
                            else if (sub.Right is Constant subRight)
                            {
                                Left = sub.Left;
                                Right = new Constant { Value = rightConstant.Value + subRight.Value };
                            }
                            else
                            {
                                throw new NotImplementedException();
                            }
                        }
                        else if (Left is DivOp div)
                        {
                            if (div.Left is Constant divLeft)
                            {
                                Left = div.Right;
                                Right = new Constant { Value = divLeft.Value / rightConstant.Value };
                            }
                            else if (div.Right is Constant divRight)
                            {
                                Left = div.Left;
                                Right = new Constant { Value = rightConstant.Value * divRight.Value };
                            }
                            else
                            {
                                throw new NotImplementedException();
                            }
                        }
                        //this.ToString().Dump();
                    }
                }
                else if (Left is Constant)
                {
                    var temp = Right;
                    Right = Left;
                    Left = temp;
                    Solve();
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
