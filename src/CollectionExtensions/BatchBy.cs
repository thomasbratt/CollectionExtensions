using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionExtensions
{
    public static partial class Sequence
    {
        /// <summary>
        /// Groups contiguous elements of a sequence according to a specified key
        /// selector delegate.
        /// </summary>
        /// <remarks>
        /// Similar to GroupBy but elements are grouped by adjacency as well as the
        /// key selector delegate.
        /// The operation is more efficient than GroupBy when processing items that have
        /// already been grouped or sorted by (for example) a database query.
        /// </remarks>
        public static IEnumerable<IGrouping<TKey, TElement>>
            BatchBy<TKey, TElement>(    this IEnumerable<TElement> source,
                                        Func<TElement, TKey> keySelector)
        {
            using (IEnumerator<TElement> enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    yield break;
                }

                TKey previousKey = keySelector(enumerator.Current);
                var batch = new List<TElement>();

                do
                {
                    TKey currentKey = keySelector(enumerator.Current);
                    if (currentKey.Equals(previousKey))
                    {
                        batch.Add(enumerator.Current);
                    }
                    else
                    {
                        yield return new Batch<TKey, TElement>(previousKey, batch);
                        previousKey = currentKey;
                        batch = new List<TElement>();
                        batch.Add(enumerator.Current);
                    }
                }
                while (enumerator.MoveNext());

                yield return new Batch<TKey, TElement>(previousKey, batch);
            }
        }
    }
}
