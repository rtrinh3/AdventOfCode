namespace Aoc2022
{
    public class Day13(string input) : IAocDay
    {
        private readonly string[] inputs = input.Split('\n');

        private static int Compare(System.Text.Json.JsonElement left, System.Text.Json.JsonElement right)
        {
            if (left.ValueKind == System.Text.Json.JsonValueKind.Number && right.ValueKind == System.Text.Json.JsonValueKind.Number)
            {
                return left.GetDouble().CompareTo(right.GetDouble());
            }
            else if (left.ValueKind == System.Text.Json.JsonValueKind.Array && right.ValueKind == System.Text.Json.JsonValueKind.Array)
            {
                for (int i = 0; i < left.GetArrayLength() && i < right.GetArrayLength(); ++i)
                {
                    int comparison = Compare(left[i], right[i]);
                    if (comparison != 0)
                    {
                        return comparison;
                    }
                }
                return left.GetArrayLength().CompareTo(right.GetArrayLength());
            }
            else if (left.ValueKind == System.Text.Json.JsonValueKind.Array)
            {
                var boxRightString = string.Format("[{0}]", right);
                var boxRight = System.Text.Json.JsonDocument.Parse(boxRightString).RootElement;
                return Compare(left, boxRight);
            }
            else
            {
                var boxLeftString = string.Format("[{0}]", left);
                var boxLeft = System.Text.Json.JsonDocument.Parse(boxLeftString).RootElement;
                return Compare(boxLeft, right);
            }
        }

        public string Part1()
        {
            int count = 0;
            for (int i = 0; i < inputs.Length; i += 3)
            {
                var left = System.Text.Json.JsonDocument.Parse(inputs[i]).RootElement;
                var right = System.Text.Json.JsonDocument.Parse(inputs[i + 1]).RootElement;
                if (Compare(left, right) < 0)
                {
                    int index = (i / 3) + 1;
                    count += index;
                }
            }
            return count.ToString();
        }

        public string Part2()
        {
            var list = inputs
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => System.Text.Json.JsonDocument.Parse(s).RootElement)
                .ToList();
            var marker2 = System.Text.Json.JsonDocument.Parse("[[2]]").RootElement;
            list.Add(marker2);
            var marker6 = System.Text.Json.JsonDocument.Parse("[[6]]").RootElement;
            list.Add(marker6);
            list.Sort(Compare);
            int index2 = 1 + list.IndexOf(marker2);
            int index6 = 1 + list.IndexOf(marker6);
            var answer = (index2 * index6);
            return answer.ToString();
        }
    }
}
