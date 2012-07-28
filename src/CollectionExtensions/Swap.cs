using System.Collections.Generic;

namespace CollectionExtensions
{
    public static partial class Sequence
    {
        /// <summary>
        /// Swaps two elements of an array in place.
        /// </summary>
        public static void Swap<T>(this IList<T> source, int first, int second)
        {
            T temp = source[first];
            source[first] = source[second];
            source[second] = temp;
        }
    }
}
