using System;
using System.Collections.Generic;

namespace CollectionExtensions
{
    public static partial class Sequence
    {
        /// <summary>
        /// Generates a pseudo-random sequence using a Galois Linear Feedback
        /// Shift Register.
        /// The sequence will contain each element exactly once.
        /// </summary>
        /// <param name="length">
        /// The length of the sequence to generate.
        /// </param>
        public static IEnumerable<int> Randomize(int length)
        {
            Sequence.CheckParameters(length, seed:0);

            return Sequence.UncheckedRandomize(length, seed:0);
        }

        /// <summary>
        /// Generates a pseudo-random sequence using a Galois Linear Feedback
        /// Shift Register.
        /// The sequence will contain each element exactly once.
        /// The first item in the sequence will be chosen pseudo-randomly.
        /// </summary>
        /// <param name="length">
        /// The length of the sequence to generate.
        /// </param>
        /// <param name="random">
        /// A source of randomness used to generate the first item (only) in the sequence.
        /// </param>
        public static IEnumerable<int> Randomize(int length, Random random = null)
        {
            if (random == null)
            {
                random = new Random();
            }

            int seed = random.Next(length);

            Sequence.CheckParameters(length, seed);

            return Sequence.UncheckedRandomize(length, seed);
        }

        /// <summary>
        /// Generates a pseudo-random sequence using a Galois Linear Feedback
        /// Shift Register.
        /// The sequence will contain each element exactly once.
        /// </summary>
        /// <param name="length">
        /// The length of the sequence to generate.
        /// </param>
        /// <param name="seed">
        /// The first value in the sequence. Must be less than length.
        /// </param>
        public static IEnumerable<int> Randomize(int length, int seed)
        {
            Sequence.CheckParameters(length, seed);

            return Sequence.UncheckedRandomize(length, seed);
        }

        // The core implementation.
        private static IEnumerable<int> UncheckedRandomize(int length, int seed)
        {
            // Special case handling for length 0.
            if (length == 0)
            {
                yield break;
            }

            // Special case handling for length 1.
            if (length == 1)
            {
                yield return 0;
                yield break;
            }

            // Special case handling for length 2.
            if (length == 2)
            {
                yield return 1;
                yield return 0;
                yield break;
            }

            // Find nearest power of 2 greater than or equal to length.
            int bits = Sequence.RoundToPowerOfTwo(length);

            // Lookup polynomial coefficients.
            // Lookup table starts at 2 bits.
            UInt32 mask = Sequence.MaskLookup[bits-2]; 

            // Start the sequence at the desired value.
            yield return seed;

            // Generate successive values in the sequence.
            // The algorithm generated sequence cannot contain zero,
            // so the method uses a sequence starting at 1 and subtracts 1
            // when returning the results, so that the resulting sequence
            // does start at zero.
            UInt32 current = (UInt32)seed + 1;
            while(true)
            {
                current = (current >> 1)
                        ^ (UInt32)(-(current & 1u) & mask);

                // Stop when the start of the sequence is reached.
                if (current == (seed+1))
                {
                    yield break;
                }

                // Skip values greater than the length required.
                if (current > length)
                {
                    continue;
                }

                // Return value -1, so that the returned sequence contains zero.
                yield return (int)current - 1;
            }
        }

        // Find nearest power of 2 greater than length.
        private static int RoundToPowerOfTwo(int value)
        {
            int bits = 1;

            while ((value >>= 1) > 0)
            {
                bits++;
            }

            return bits;
        }

        private static void CheckParameters(int length, int seed)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", "Length must not be negative.XX");
            }

            if(seed < 0)
            {
                throw new ArgumentOutOfRangeException("seed", "Seed must not be negative.");
            }

            // The only case when the seed and length can both be the same.
            if (    seed == 0
                &&  length == 0)
            {
                return;
            }

            if (seed >= length)
            {
                throw new ArgumentOutOfRangeException(  "seed",
                                                        "Seed position in sequence must be less than the length.");
            }
        }

        // Sequence length for a given number of bits (N) is (2^N)-1.

        private readonly static UInt32[] MaskLookup =
        {
            0x00000003,    //  2 bit mask, sequence length 3.
            0x00000006,    //  3 bit mask, sequence length 7.
            0x0000000C,    //  4 bit mask, sequence length 15.
            0x00000014,    //  5 bit mask.
            0x00000030,    //  6 bit mask.
            0x00000060,    //  7 bit mask.
            0x000000B8,    //  8 bit mask.
            0x00000110,    //  9 bit mask.
            0x00000240,    // 10 bit mask.
            0x00000500,    // 11 bit mask.
            0x00000829,    // 12 bit mask.
            0x0000100D,    // 13 bit mask.
            0x00002015,    // 14 bit mask.
            0x00006000,    // 15 bit mask.
            0x0000D008,    // 16 bit mask.
            0x00012000,    // 17 bit mask.
            0x00020400,    // 18 bit mask.
            0x00040023,    // 19 bit mask.
            0x00090000,    // 20 bit mask.
            0x00140000,    // 21 bit mask.
            0x00300000,    // 22 bit mask.
            0x00420000,    // 23 bit mask.
            0x00E10000,    // 24 bit mask.
            0x01200000,    // 25 bit mask.
            0x02000023,    // 26 bit mask.
            0x04000013,    // 27 bit mask.
            0x09000000,    // 28 bit mask.
            0x14000000,    // 29 bit mask.
            0x20000029,    // 30 bit mask.
            0x48000000,    // 31 bit mask.
            0x80200003,    // 32 bit mask.
        };
    }
}
