using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using PokerObjects;
using System.Collections.Generic;

namespace CombinationsComparer.Test
{
    [TestClass]
    public class CombinationsComparerTest
    {
        public Card GetCardFromStrings(string cardData)
        {
            return new Card(cardData[0], int.Parse(cardData.Substring(1)));
        }
        public void ApplyTest(string[] first, string[] second, string[] table, int expectedResult)
        {
            var firstCards = first.Select(GetCardFromStrings).ToArray();
            var secondCards = second.Select(GetCardFromStrings).ToArray();
            var onTable = table.Select(GetCardFromStrings).ToArray();
            var actualResult = CombinationsComparer.CompareCombinations(firstCards, secondCards, onTable);
            if (expectedResult == 0)
            {
                Assert.AreEqual(0, actualResult);
            }
            else
            {
                Assert.AreEqual(expectedResult < 0, actualResult < 0);
            }
        }
        [TestMethod]
        public void TestHighVSHigh()
        {
            ApplyTest(
                new[] { "A12", "B1" },
                new[] { "A11", "B2" },
                new[] { "C4", "C5", "D7", "D8", "C9" },
                1
            );
            ApplyTest(
                new[] { "B12", "B2" },
                new[] { "A12", "B3" },
                new[] { "C10", "C5", "D7", "D8", "C9" },
                0
            );
        }
        [TestMethod]
        public void TestPairVSPair()
        {
            ApplyTest(
                new[] { "A12", "B5" },
                new[] { "A11", "B7" },
                new[] { "C4", "C5", "D7", "D8", "C9" },
                -1
            );
            ApplyTest(
                new[] { "B11", "B8" },
                new[] { "A11", "C8" },
                new[] { "C4", "C5", "D7", "D8", "C9" },
                0
            );
            ApplyTest(
                new[] { "B11", "B8" },
                new[] { "A10", "C8" },
                new[] { "C4", "C5", "D7", "D8", "C9" },
                1
            );
        }
        [TestMethod]
        public void TestStragihts()
        {
            ApplyTest(
                new[] { "B11", "B12" },
                new[] { "A10", "A6" },
                new[] { "C5", "C6", "D7", "D8", "C9" },
                -1
            );
            ApplyTest(
                new[] { "B11", "B7" },
                new[] { "A10", "C8" },
                new[] { "C4", "C5", "D7", "D8", "C6" },
                0               
            );
            ApplyTest(
                new[] { "A12", "B10" },
                new[] { "A11", "B5" },
                new[] { "C4", "C5", "D3", "D2", "C9" },
                1
            );
        }
        [TestMethod]
        public void TestFlush()
        {
            ApplyTest(
                new[] { "A3", "A4" },
                new[] { "A5", "B6" },
                new[] { "A14", "A13", "A12", "A11", "C9" },
                -1
            );

            ApplyTest(
                new[] { "A3", "A4" },
                new[] { "B10", "B6" },
                new[] { "A14", "A13", "A12", "A11", "C9" },
                1
            );

            //Not here - Straight Flush
            ApplyTest(
                new[] { "A10", "D4" },
                new[] { "B10", "A6" },
                new[] { "A14", "A13", "A12", "A11", "C9" },
                1
            );

            ApplyTest(
                new[] { "B14", "D4" },
                new[] { "B10", "A6" },
                new[] { "C14", "C13", "C12", "C11", "C9" },
                0
            );
            //Not here - Full House
            ApplyTest(
                new[] { "B14", "D12" },
                new[] { "C3", "C6" },
                new[] { "C14", "C13", "C12", "C11", "A12" },
                1
            );
        }
    }
}
