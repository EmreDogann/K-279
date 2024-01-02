using System;
using System.Collections.Generic;

namespace Utils.ObservableCollections
{
    public delegate void NotifyCollectionChangedEventHandler<T>(in NotifyCollectionChangedEventArgs<T> e);

    public interface IObservableCollection<T> : IReadOnlyCollection<T>
    {
        event NotifyCollectionChangedEventHandler<T> CollectionChanged;
        object SyncRoot { get; }
    }

    public static class ObservableCollectionsExtensions
    {
        private class AnonymousComparer<T, TCompare> : IComparer<T>
        {
            private readonly Func<T, TCompare> selector;
            private readonly int f;

            public AnonymousComparer(Func<T, TCompare> selector, bool ascending)
            {
                this.selector = selector;
                f = ascending ? 1 : -1;
            }

            public int Compare(T x, T y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }

                if (x == null)
                {
                    return 1 * f;
                }

                if (y == null)
                {
                    return -1 * f;
                }

                return Comparer<TCompare>.Default.Compare(selector(x), selector(y)) * f;
            }
        }
    }
}