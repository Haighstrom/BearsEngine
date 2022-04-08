namespace BearsEngine.Pathfinding
{
    public interface IPriorityQueue<T>
    {
        int Push(T item);
        T Pop();
        T Peek();
        void Update(int i);
    }

    public class PriorityQueue<T> : IPriorityQueue<T>
    {
        protected List<T> InnerList = new();
        protected IComparer<T> Comparer;

        public PriorityQueue()
        {
            Comparer = Comparer<T>.Default;
        }
        public PriorityQueue(IComparer<T> comparer)
        {
            Comparer = comparer;
        }
        public PriorityQueue(IComparer<T> comparer, int capacity)
        {
            Comparer = comparer;
            InnerList.Capacity = capacity;
        }

        public T this[int index]
        {
            get { return InnerList[index]; }
            set
            {
                InnerList[index] = value;
                Update(index);
            }
        }

        protected void SwapElements(int i, int j)
        {
            T temp = InnerList[i];
            InnerList[i] = InnerList[j];
            InnerList[j] = temp;
        }

        protected virtual int Compare(int i, int j)
        {
            return Comparer.Compare(InnerList[i], InnerList[j]);
        }

        /// <summary>
        /// Push an item onto the PQ. It will be added at the position returned.
        /// Uses heap-ordered binary tree structure for insertion
        /// </summary>
        public int Push(T item)
        {
            int p = InnerList.Count, p2;
            InnerList.Add(item);
            do
            {
                if (p == 0)
                    break;
                p2 = (p - 1) / 2;
                if (Compare(p, p2) < 0)
                {
                    SwapElements(p, p2);
                    p = p2;
                }
                else
                    break;
            } while (true);
            return p;
        }

        /// <summary>
        /// Get the smallest object and remove it.
        /// Sorts the binary tree beneath the node removex
        /// </summary>
        public T Pop()
        {
            T result = InnerList[0];
            int p = 0, p1, p2, pn;
            InnerList[0] = InnerList[InnerList.Count - 1];
            InnerList.RemoveAt(InnerList.Count - 1);
            do
            {
                pn = p;
                p1 = 2 * p + 1;
                p2 = 2 * p + 2;
                if (InnerList.Count > p1 && Compare(p, p1) > 0) // links kleiner
                    p = p1;
                if (InnerList.Count > p2 && Compare(p, p2) > 0) // rechts noch kleiner
                    p = p2;

                if (p == pn)
                    break;
                SwapElements(p, pn);
            } while (true);

            return result;
        }

        /// <summary>
        /// Notify the PQ that the object at position i has changed
        /// and the PQ needs to restore order.
        /// Since you dont have access to any indexes (except by using the
        /// explicit IList.this) you should not call this function without knowing exactly
        /// what you do.
        /// </summary>
        /// <param name="i">The index of the changed object.</param>
        public void Update(int i)
        {
            int p = i, pn;
            int p1, p2;
            do	// aufsteigen
            {
                if (p == 0)
                    break;
                p2 = (p - 1) / 2;
                if (Compare(p, p2) < 0)
                {
                    SwapElements(p, p2);
                    p = p2;
                }
                else
                    break;
            } while (true);
            if (p < i)
                return;
            do	   // absteigen
            {
                pn = p;
                p1 = 2 * p + 1;
                p2 = 2 * p + 2;
                if (InnerList.Count > p1 && Compare(p, p1) > 0) // links kleiner
                    p = p1;
                if (InnerList.Count > p2 && Compare(p, p2) > 0) // rechts noch kleiner
                    p = p2;

                if (p == pn)
                    break;
                SwapElements(p, pn);
            } while (true);
        }

        /// <summary>
        /// Get the smallest object without removing it.
        /// </summary>
        /// <returns>The smallest object</returns>
        public T Peek()
        {
            if (InnerList.Count > 0) return InnerList[0];
            return default;
        }

        public void Clear() { InnerList.Clear(); }

        public int Count { get { return InnerList.Count; } }

        public void Remove(T item)
        {
            int index = -1;
            for (int i = 0; i < InnerList.Count; i++)
            {

                if (Comparer.Compare(InnerList[i], item) == 0)
                    index = i;
            }

            if (index != -1)
                InnerList.RemoveAt(index);
        }
    }
}
