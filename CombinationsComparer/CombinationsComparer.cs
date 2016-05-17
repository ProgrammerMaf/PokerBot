using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerObjects;

namespace CombinationsComparer
{
    public static class CombinationsDetector
    {
        static int[] IndexArray = new[] { 0, 1, 2, 3, 4, 5, 6 };
        static Card[] SelectCards(Card[] sortedCards, params int[] selected)
        {
            var unselected = IndexArray.Where(index => !selected.Contains(index));
            var unselectedCount = 5 - selected.Length;
            var selectRequiredCards = selected.Select(index => sortedCards[index]);
            var selectOtherCards = unselected.Select(index => sortedCards[index]);
            return selectRequiredCards
                .Concat(
                    selectOtherCards
                        .OrderByDescending(e => e.Rank)
                        .Take(unselectedCount)
                )
                .ToArray();
        }
        public static void ChoiseElements(int start, int n, int remained, List<int[]> result, List<int> current)
        {
            if (remained == 0)
            {
                result.Add(current.ToArray());
                return;
            }
            for (int i = start + 1; i < n; i++)
            {
                current.Add(i);
                ChoiseElements(i, n, remained - 1, result, current);
                current.RemoveAt(current.Count - 1);
            }
        }
        public static List<int[]> IntCombinations(int n, int k)
        {
            var result = new List<int[]>();
            var current = new List<int>(4);
            ChoiseElements(-1, n, k, result, current);
            return result;
        }
        static Card[] IsCorrectCombination(Card[] cards, int choisenCount, Func<Card[], Card[]> SortCards, Func<Card[], bool> CheckCombination)
        {
            var sortedCards = SortCards(cards);
            foreach (var indices in IntCombinations(7, choisenCount))
            {
                var choisenCards = indices.Select(index => sortedCards[index]).ToArray();
                if (CheckCombination(choisenCards))
                {
                    return SelectCards(sortedCards, indices);
                }
            }
            return null;
        }
        private static bool isStraight(Card[] cards)
        {
            for (int i = 1; i < 5; i++)
            {
                if (cards[i].Rank != cards[i - 1].Rank - 1)
                    return false;
            }
            return true;
        }
        private static bool isFlush(Card[] cards)
        {
            for (int i = 1; i < 5; i++)
            {
                if (cards[i].Suit != cards[i - 1].Suit)
                    return false;
            }
            return true;
        }
        public static Card[] RoyalFlush(Card[] hand, Card[] onTable)
        {
            return IsCorrectCombination(
                hand.Concat(onTable).ToArray(),
                5,
                cards => cards.OrderByDescending(e => e.Rank).ToArray(),
                cards => isStraight(cards) && isFlush(cards) && cards[0].Rank == Card.MaxRank
            );
        }
        public static Card[] StraightFlush(Card[] hand, Card[] onTable)
        {
            return IsCorrectCombination(
                hand.Concat(onTable).ToArray(),
                5,
                cards => cards.OrderByDescending(e => e.Rank).ToArray(),
                cards => isStraight(cards) && isFlush(cards)
            );
        }
        public static Card[] Quads(Card[] hand, Card[] onTable)
        {
            return IsCorrectCombination(
                hand.Concat(onTable).ToArray(),
                4,
                cards => cards.OrderByDescending(e => e.Rank).ToArray(),
                cards => cards[0].Rank == cards[1].Rank && 
                         cards[1].Rank == cards[2].Rank &&
                         cards[2].Rank == cards[3].Rank
            );
        }
        public static Card[] FullHouse(Card[] hand, Card[] onTable)
        {
            var sortedCards = hand.Concat(onTable).OrderByDescending(e => e.Rank).ToArray();
            foreach (var indices in IntCombinations(7, 5))
            {
                var choisenCards = indices.Select(index => sortedCards[index]).ToArray();
                if (choisenCards[0].Rank == choisenCards[1].Rank &&
                    choisenCards[1].Rank == choisenCards[2].Rank &&
                    choisenCards[3].Rank == choisenCards[4].Rank
                    )
                    return SelectCards(sortedCards, indices);
                if (choisenCards[0].Rank == choisenCards[1].Rank &&
                    choisenCards[2].Rank == choisenCards[3].Rank &&
                    choisenCards[3].Rank == choisenCards[4].Rank
                    )
                    return SelectCards(sortedCards, indices[2], indices[3], indices[4], indices[0], indices[1]);
            }
            return null;
        }
        public static Card[] Flush(Card[] hand, Card[] onTable)
        {
            return IsCorrectCombination(
                hand.Concat(onTable).ToArray(),
                5,
                cards => cards.OrderByDescending(e => e.Rank).ToArray(),
                cards => isFlush(cards)
            );
        }
        public static Card[] Straight(Card[] hand, Card[] onTable)
        {
            return IsCorrectCombination(
                hand.Concat(onTable).ToArray(),
                5,
                cards => cards.OrderByDescending(e => e.Rank).ToArray(),
                cards => isStraight(cards)
            );
        }
        public static Card[] Set(Card[] hand, Card[] onTable)
        {
            return IsCorrectCombination(
                hand.Concat(onTable).ToArray(),
                3,
                cards => cards.OrderByDescending(e => e.Rank).ToArray(),
                cards => cards[0].Rank == cards[1].Rank && cards[1].Rank == cards[2].Rank
            );
        }
        public static Card[] TwoPairs(Card[] hand, Card[] onTable)
        {
            return IsCorrectCombination(
                hand.Concat(onTable).ToArray(),
                4,
                cards => cards.OrderByDescending(e => e.Rank).ToArray(),
                cards => cards[0].Rank == cards[1].Rank && cards[2].Rank == cards[3].Rank
            );
        }
        public static Card[] Pair(Card[] hand, Card[] onTable)
        {
            return IsCorrectCombination(
                hand.Concat(onTable).ToArray(),
                2,
                cards => cards.OrderByDescending(e => e.Rank).ToArray(),
                cards => cards[0].Rank == cards[1].Rank
            );
        }
        public static Card[] HighCard(Card[] hand, Card[] onTable)
        {
            return hand.Concat(onTable).OrderByDescending(e => e.Rank).Take(5).ToArray();
        }

    }
    public static class CombinationsComparer
    {
        static Func<Card[], Card[], Card[]>[] CombinationDetectors;
        static CombinationsComparer()
        {
            CombinationDetectors = new Func<Card[], Card[], Card[]>[]
            {
                CombinationsDetector.RoyalFlush,
                CombinationsDetector.StraightFlush,
                CombinationsDetector.Quads,
                CombinationsDetector.FullHouse,
                CombinationsDetector.Flush,
                CombinationsDetector.Straight,
                CombinationsDetector.Set,
                CombinationsDetector.TwoPairs,
                CombinationsDetector.Pair,
                CombinationsDetector.HighCard
            };
        }
        static int CompareEqualCombinations(Card[] first, Card[] second)
        {
            for (int i = 0; i < 5; i++)
            {
                if (first[i].Rank != second[i].Rank)
                    return first[i].Rank - second[i].Rank;
            }
            return 0;
        }
        public static int CompareCombinations(Card[] firstPlayerHand, Card[] secondPlayerHand, Card[] onTable)
        {
            //> 0, if first player wins
            //< 0, if second player wins
            //= 0, otherwise
            foreach (var GetCombination in CombinationDetectors)
            {
                var firstPlayerCombination = GetCombination(firstPlayerHand, onTable);
                var secondPlayerCombination = GetCombination(secondPlayerHand, onTable);
                if (firstPlayerCombination != null || secondPlayerCombination != null)
                {
                    if (firstPlayerCombination != null && secondPlayerCombination != null)
                    {
                        var comparsion = CompareEqualCombinations(firstPlayerCombination, secondPlayerCombination);
                        return comparsion;
                    }
                    else if (firstPlayerCombination == null)
                    {
                        return -1;
                    }
                    else if (secondPlayerCombination == null)
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }
    }
}
