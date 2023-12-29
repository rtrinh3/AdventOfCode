using System.Numerics;

namespace Aoc2019
{
    public class IntcodeInterpreter
    {
        private static int OpCodeParameterCount(int opCode)
        {
            return opCode switch
            {
                1 => 3,
                2 => 3,
                3 => 1,
                4 => 1,
                5 => 2,
                6 => 2,
                7 => 3,
                8 => 3,
                9 => 1,
                99 => 0,
                _ => 0
            };
        }
        private readonly IReadOnlyList<BigInteger> prgm;
        private readonly Dictionary<BigInteger, BigInteger> mask = new Dictionary<BigInteger, BigInteger>();
        private BigInteger Ip = 0;
        private BigInteger Rb = 0;

        public IntcodeInterpreter(IReadOnlyList<BigInteger> program)
        {
            prgm = program;
        }
        public IntcodeInterpreter(string program)
        {
            prgm = program.Split(',').Select(BigInteger.Parse).ToArray();
        }

        public BigInteger? RunUntilOutputImpl(IEnumerator<BigInteger> inputIterator)
        {
            while (true)
            {
                BigInteger instruction = Peek(Ip);
                int opCode = (int)(instruction % 100);
                int parameterCount = OpCodeParameterCount(opCode);

                // Parameters
                BigInteger parameterModes = instruction / 100;
                BigInteger[] parameters = new BigInteger[parameterCount];
                for (int i = 0; i < parameterCount; i++)
                {
                    BigInteger parameterAddress = Ip + 1 + i;
                    int parameterMode = (int)(parameterModes % 10);
                    parameters[i] = parameterMode switch
                    {
                        0 => Peek(parameterAddress), // Position
                        1 => parameterAddress, // Immediate
                        2 => Rb + Peek(parameterAddress), // Relative
                        _ => throw new NotImplementedException($"Unknown parameter mode {parameterMode}")
                    };
                    parameterModes /= 10;
                }

                // Execute
                switch (opCode)
                {
                    case 1:
                        Poke(parameters[2], Peek(parameters[0]) + Peek(parameters[1]));
                        Ip += parameterCount + 1;
                        break;
                    case 2:
                        Poke(parameters[2], Peek(parameters[0]) * Peek(parameters[1]));
                        Ip += parameterCount + 1;
                        break;
                    case 3:
                        if (!inputIterator.MoveNext())
                        {
                            throw new InvalidOperationException("Not enough inputs");
                        }
                        Poke(parameters[0], inputIterator.Current);
                        Ip += parameterCount + 1;
                        break;
                    case 4:
                        Ip += parameterCount + 1;
                        return Peek(parameters[0]);
                    case 5:
                        if (Peek(parameters[0]) != 0)
                        {
                            Ip = Peek(parameters[1]);
                        }
                        else
                        {
                            Ip += parameterCount + 1;
                        }
                        break;
                    case 6:
                        if (Peek(parameters[0]) == 0)
                        {
                            Ip = Peek(parameters[1]);
                        }
                        else
                        {
                            Ip += parameterCount + 1;
                        }
                        break;
                    case 7:
                        Poke(parameters[2], (Peek(parameters[0]) < Peek(parameters[1])) ? 1 : 0);
                        Ip += parameterCount + 1;
                        break;
                    case 8:
                        Poke(parameters[2], (Peek(parameters[0]) == Peek(parameters[1])) ? 1 : 0);
                        Ip += parameterCount + 1;
                        break;
                    case 9:
                        Rb += Peek(parameters[0]);
                        Ip += parameterCount + 1;
                        break;
                    case 99:
                        // Stay on Stop instruction, do not increment ip
                        return null;
                    default:
                        throw new NotImplementedException($"Unknown opCode {instruction} at address {Ip}");
                }
            }
        }
        public BigInteger? RunUntilOutput(params BigInteger[] inputs)
        {
            return RunUntilOutputImpl(inputs.AsEnumerable().GetEnumerator());
        }
        public IEnumerable<BigInteger> RunToEnd(IEnumerable<BigInteger>? inputs = null)
        {
            if (inputs == null)
            {
                inputs = Array.Empty<BigInteger>();
            }
            var inputIterator = inputs.GetEnumerator();
            while (true)
            {
                var output = RunUntilOutputImpl(inputIterator);
                if (output != null)
                {
                    yield return output.Value;
                }
                else
                {
                    break;
                }
            }
        }
        public string Dump()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine($"IP {Ip} \tRBO {Rb}");
            BigInteger cursor = 0;
            var addressesAfterPrgm = mask.Keys.Where(a => a >= prgm.Count).ToList();
            addressesAfterPrgm.Sort();
            var maxAddressAfterPrgm = addressesAfterPrgm.Any() ? addressesAfterPrgm[^1] : 0;
            BigInteger upperBound = (prgm.Count > maxAddressAfterPrgm + 1) ? prgm.Count : maxAddressAfterPrgm + 1;
            while (cursor < upperBound)
            {
                sb.Append(cursor.ToString());
                sb.Append(":\t");

                var instruction = Peek(cursor);
                sb.Append(instruction.ToString());
                sb.Append('\t');

                int opCode = (int)(instruction % 100);
                string opCodeMnemonic = opCode switch
                {
                    1 => "Add\t",
                    2 => "Mul\t",
                    3 => "In\t",
                    4 => "Out\t",
                    5 => "Jnz\t",
                    6 => "Jz\t",
                    7 => "Ls\t",
                    8 => "Eq\t",
                    9 => "Rbo\t",
                    99 => "Stop\t",
                    _ => "?\t"
                };
                sb.Append(opCodeMnemonic);

                // Parameters
                int parameterCount = OpCodeParameterCount(opCode);
                var parameterModes = instruction / 100;
                for (int i = 0; i < parameterCount; i++)
                {
                    int parameterMode = (int)(parameterModes % 10);
                    string prefix = parameterMode switch
                    {
                        0 => "*",
                        1 => "#",
                        2 => "~",
                        _ => $"{parameterMode}?"
                    };
                    sb.Append(prefix);
                    sb.Append(Peek(cursor + 1 + i));
                    sb.Append('\t');
                    parameterModes /= 10;
                }
                sb.AppendLine();

                // Cursor adjustment
                cursor += parameterCount + 1;
                if (cursor >= prgm.Count)
                {
                    var remainingAddresses = addressesAfterPrgm.SkipWhile(a => a < cursor);
                    if (remainingAddresses.Any())
                    {
                        cursor = remainingAddresses.First();
                    }
                }
            }
            return sb.ToString();
        }
        public void Reset()
        {
            mask.Clear();
            Ip = 0;
            Rb = 0;
        }
        public BigInteger Peek(BigInteger address)
        {
            if (mask.TryGetValue(address, out var value))
            {
                return value;
            }
            else if (0 <= address && address < prgm.Count)
            {
                return prgm[(int)address];
            }
            else
            {
                return mask[address] = BigInteger.Zero;
            }
        }
        public void Poke(BigInteger address, BigInteger val)
        {
            mask[address] = val;
        }
    }
}
