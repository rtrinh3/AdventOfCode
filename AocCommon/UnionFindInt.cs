namespace AocCommon
{
    public class UnionFindInt
    {
        private readonly List<int> nodes = new();
        
        public int Find(int x)
        {
            if (x < 0)
            {
                throw new ArgumentException("Argument must be zero or greater");
            }
            if (nodes.Count <= x)
            {
                nodes.AddRange(Enumerable.Repeat(-1, x - nodes.Count + 1));
            }
            if (nodes[x] < 0)
            {
                return x;
            }
            else
            {
                return nodes[x] = Find(nodes[x]);
            }
        }
        public bool AreMerged(int x, int y) => (Find(x) == Find(y));
        public void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);
            if (rootX == rootY)
            {
                return;
            }
            if (nodes[rootY] > nodes[rootX])
            {
                nodes[rootY] = rootX;
            }
            else if (nodes[rootX] > nodes[rootY])
            {
                nodes[rootX] = rootY;
            }
            else
            {
                nodes[rootY] = rootX;
                nodes[rootX]--;
            }
        }
    }
}
