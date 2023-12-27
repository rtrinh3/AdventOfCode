using System.Text.RegularExpressions;

namespace Aoc2022
{
    public class Day07 : IAocDay
    {
        interface INode
        {
            int GetSize();
        }

        class Directory : INode
        {
            public Dictionary<string, INode> Children { get; } = new Dictionary<string, INode>();
            private readonly Lazy<int> SizeHolder;

            public int GetSize() => SizeHolder.Value;
            public Directory()
            {
                SizeHolder = new Lazy<int>(() => Children.Sum(c => c.Value.GetSize()));
            }
        }

        class File : INode
        {
            public int Size { get; init; }
            public int GetSize() => Size;
        }

        IEnumerable<Directory> DepthFirstTraversal(Directory d)
        {
            List<Directory> head = new List<Directory> { d };
            return head.Concat(d.Children.Values.OfType<Directory>().SelectMany(c => DepthFirstTraversal(c)));
        }

        Directory Root = new Directory();

        public Day07(string input)
        {
            // Parse command log to build file tree
            using (System.IO.StringReader reader = new System.IO.StringReader(input))
            {
                Stack<Directory> path = null;
                void GoToRoot()
                {
                    path = new Stack<Directory>(new Directory[] { Root });
                }
                GoToRoot();

                string line;
                string state = "CMD";
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("$ cd "))
                    {
                        string dir = line.Substring(5);
                        if (dir == "/")
                        {
                            GoToRoot();
                        }
                        else if (dir == "..")
                        {
                            path.Pop();
                        }
                        else
                        {
                            path.Push((Directory)path.Peek().Children[dir]);
                        }
                        state = "CMD";
                    }
                    else if (line.StartsWith("$ ls"))
                    {
                        state = "LS";
                    }
                    else if (line.StartsWith('$'))
                    {
                        throw new InvalidOperationException(line);
                    }
                    else
                    {
                        if (state != "LS")
                        {
                            Console.WriteLine("Warning: not expecting directory listing");
                        }
                        var parsed = Regex.Match(line, @"(\w+) (.+)");
                        string name = parsed.Groups[2].Value;
                        string left = parsed.Groups[1].Value;
                        if (left == "dir")
                        {
                            path.Peek().Children[name] = new Directory();
                        }
                        else if (int.TryParse(left, out int size))
                        {
                            path.Peek().Children[name] = new File { Size = size };
                        }
                    }
                }
            }
        }

        public string Part1()
        {
            return DepthFirstTraversal(Root).Select(d => d.GetSize()).Where(s => s <= 100000).Sum().ToString();
        }
        public string Part2()
        {
            return DepthFirstTraversal(Root).Select(d => d.GetSize()).OrderBy(s => s).SkipWhile(s => s < 8381165).First().ToString();
        }
    }
}
