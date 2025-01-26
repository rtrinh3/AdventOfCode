namespace Aoc2022
{
    public class Day25(string input) : IAocDay
    {
        public string Part1()
        {
            Snafu sum = Snafu.Zero;
            foreach (string line in AocCommon.Parsing.SplitLines(input))
            {
                var value = Snafu.Parse(line);
                sum += value;
            }
            return sum.ToString();
        }
        public string Part2()
        {
            return "Merry Christmas!";
        }

        record class Snafu(sbyte[] digits)
        {
            // digits is little-endian
            public override string ToString()
            {
                return string.Concat(
                    digits.Reverse().Select(d => d switch {
                        0 => '0',
                        1 => '1',
                        2 => '2',
                        -1 => '-',
                        -2 => '=',
                        _ => throw new FormatException()
                    })
                );
            }

            public decimal GetValue()
            {
                decimal placeValue = 1;
                decimal sum = 0;
                foreach (sbyte digit in digits)
                {
                    sum += placeValue * (decimal)digit;
                    placeValue *= 5;
                }
                return sum;
            }

            public static Snafu operator +(Snafu left, Snafu right)
            {
                int outputLength = 1 + Math.Max(left.digits.Length, right.digits.Length);
                List<sbyte> output = new List<sbyte>(outputLength);
                sbyte GetAt(IList<sbyte> list, int index) => (index >= list.Count) ? (sbyte)0 : list[index];
                sbyte carry = 0;
                for (int i = 0; i < outputLength; ++i)
                {
                    int thisDigit = GetAt(left.digits, i) + GetAt(right.digits, i) + carry;
                    switch (thisDigit)
                    {
                        case -5:
                            carry = -1;
                            output.Add(0);
                            break;
                        case -4:
                            carry = -1;
                            output.Add(1);
                            break;
                        case -3:
                            carry = -1;
                            output.Add(2);
                            break;
                        case -2:
                            carry = 0;
                            output.Add(-2);
                            break;
                        case -1:
                            carry = 0;
                            output.Add(-1);
                            break;
                        case 0:
                            carry = 0;
                            output.Add(0);
                            break;
                        case 1:
                            carry = 0;
                            output.Add(1);
                            break;
                        case 2:
                            carry = 0;
                            output.Add(2);
                            break;
                        case 3:
                            carry = 1;
                            output.Add(-2);
                            break;
                        case 4:
                            carry = 1;
                            output.Add(-1);
                            break;
                        case 5:
                            carry = 1;
                            output.Add(0);
                            break;
                        default:
                            throw new Exception();
                    }
                }
                while (output.Last() == 0)
                {
                    output.RemoveAt(output.Count - 1);
                }
                return new Snafu(output.ToArray());
            }

            public static Snafu Parse(string s)
            {
                return new Snafu(
                    s.Reverse().Select(d => d switch {
                        '0' => (sbyte)0,
                        '1' => (sbyte)1,
                        '2' => (sbyte)2,
                        '-' => (sbyte)-1,
                        '=' => (sbyte)-2,
                        _ => throw new FormatException()
                    }).ToArray()
                );
            }

            public static Snafu Zero = new Snafu([]);
        }
    }
}
