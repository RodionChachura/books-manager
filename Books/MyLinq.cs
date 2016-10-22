using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books
{
    public interface IOrderingImpl<T> : IEnumerable<T>
    {
        int CompareTo(T left, T right);
        IEnumerable<T> OriginalSource { get; }
    }
    public static class MyLinq
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            foreach (T item in sequence)
                if (predicate(item))
                    yield return item;
        }

        public static IEnumerable<T2> Select<T1, T2>(this IEnumerable<T1> sequence, Func<T1, T2> transform)
        {
            foreach (T1 item in sequence)
                yield return transform(item);
        }

        public static IEnumerable<T2> Select<T1, T2>(this IEnumerable<T1> sequence, Func<T1, int, T2> transform)
        {
            int index = 0;
            foreach (T1 item in sequence)
                yield return transform(item, index++);
        }

        public static bool Any<T>(this IEnumerable<T> sequence)
        {
            return sequence.GetEnumerator().MoveNext();
        }

        public static bool Any<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            return sequence.Where(predicate).GetEnumerator().MoveNext();
        }

        public static int Count<T>(this IEnumerable<T> sequence)
        {
            int count = 0;
            foreach (T item in sequence)
                count++;
            return count;
        }

        public static int Count<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            int count = 0;
            foreach (T item in sequence)
                if(predicate(item))
                    count++;
            return count;
        }

        public static int Sum(this IEnumerable<int> sequence, int seen=0)
        {
            int sum = seen;
            foreach (int item in sequence)
                sum += item;
            return sum;
        }

        public static int Aggregate(this IEnumerable<int> sequence, Func<int, int, int> func, int seed=0)
        {
            int sum = seed;
            foreach (int item in sequence)
                sum = func(sum, item);
            return sum;
        }

        public static T Aggregate<T>(this IEnumerable<T> sequence, Func<T, T, T> func, T seed = default(T))
        {
            T sum = seed;
            foreach (T item in sequence)
                sum = func(sum, item);
            return sum;
        }

        public static T2 Aggregate<T1, T2>(this IEnumerable<T1> sequence, Func<T2, T1, T2> func, T2 seed=default(T2))
        {
            var sum = seed;
            foreach (var item in sequence)
                sum = func(sum, item);
            return sum;
        }

        public static IOrderingImpl<T> MyOrderBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> comparer)
            where TKey : IComparable<TKey>
        {
            return new MyOrderedEnumerable<T, TKey>(source, comparer);
        }

        public static IOrderingImpl<T> MyThenBy<T, TKey>(this IOrderingImpl<T> source, Func<T, TKey> comparer)
            where TKey : IComparable<TKey>
        {
            return new MyOrderedEnumerable<T, TKey>(source, comparer);
        }
    }
    public class MyOrderedEnumerable<T, TKey> : IOrderingImpl<T>, IEnumerable<T> where TKey : IComparable<TKey>
    {
        private Comparison<T> comparison;
        private IEnumerable<T> source;

        public MyOrderedEnumerable(IOrderingImpl<T> source, Func<T, TKey> comparer)
        {
            this.source = source;
            comparison = (a, b) =>
            {
                var originalComparison = source.CompareTo(a, b);
                if (originalComparison != 0)
                    return originalComparison;
                else
                    return comparer(a).CompareTo(comparer(b));
            };
        }

        public MyOrderedEnumerable(IEnumerable<T> source, Func<T, TKey> comparer)
        {
            this.source = source;
            comparison = (a, b) => comparer(a).CompareTo(comparer(b));
        }

        public IEnumerable<T> OriginalSource
        {
            get
            {
                return source;
            }
        }

        public int CompareTo(T left, T right)
        {
            return comparison(left, right);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var sorted = source.ToList();
            sorted.Sort(comparison);
            return sorted.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
