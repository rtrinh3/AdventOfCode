namespace AocCommon
{
    public class UnionFind<T>
        where T : notnull
    {
        private record struct Node(T parent, int rank);
        private readonly Dictionary<T, Node> nodes = new();

        public T Find(T x)
        {
            if (!nodes.ContainsKey(x))
            {
                nodes[x] = new Node(x, 0);
            }
            if (!object.Equals(x, nodes[x].parent))
            {
                nodes[x] = nodes[x] with { parent = Find(nodes[x].parent) };
            }
            return nodes[x].parent;
        }
        public bool AreMerged(T x, T y) => object.Equals(Find(x), Find(y));
        public void Union(T x, T y)
        {
            x = Find(x);
            y = Find(y);
            if (object.Equals(x, y))
            {
                return;
            }
            if (nodes[x].rank > nodes[y].rank)
            {
                nodes[y] = nodes[y] with { parent = x };
            }
            else if (nodes[x].rank < nodes[y].rank)
            {
                nodes[x] = nodes[x] with { parent = y };
            }
            else
            {
                nodes[y] = nodes[y] with { parent = x };
                nodes[x] = nodes[x] with { rank = nodes[x].rank + 1 };
            }
        }
    }
}
