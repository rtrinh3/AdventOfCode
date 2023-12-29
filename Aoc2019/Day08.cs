using System.Text;
using System.Text.Encodings.Web;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/8
    public class Day08(string input) : IAocDay
    {
        const int width = 25;
        const int height = 6;
        const int layerLength = width * height;
        private readonly List<char[]> layers = input.Chunk(layerLength).ToList();

        public string Part1()
        {
            var minZeroLayer = layers.MinBy(l => l.Count(c => c == '0'));
            var check = minZeroLayer.Count(c => c == '1') * minZeroLayer.Count(c => c == '2');
            return check.ToString();
        }

        public string Part2()
        {
            StringBuilder screen = new();
            for (int pixel = 0; pixel < layerLength; pixel++)
            {
                char color = Enumerable.Range(0, layers.Count).Select(l => layers[l][pixel]).FirstOrDefault(c => c != '2');
                char output = (color == 0) ? '0' : color;
                screen.Append(output);
                if (pixel % width == width - 1)
                {
                    screen.AppendLine();
                }
            }
            var answer = screen.ToString();
            Console.WriteLine(answer.Replace('0', ' ').Replace('1', '█'));
            return answer;
        }
    }
}
