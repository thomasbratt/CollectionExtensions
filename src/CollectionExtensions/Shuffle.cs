using System;
using System.Collections.Generic;

namespace CollectionExtensions
{
    public static partial class Sequence
    {
        /// <summary>
        /// Shuffle the elements of the input array using the
        /// Knuth-Fisher-Yates shuffle algorithm.
        /// </summary>
        /// <returns>
        /// A shuffled copy of the source array.
        /// </returns>
        public static IList<T> Shuffle<T>(this IList<T> source, Random random = null)
        {
            if (random == null)
            {
                random = new Random();
            }

            // Take a copy.
            var result = new T[source.Count];
            for (int i = 0; i < source.Count; ++i)
            {
                result[i] = source[i];
            }

            // Shuffle the copy.
            result.ShuffleInPlace(random);

            return result;
        }

        /// <summary>
        /// Shuffle the elements of the input array using the
        /// Knuth-Fisher-Yates shuffle algorithm.
        /// </summary>
        public static void ShuffleInPlace<T>(this IList<T> source, Random random = null)
        {
            for (int i = source.Count - 1; i > 0; i--)
            {
                int n = random.Next(i + 1);
                source.Swap(i, n);
            }
        }
    }
}
