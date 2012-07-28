using System.Collections.Generic;

namespace CollectionExtensions
{
    public static partial class Sequence
    {
        /// <summary>
        /// Takes multiple enumerations (IEnumerable of T) and combines them into a single
        /// IEnumerable of T.
        /// </summary>
        /// <remarks>
        /// The source enumerations do not have to have the same number of elements.
        /// The combination is done as fairly as possible using round-robin, with some
        /// adjustment when one enumeration finishes before the others.
        /// </remarks>
        public static IEnumerable<T> Interleave<T>(params IEnumerable<T>[] allSources)
        {
            // Initialize the array of enumerations-in-progress.
            var inprogress = new IEnumerator<T>[allSources.Length];
            for (int i = 0; i < allSources.Length; ++i)
            {
                inprogress[i] = allSources[i].GetEnumerator();
            }
            int remaining = inprogress.Length;

            // Return elements from each of the enumerations in turn.
            while (remaining > 0)
            {
                for (int i = 0; i < remaining; ++i)
                {
                    if(inprogress[i].MoveNext())
                    {
                        yield return inprogress[i].Current;
                    }
                    else
                    {
                        inprogress.Swap(i--, --remaining);
                    }
                }
            }
        }
    }
}
