using AocCommon;
using System.Numerics;
using System.Text;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/13
    public class Day13(string input) : IAocDay
    {
        private readonly IntcodeInterpreter interpreter = new(input);
        private readonly Dictionary<VectorXY, BigInteger> screen = new();
        private readonly VectorXY scoreCoord = new(-1, 0);
        private const bool ANIMATE = false;

        public string Part1()
        {
            screen.Clear();
            interpreter.Reset();
            var output1 = interpreter.RunToEnd();
            UpdateScreen(output1);
            if (ANIMATE)
            {
                ShowScreen();
            }
            int numberOfTiles = screen.Values.Count(t => t == 2);
            return numberOfTiles.ToString();
        }

        public string Part2()
        {
            screen.Clear();
            interpreter.Reset();
            interpreter.Poke(0, 2);
            var output2 = interpreter.RunToEnd(BallChaserInputs());
            UpdateScreen(output2);
            if (ANIMATE)
            {
                ShowScreen();
            }
            var finalScore = screen[scoreCoord];
            return finalScore.ToString();
        }

        IEnumerable<BigInteger> InteractiveInputs()
        {
            while (true)
            {
                ShowScreen();
                Console.Write("Joystick? (A=Left, D=Right, Other=Neutral) > ");
                string line = Console.ReadLine();
                yield return line switch
                {
                    "a" => -1,
                    "d" => +1,
                    _ => 0
                };
            }
        }

        IEnumerable<BigInteger> BallChaserInputs()
        {
            while (true)
            {
                if (ANIMATE)
                {
                    ShowScreen();
                    System.Threading.Thread.Sleep(20);
                }
                var ball = screen.First(kvp => kvp.Value == 4).Key.X;
                var paddle = screen.First(kvp => kvp.Value == 3).Key.X;
                if (ball < paddle)
                {
                    yield return -1;
                }
                else if (ball == paddle)
                {
                    yield return 0;
                }
                else if (paddle < ball)
                {
                    yield return 1;
                }
            }
        }

        void ShowScreen()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Score {screen.GetValueOrDefault(scoreCoord)}");
            int xMin = screen.Keys.Select(v => v.X).Min();
            int xMax = screen.Keys.Select(v => v.X).Max();
            int yMin = screen.Keys.Select(v => v.Y).Min();
            int yMax = screen.Keys.Select(v => v.Y).Max();
            for (int row = yMin; row <= yMax; row++)
            {
                for (int col = xMin; col <= xMax; col++)
                {
                    VectorXY coords = new(col, row);
                    var tile = screen.GetValueOrDefault(coords);
                    sb.Append(tile == 0 ? ' ' : tile.ToString()[^1]);
                }
                sb.AppendLine();
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(sb.ToString());
        }

        void UpdateScreen(IEnumerable<BigInteger> machineOutput)
        {
            foreach (var chunk in machineOutput.Chunk(3))
            {
                screen[new VectorXY((int)chunk[0], (int)chunk[1])] = chunk[2];
            }
        }
    }
}
