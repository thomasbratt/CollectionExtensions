using System;
using System.Linq;
using NUnit.Framework;

namespace CollectionExtensions.UnitTests
{
    [TestFixture]
    public class ShuffleTest
    {
        /// <summary>
        /// Simple test that ensures a collection can be shuffled.
        /// </summary>
        [Test]
        public void SmokeTestForShuffle()
        {
            // Arrange.
            int[] data = { 1, 2, 3, 4, 5 };
            int[] expected = { 1, 2, 3, 4, 5 };
            var random = new Random(9278);

            // Act.
            int[] actual = data.Shuffle(random)
                               .ToArray();

            // Assert.
            CollectionAssert.AreEquivalent(expected, actual);
            CollectionAssert.AreNotEqual(expected, actual);
        }

        /// <summary>
        /// Simple test that ensures a collection can be shuffled in place (without creating a copy).
        /// </summary>
        [Test]
        public void SmokeTestForShuffleInPlace()
        {
            // Arrange.
            int[] data = { 1, 2, 3, 4, 5 };
            int[] expected = { 1, 2, 3, 4, 5 };
            var random = new Random(9278);

            // Act.
            data.ShuffleInPlace(random);

            // Assert.
            CollectionAssert.AreEquivalent(data, expected);
            CollectionAssert.AreNotEqual(data, expected);
        }

        /// <summary>
        /// Simple test that ensures a collection can be shuffled when a source
        /// of randomness is requested but not explicitly provided.
        /// </summary>
        /// <remarks>
        /// May fail intermittently as the source of randomness may generate a
        /// sequence that matches the original.
        /// </remarks>
        [Test]
        public void SmokeTestForShuffleWithImplictRandomSeedSource()
        {
            // Arrange.
            int[] data = { 1, 2, 3, 4, 5 };
            int[] expected = { 1, 2, 3, 4, 5 };

            // Act.
            int[] actual = data.Shuffle(null)
                               .ToArray();

            // Assert.
            CollectionAssert.AreEquivalent(expected, actual);
            CollectionAssert.AreNotEqual(expected, actual);
        }
    }
}
