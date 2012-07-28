using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace CollectionExtensions.UnitTests
{
    [TestFixture]
    public class RandomizeSequenceTest
    {
        // Controls duration of TestRandomizedSequences.
        // Takes around 1.7 seconds with 4 processors.
        private const int MaxSequence = 600;   

        // Controls duration of TestMaxSequenceLengths.
        // A value of 16 takes around 70.76 seconds with 4 processors.
        private const int MaxBitLength = 10;

        /// <summary>
        /// Testing all possible sequences would take a prohibitively long time to complete.
        /// This method tests a sample, based on knowledge of the LFSR algorithm.
        /// </summary>
        [Test]
        public void TestMaxSequenceLengths()
        {
            for (int i = 2; i < MaxBitLength; ++i)
            {
                int length = (1 << i) - 1;

                Debug.WriteLine("Testing (2^{0})-1, length: {1} ", i, length);

                TestSequenceOfLength(length);
            }
        }

        /// <summary>
        /// The following tests that sequences of lengths {3,..,N} are generated
        /// meeting the following conditions:
        /// - The sequence elements are not in ascending order.
        /// - The sequence contains all values from {0,..,length}.
        /// </summary>
        [Test]
        public void TestRandomizedSequences()
        {
            for (int i = 3; i < MaxSequence + 1; ++i)
            {
                TestSequenceOfLength(i);
            }
        }

        /// <summary>
        /// Test that the sequence is generated as expected when a source of
        /// randomness is provided.
        /// </summary>
        [Test]
        public void TestRandomizedSequencsWithExplictRandomSeedSource()
        {            
            // Arrange.
            var random = new Random(9278);
            int[] expected = { 0, 1, 2, };

            // Act.
            int[] actual = Sequence.Randomize(length: 3, random: random)
                                   .ToArray();

            // Assert.

            // The sequence contains all values from {0,..,length}.
            CollectionAssert.AreEquivalent(expected, actual);

            // The sequence elements are not in the same order.
            CollectionAssert.AreNotEqual(expected, actual);
        }

        /// <summary>
        /// Test that the sequence is generated as expected when a source of
        /// randomness is requested but not explicitly provided.
        /// </summary>
        /// <remarks>
        /// May fail intermittently as the source of randomness may generate a
        /// sequence that matches the original.
        /// </remarks>
        [Test]
        public void TestRandomizedSequencsWithImplictRandomSeedSource()
        {
            // Arrange.
            int[] expected = { 0, 1, 2, };

            // Act.
            int[] actual = Sequence.Randomize(length: 3, random:null)
                                   .ToArray();

            // Assert.

            // The sequence contains all values from {0,..,length}.
            CollectionAssert.AreEquivalent(expected, actual);

            // The sequence elements are not in the same order.
            CollectionAssert.AreNotEqual(expected, actual);
        }

        /// <summary>
        /// The following tests that sequences of lengths {3,..,N} are generated
        /// meeting the following conditions:
        /// - The sequence elements are not in the same order.
        /// - The sequences contain the same values from {0,..,length}.
        /// </summary>
        [Test]
        public void TestRandomizedSequencesWithNonDefaultSeed()
        {
            for (int length = 3; length <= MaxSequence; ++length)
            {
                // Arrange.
                int[] expected = Sequence.Randomize(length)
                                         .ToArray();
                int seed = expected.First(x => x != 0);

                // Act.
                int[] actual = Sequence.Randomize(length, seed)
                                       .ToArray();

                // Assert.

                // The sequence contains all values from {0,..,length}.
                CollectionAssert.AreEquivalent(expected, actual);

                // The sequence elements are not in the same order.
                CollectionAssert.AreNotEqual(expected, actual);
            }
        }

        /// <summary>
        /// A length of one is valid and should return a sequence with a single element.
        /// </summary>
        [Test]
        public void TestRandomizedSequenceOfLength1()
        {
            int[] expected = { 0 };

            int[] actual = Sequence.Randomize(1)
                                   .ToArray();

            CollectionAssert.AreEqual(actual, expected);
        }

        /// <summary>
        /// A length of two is valid and should return a sequence with two elements.
        /// </summary>
        [Test]
        public void TestRandomizedSequenceOfLength2()
        {
            int[] expected = { 1, 0 };

            int[] actual = Sequence.Randomize(2)
                                   .ToArray();

            CollectionAssert.AreEqual(actual, expected);
        }

        /// <summary>
        /// A length of zero is valid and should return a sequence with no elements.
        /// </summary>
        [Test]
        public void TestRandomizedSequenceWithLengthZero()
        {
            int[] expected = { };

            int[] actual = Sequence.Randomize(length: 0)
                                   .ToArray();

            CollectionAssert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Negative sequence lengths are invalid and should cause an exception to be thrown.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRandomizedSequenceWithNegativeLength()
        {
            Sequence.Randomize(length: -1);
        }

        /// <summary>
        /// Negative seeds are invalid and should cause an exception to be thrown.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRandomizedSequenceWithNegativeSeed()
        {
            Sequence.Randomize(length:4, seed:-1);
        }

        /// <summary>
        /// Negative seeds are invalid and should cause an exception to be thrown.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRandomizedSequenceWithSeedGreaterThanLength()
        {
            Sequence.Randomize(length:4, seed:4);
        }

        private static void TestSequenceOfLength(int length)
        {
            // Arrange.
            int[] expected = Enumerable.Range(0, length)
                                       .ToArray();

            // Act.

            IEnumerable<int> sequence = Sequence.Randomize(expected.Length);
            IEnumerator<int> iterator = sequence.GetEnumerator();
            
            // Take the expected number.
            int[] actual = new int[length];
            for(int i=0; i<length; ++i)
            {
                Assert.IsTrue(iterator.MoveNext());
                actual[i] = iterator.Current;
            }

            // Should not be any more elements returned from the sequence.
            Assert.IsFalse(iterator.MoveNext());

            // Debugging diagnostics.
            //string message = String.Format( CultureInfo.InvariantCulture,
            //                                "Sequence of length {0} is: {1}",
            //                                length,
            //                                String.Join(",", actual));
            //Debug.WriteLine(message);

            // Assert.

            // The sequence contains all values from {0,..,length}.
            CollectionAssert.AreEquivalent(expected, actual);

            // The sequence elements are not in ascending order.
            CollectionAssert.AreNotEqual(expected, actual);
        }
    }
}
