using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerObjects;

namespace CombinationsComparer.Test
{
    [TestClass]
    public class CombinationsComparerTest
    {
        public void ApplyTest(string[] first, string[] second, string[] table, int expectedResult)
        {
            var firstCards = first.Select(e => new Card(e[0], int.Parse(e.Substring(1)))).ToArray();
            var secondCards = second.Select(e => new Card(e[0], int.Parse(e.Substring(1)))).ToArray();
            var onTable = table.Select(e => new Card(e[0], int.Parse(e.Substring(1)))).ToArray();
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
                new[] { "B11", "B1" },
                new[] { "A11", "B2" },
                new[] { "C4", "C5", "D7", "D8", "C9" },
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
    }
}
