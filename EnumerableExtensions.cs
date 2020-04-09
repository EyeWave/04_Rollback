using System;
using System.Collections.Generic;

namespace TaskRollback
{
    public static class EnumerableExtensions
    {
        public static void Each<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (action != null)
                foreach (var element in collection)
                    action.Invoke(element);
        }

        public struct Indexer<T>
        {
            public readonly int index;
            public readonly T item;

            public Indexer(int index, T item)
            {
                this.index = index;
                this.item = item;
            }
        }

        public static IEnumerable<Indexer<T>> Enumerated<T>(this IEnumerable<T> collection)
        {
            var i = 0;
            var enumerator = collection.GetEnumerator();

            try
            {
                while (enumerator.MoveNext())
                    yield return new Indexer<T>(i++, enumerator.Current);
            }
            finally
            {
                enumerator?.Dispose();
                enumerator = null;
            }
        }
    }
}
