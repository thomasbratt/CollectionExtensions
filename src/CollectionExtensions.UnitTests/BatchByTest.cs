using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CollectionExtensions.UnitTests
{
    /// <summary>
    /// Tests to ensure the BatchBy extension method works as expected.
    /// </summary>
    [TestFixture]
    public class BatchByTest
    {
        /// <summary>
        /// Test that the sequence of elements is correctly processed when the
        /// batch size is 1.
        /// </summary>
        [Test]
        public void TestBatchSizeOf1()
        {
            // Arrange.
            IEnumerable<int> data = new int[] { 1, 2, 3, 4 };
            var expected1 = new int[] { 1, };
            var expected2 = new int[] { 2, };
            var expected3 = new int[] { 3, };
            var expected4 = new int[] { 4, };

            // Act.
            IEnumerable<IGrouping<int, int>> actual = data.BatchBy(x => x * 10);

            // Assert.
            using (IEnumerator<IGrouping<int, int>> current = actual.GetEnumerator())
            {
                Assert.IsTrue(current.MoveNext());
                IGrouping<int, int> group1 = current.Current;
                Assert.IsTrue(group1.Key == 10);
                CollectionAssert.AreEqual(group1.ToArray(), expected1);

                Assert.IsTrue(current.MoveNext());
                IGrouping<int, int> group2 = current.Current;
                Assert.IsTrue(group2.Key == 20);
                CollectionAssert.AreEqual(group2.ToArray(), expected2);

                Assert.IsTrue(current.MoveNext());
                IGrouping<int, int> group3 = current.Current;
                Assert.IsTrue(group3.Key == 30);
                CollectionAssert.AreEqual(group3.ToArray(), expected3);

                Assert.IsTrue(current.MoveNext());
                IGrouping<int, int> group4 = current.Current;
                Assert.IsTrue(group4.Key == 40);
                CollectionAssert.AreEqual(group4.ToArray(), expected4);

                Assert.IsFalse(current.MoveNext());
            }
        }

        /// <summary>
        /// Test that empty collections are handled correctly.
        /// </summary>
        [Test]
        public void TestEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> data = new int[] { };

            // Act.
            IEnumerable<IGrouping<int, int>> actual = data.BatchBy(x => x);

            // Assert.
            Assert.IsTrue(actual.Count() == 0);
        }

        /// <summary>
        /// Test that multiple batches in a sequence are split correctly.
        /// </summary>
        [Test]
        public void TestMultipleBatches()
        {
            // Arrange.
            IEnumerable<int> data = new int[] { 1, 1, 1, 4, 5, 5, 6, 6, 6 };
            var expected1 = new int[] { 1, 1, 1 };
            var expected2 = new int[] { 4, };
            var expected3 = new int[] { 5, 5 };
            var expected4 = new int[] { 6, 6, 6 };

            // Act.
            IEnumerable<IGrouping<int, int>> actual = data.BatchBy(x => x * 10);

            // Assert.
            using (IEnumerator<IGrouping<int, int>> current = actual.GetEnumerator())
            {
                Assert.IsTrue(current.MoveNext());
                IGrouping<int, int> group1 = current.Current;
                Assert.IsTrue(group1.Key == 10);
                CollectionAssert.AreEqual(group1.ToArray(), expected1);

                Assert.IsTrue(current.MoveNext());
                IGrouping<int, int> group2 = current.Current;
                Assert.IsTrue(group2.Key == 40);
                CollectionAssert.AreEqual(group2.ToArray(), expected2);

                Assert.IsTrue(current.MoveNext());
                IGrouping<int, int> group3 = current.Current;
                Assert.IsTrue(group3.Key == 50);
                CollectionAssert.AreEqual(group3.ToArray(), expected3);

                Assert.IsTrue(current.MoveNext());
                IGrouping<int, int> group4 = current.Current;
                Assert.IsTrue(group4.Key == 60);
                CollectionAssert.AreEqual(group4.ToArray(), expected4);

                Assert.IsFalse(current.MoveNext());
            }
        }

        /// <summary>
        /// Test that the sequence is split correctly when the batch key value
        /// is repeated in a later batch.
        /// </summary>
        [Test]
        public void TestMultipleBatchesWithSameKey()
        {
            // Arrange.
            IEnumerable<int> data = new int[] { 1, 1, 1, 4, 1, 1, 6, 6, 6 };
            var expected1 = new int[] { 1, 1, 1 };
            var expected2 = new int[] { 4, };
            var expected3 = new int[] { 1, 1 };
            var expected4 = new int[] { 6, 6, 6 };

            // Act.
            IEnumerable<IGrouping<int, int>> actual = data.BatchBy(x => x * 10);

            // Assert.
            using (IEnumerator<IGrouping<int, int>> current = actual.GetEnumerator())
            {
                Assert.IsTrue(current.MoveNext());
                IGrouping<int, int> group1 = current.Current;
                Assert.IsTrue(group1.Key == 10);
                CollectionAssert.AreEqual(group1.ToArray(), expected1);

                Assert.IsTrue(current.MoveNext());
                IGrouping<int, int> group2 = current.Current;
                Assert.IsTrue(group2.Key == 40);
                CollectionAssert.AreEqual(group2.ToArray(), expected2);

                Assert.IsTrue(current.MoveNext());
                IGrouping<int, int> group3 = current.Current;
                Assert.IsTrue(group3.Key == 10);
                CollectionAssert.AreEqual(group3.ToArray(), expected3);

                Assert.IsTrue(current.MoveNext());
                IGrouping<int, int> group4 = current.Current;
                Assert.IsTrue(group4.Key == 60);
                CollectionAssert.AreEqual(group4.ToArray(), expected4);

                Assert.IsFalse(current.MoveNext());
            }
        }

        /// <summary>
        /// Test that batching still works when the key has a null value.
        /// </summary>
        [Test]
        public void TestNullKey()
        {
            // Arrange.
            IEnumerable<int> data = new int[] { 1, 1, 1, 4, 5, 5, 6, 6, 6, 7 };
            var expected1 = new int[] { 1, 1, 1, };
            var expected2 = new int[] { 4, };
            var expected3 = new int[] { 5, 5, 6, 6, 6, };
            var expected4 = new int[] { 7, };

            // Act.
            // Key selector will assign null keys to batches with elements 5 or 6.
            IEnumerable<IGrouping<int?, int>> actual = data.BatchBy(x =>
                    (x == 5 || x == 6)
                ? (int?)null
                : (int?)(x * 10));

            // Assert.
            using (IEnumerator<IGrouping<int?, int>> current = actual.GetEnumerator())
            {
                Assert.IsTrue(current.MoveNext());
                IGrouping<int?, int> group1 = current.Current;
                Assert.IsTrue(group1.Key == 10);
                CollectionAssert.AreEqual(group1.ToArray(), expected1);

                Assert.IsTrue(current.MoveNext());
                IGrouping<int?, int> group2 = current.Current;
                Assert.IsTrue(group2.Key == 40);
                CollectionAssert.AreEqual(group2.ToArray(), expected2);

                Assert.IsTrue(current.MoveNext());
                IGrouping<int?, int> group3 = current.Current;
                Assert.IsTrue(group3.Key == null);
                CollectionAssert.AreEqual(group3.ToArray(), expected3);

                Assert.IsTrue(current.MoveNext());
                IGrouping<int?, int> group4 = current.Current;
                Assert.IsTrue(group4.Key == 70);
                CollectionAssert.AreEqual(group4.ToArray(), expected4);

                Assert.IsFalse(current.MoveNext());
            }
        }

        /// <summary>
        /// Test that a single batch of items is handled correctly.
        /// </summary>
        [Test]
        public void TestSingleBatch()
        {
            // Arrange.
            IEnumerable<int> data = new int[] { 1, 1, 1 };
            var expected1 = new int[] { 1, 1, 1 };

            // Act.
            IEnumerable<IGrouping<int, int>> actual = data.BatchBy(x => x);
            IGrouping<int, int> group1 = actual.First();

            // Assert.
            Assert.IsTrue(actual.Count() == 1);
            Assert.IsTrue(group1.Count() == 3);
            CollectionAssert.AreEqual(expected1, group1.ToArray());
        }

        /// <summary>
        /// Test that a single element in the source sequence is handled correctly.
        /// </summary>
        [Test]
        public void TestSingleElement()
        {
            // Arrange.
            IEnumerable<int> data = new int[] { 42, };
            var expected1 = new int[] { 42 };

            // Act.
            IEnumerable<IGrouping<int, int>> actual = data.BatchBy(x => x);
            IGrouping<int, int> group1 = actual.First();

            // Assert.
            Assert.IsTrue(actual.Count() == 1);
            Assert.IsTrue(group1.Count() == 1);
            CollectionAssert.AreEqual(expected1, group1.ToArray());
        }

        [Test]
        public void TestThatBatchesAreExposedAsLists()
        {
            // Arrange.
            IEnumerable<int> data = new int[] { 1, 2, 2, 3, 3, 3 };

            // Act.
            IEnumerable<IGrouping<int, int>> actual = data.BatchBy(x => x);

            // Assert.
            int expected = 0;
            foreach (List<int> list in actual)
            {
                Assert.AreEqual(++expected, list.Count);
            }
        }
    }
}
