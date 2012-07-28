using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CollectionExtensions.UnitTests
{
    /// <summary>
    /// Tests that the Interleave static method behaves correctly when presented
    /// with empty collections, collections of different lengths and so on.
    /// </summary>
    [TestFixture]
    public class InterleaveTest
    {
        [Test]
        public void TestSingleEmptySequence()
        {
            int[] data = { };
            int[] expected = { };

            IEnumerable<int> actual = Sequence.Interleave(data);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMultipleEmptySequences()
        {
            int[] data1 = { };
            int[] data2 = { };
            int[] data3 = { };
            int[] expected = { };

            IEnumerable<int> actual = Sequence.Interleave(data1, data2, data3);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMixOfEmptyAndNonEmptySequences()
        {
            int[] data1 = { };
            int[] data2 = { 2, 5 };
            int[] data3 = { };
            int[] data4 = { 3, 6 };
            int[] data5 = { };
            int[] data6 = { 1, 4 };
            int[] expected = { 1, 2, 3, 4, 5, 6 };

            IEnumerable<int> actual = Sequence.Interleave(data1, data2, data3, data4, data5, data6);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TestSingleSequence()
        {
            int[] data = { 1, 2, 3 };
            int[] expected = { 1, 2, 3 };

            IEnumerable<int> actual = Sequence.Interleave(data);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMultipleSequencesOfSameLength()
        {
            int[] data1 = { 1, 4, 7 };
            int[] data2 = { 2, 5, 8 };
            int[] data3 = { 3, 6, 9 };
            int[] expected =
            {
                1, 2, 3,
                4, 5, 6,
                7, 8, 9,
            };

            IEnumerable<int> actual = Sequence.Interleave(data1, data2, data3);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMultipleSequencesOfDifferentLengths1()
        {
            int[] data1 = { 1, 4, 6 };
            int[] data2 = { 2, 5 };
            int[] data3 = { 3 };
            int[] expected =
            {
                1, 2, 3, 4, 5, 6,
            };

            IEnumerable<int> actual = Sequence.Interleave(data1, data2, data3);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMultipleSequencesOfDifferentLengths2()
        {
            int[] data1 = { 1 };
            int[] data2 = { 2, 5 };
            int[] data3 = { 3, 4, 6 };
            int[] expected =
            {
                1, 2, 3, 4, 5, 6,
            };

            int[] actual = Sequence.Interleave(data1, data2, data3)
                                   .ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMultipleSequencesOfDifferentLengths3()
        {
            int[] data1 = { 1, 4, 6, 7, 8, 9 };
            int[] data2 = { 2 };
            int[] data3 = { 3, 5 };
            var allSources = new IEnumerable<int>[] { data1, data2, data3 };
            int[] expected = 
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9,
            };

            int[] actual = Sequence.Interleave(allSources)
                                   .ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}