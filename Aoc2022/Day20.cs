namespace Aoc2022
{
    public class Day20(string input) : IAocDay
    {
        (LinkedList<long> list, List<LinkedListNode<long>> nodes, LinkedListNode<long> nodeZero) Init()
        {
            LinkedList<long> list = new();
            List<LinkedListNode<long>> nodes = new();
            LinkedListNode<long> nodeZero = null;
            foreach (string line in AocCommon.Parsing.SplitLines(input))
            {
                nodes.Add(list.AddLast(long.Parse(line)));
                if (list.Last.Value == 0)
                {
                    nodeZero = list.Last;
                }
            }
            return (list, nodes, nodeZero);
        }
        LinkedListNode<long> GetPreviousNode(LinkedListNode<long> node)
        {
            return node.Previous == null ? node.List.Last : node.Previous;
        }
        LinkedListNode<long> GetNextNode(LinkedListNode<long> node)
        {
            return node.Next == null ? node.List.First : node.Next;
        }
        void Mix(IEnumerable<LinkedListNode<long>> nodes)
        {
            foreach (var node in nodes)
            {
                if (node.Value == 0)
                {
                    continue;
                }
                var targetNode = GetPreviousNode(node);
                node.List.Remove(node);
                long step = Math.Sign(node.Value);
                long count = Math.Abs(node.Value) % targetNode.List.Count;
                for (long i = 0; i < count; ++i)
                {
                    if (step == +1)
                    {
                        targetNode = GetNextNode(targetNode);
                    }
                    else if (step == -1)
                    {
                        targetNode = GetPreviousNode(targetNode);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                targetNode.List.AddAfter(targetNode, node);
            }
        }
        long GetCoordinates(LinkedListNode<long> nodeZero)
        {
            int advanceThousand = 1000 % nodeZero.List.Count;
            var node = nodeZero;
            long thousandSum = 0;
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < advanceThousand; ++j)
                {
                    node = GetNextNode(node);
                }
                thousandSum += node.Value;
            }
            return thousandSum;
        }
        public string Part1()
        {
            var (list, nodes, nodeZero) = Init();
            Mix(nodes);
            var answer = GetCoordinates(nodeZero);
            return answer.ToString();
        }
        public string Part2()
        {
            const long decryptionKey = 811589153L;
            var (list, nodes, nodeZero) = Init();
            foreach (var node in nodes)
            {
                node.Value = node.Value * decryptionKey;
            }
            for (int i = 0; i < 10; ++i)
            {
                Mix(nodes);
            }
            var answer = GetCoordinates(nodeZero);
            return answer.ToString();
        }
    }
}
