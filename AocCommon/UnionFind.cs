namespace AocCommon
{
    // Wrapper around UnionFindInt
    public class UnionFind<T>
        where T : notnull
    {
        private readonly UnionFindInt impl = new();
        private readonly Func<T, int> getIndex;

        public UnionFind()
        {
            // default linearizer: insertion order
            Dictionary<T, int> indices = new();
            getIndex = item =>
            {
                if (indices.TryGetValue(item, out var index))
                {
                    return index;
                }
                else
                {
                    var newIndex = indices.Count;
                    indices.Add(item, newIndex);
                    return newIndex;
                }
            };
        }

        public UnionFind(Func<T, int> linearizer)
        {
            getIndex = linearizer;
        }

        public int Find(T x) => impl.Find(getIndex(x));
        public bool AreMerged(T x, T y) => impl.AreMerged(getIndex(x), getIndex(y));
        public void Union(T x, T y) => impl.Union(getIndex(x), getIndex(y));
    }
}
