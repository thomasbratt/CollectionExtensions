CollectionExtensions
====================

A .NET library for reordering collections (batching, interleaving, randomizing,
shuffling, swapping elements, etc).

API
---

### BatchBy ###

* Groups contiguous elements of a sequence according to a specified key
  selector delegate.
* A common use is processing items in batches that have already been grouped or
  sorted by a database query.
* Similar to GroupBy but elements are grouped by adjacency as well as the
  key selector delegate. The operation is more efficient than GroupBy and
  only visits each element once, without needing to hash or sort the elements.
* Each batch is returned as a type that inherits from List of T, to allow more
  efficient counts and lookups than a plain enumerable would provide.
   
### Interleave ###

* Takes multiple enumerations (IEnumerable of T) and interleaves elements from
  each into a single IEnumerable of T.
  
### Randomize ###

* Generates a pseudo-random sequence using a Galois Linear Feedback
  Shift Register.
* Unlike most alternatives, storage is O(1) and time is O(1).
* The sequence will contain each element exactly once.
* The same sequence is always generated, although it can
  be seeded to start at a different position within the sequence. Although the
  sequence is predictable, it is 'random enough' for many uses.
  
### Shuffle ###

* Shuffles the elements of the input list or array using the
  Knuth-Fisher-Yates shuffle algorithm.
  
### Swap ###
        
* Swaps two elements of a list or array in place.
        
References
----------

* http://en.wikipedia.org/wiki/Linear_feedback_shift_register
* http://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
* http://www.codinghorror.com/blog/2007/12/the-danger-of-naivete.html

License
-------

MIT permissive license. See MIT-LICENSE.txt for full license details.     
     
Source Code Repository
----------------------
 
https://github.com/thomasbratt/collectionextensions
