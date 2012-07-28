using System.Collections.Generic;
using System.Linq;

namespace CollectionExtensions
{
    /// <summary>
    /// Holds a batch of elements, where each one shares a common key.
    /// </summary>
    /// <remarks>
    /// This class uses inheritance from List of TElement rather than
    /// aggregation so that the List can be used, if required.
    /// </remarks>
    internal class Batch<TKey, TElement> : List<TElement>,
        IGrouping<TKey, TElement>
    {
        public TKey Key { get; private set; }

        public Batch(TKey key, IEnumerable<TElement> elements) :
            base(elements)
        {
            this.Key = key;
        }
    }
}
