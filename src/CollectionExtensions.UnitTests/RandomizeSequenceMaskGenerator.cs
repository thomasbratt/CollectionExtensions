using System;
using System.Diagnostics;
using NUnit.Framework;

namespace CollectionExtensions.UnitTests
{
    /// <summary>
    /// Not a true test - just a convenient place to store the mask generation code.
    /// </summary>
    [TestFixture]
    internal class RandomizeSequenceMaskGenerator
    {
        [Test]
        public void Generate()
        {
            Trace.WriteLine("" + TermsToMask(2, 1)        + "    //  2 bit mask.");
            Trace.WriteLine("" + TermsToMask(3, 2)        + "    //  3 bit mask.");
            Trace.WriteLine("" + TermsToMask(4, 3)        + "    //  4 bit mask.");
            Trace.WriteLine("" + TermsToMask(5, 3)        + "    //  5 bit mask.");
            Trace.WriteLine("" + TermsToMask(6, 5)        + "    //  6 bit mask.");
            Trace.WriteLine("" + TermsToMask(7, 6)        + "    //  7 bit mask.");
            Trace.WriteLine("" + TermsToMask(8, 6, 5, 4)  + "    //  8 bit mask.");
            Trace.WriteLine("" + TermsToMask(9, 5)        + "    //  9 bit mask.");
            Trace.WriteLine("" + TermsToMask(10, 7)           + "    // 10 bit mask.");
            Trace.WriteLine("" + TermsToMask(11, 9)           + "    // 11 bit mask.");
            Trace.WriteLine("" + TermsToMask(12, 6, 4, 1)     + "    // 12 bit mask.");
            Trace.WriteLine("" + TermsToMask(13, 4, 3, 1)     + "    // 13 bit mask.");
            Trace.WriteLine("" + TermsToMask(14, 5, 3, 1)     + "    // 14 bit mask.");
            Trace.WriteLine("" + TermsToMask(15, 14)          + "    // 15 bit mask.");
            Trace.WriteLine("" + TermsToMask(16, 15, 13, 4)   + "    // 16 bit mask.");

            Trace.WriteLine("" + TermsToMask(17, 14)          + "    // 17 bit mask.");
            Trace.WriteLine("" + TermsToMask(18, 11)          + "    // 18 bit mask.");
            Trace.WriteLine("" + TermsToMask(19, 6, 2, 1)     + "    // 19 bit mask.");
            Trace.WriteLine("" + TermsToMask(20, 17)          + "    // 20 bit mask.");
            Trace.WriteLine("" + TermsToMask(21, 19)          + "    // 21 bit mask.");
            Trace.WriteLine("" + TermsToMask(22, 21)          + "    // 22 bit mask.");
            Trace.WriteLine("" + TermsToMask(23, 18)          + "    // 23 bit mask.");
            Trace.WriteLine("" + TermsToMask(24, 23, 22, 17)  + "    // 24 bit mask.");
            Trace.WriteLine("" + TermsToMask(25, 22)          + "    // 25 bit mask.");
            Trace.WriteLine("" + TermsToMask(26, 6, 2, 1)     + "    // 26 bit mask.");
            Trace.WriteLine("" + TermsToMask(27, 5, 2, 1)     + "    // 27 bit mask.");
            Trace.WriteLine("" + TermsToMask(28, 25)          + "    // 28 bit mask.");
            Trace.WriteLine("" + TermsToMask(29, 27)          + "    // 29 bit mask.");
            Trace.WriteLine("" + TermsToMask(30, 6, 4, 1)     + "    // 30 bit mask.");
            Trace.WriteLine("" + TermsToMask(31, 28)          + "    // 31 bit mask.");
            Trace.WriteLine("" + TermsToMask(32, 22, 2, 1)    + "    // 32 bit mask.");  // ?
        }

        private static string TermsToMask(params int[] allTerms)
        {
            int value = 0;
            foreach (int term in allTerms)
            {
                value |= (1 << term-1);
            }

            return String.Format("0x{0:X8},", value);
        }
    }
}
