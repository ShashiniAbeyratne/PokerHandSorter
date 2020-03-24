using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHandSorter.Domain.Models
{
    public class Hand
    {
        public List<Card> Cards { get; set; }
        public HandRank Rank { get; set; }

        private static readonly int CardCount = 5; 
        private static readonly int OnePair = 1; 
        private static readonly int TwoPairs = 2;

        protected Hand()
        {
            Cards = new List<Card>();
        }

        public Hand(List<string> cards)
        {
            if (cards.Count != CardCount)
                throw new ArgumentException("A hand should consist of 5 cards only! Please provide valid content and try again.");

            Cards = new List<Card>();

            foreach (string card in cards)
                Cards.Add(new Card(card.Trim()));
        }

        public void EvaluateHandRank()
        {
            if (IsSameSuit())
            {
                if (IsRoyalFlush())
                    Rank = HandRank.RoyalFlush;
                else if (IsInConsecutiveOrder())
                    Rank = HandRank.StraightFlush;
                else
                    Rank = HandRank.Flush;
            }
            else
            {
                if (IsFourOfTheSameKind())
                    Rank = HandRank.FourOfAKind;
                else if (IsThreeOfTheSameKind())
                {
                    if (HasPairs(OnePair))
                        Rank = HandRank.FullHouse;
                    else
                        Rank = HandRank.ThreeOfAKind;
                }
                else if (HasPairs(TwoPairs))
                    Rank = HandRank.TwoPairs;
                else if (HasPairs(OnePair))
                    Rank = HandRank.Pair;
                else if (IsInConsecutiveOrder())
                    Rank = HandRank.Straight;
                else
                    Rank = HandRank.HighCard;
            }
        }

        public int GetHighestCard(int level)
        {
            if (level >= Rank.MaxComparisons)
                throw new ArgumentException("End of Hand Reached. No other high card! Please try again with correct level.");

            if (Rank == HandRank.Pair)
                return GetRankPairHighestCard(level);
            else if (Rank == HandRank.TwoPairs)
                return GetRankTwoPairsHighestCard(level);
            else if (Rank == HandRank.ThreeOfAKind)
                return GetThreeOfAKindHighestCard(level);
            else if (Rank == HandRank.FullHouse)
                return GetRankFullHouseHighestCard(level);
            else if (Rank == HandRank.FourOfAKind)
                return GetRankFourOfAkindHighestCard(level);
            else
                return (int) Cards.OrderByDescending(c => c.Value).Skip(level).Max(c => c.Value); 
        }

        private bool IsSameSuit()
        {
            return Cards.GroupBy(c => c.Suit).Count() == 1 ? true : false;
        }

        private bool IsRoyalFlush()
        {
            if (!Cards.Exists(c => c.Value == CardValue.Ten))
                return false;

            if (!Cards.Exists(c => c.Value == CardValue.Jack))
                return false;

            if (!Cards.Exists(c => c.Value == CardValue.Queen))
                return false;

            if (!Cards.Exists(c => c.Value == CardValue.King))
                return false;

            if (!Cards.Exists(c => c.Value == CardValue.Ace))
                return false;

            return true;
        }

        private bool IsInConsecutiveOrder()
        {
            List<Card> reverseCopy = Cards.ToList();
            reverseCopy.Reverse();

            if (!Cards.Select((card, index) => card.Value - index).Distinct().Skip(1).Any() || !reverseCopy.Select((card, index) => card.Value - index).Distinct().Skip(1).Any())
                return true;
            else
                return false;
        }

        private bool IsFourOfTheSameKind()
        {
            return Cards.GroupBy(c => c.Value).Any(g => g.Count() == 4);
        }

        private bool IsThreeOfTheSameKind()
        {
            return Cards.GroupBy(c => c.Value).Any(g => g.Count() == 3);
        }

        private bool HasPairs(int pairCount)
        {
            return Cards.GroupBy(c => c.Value).Where(g => g.Count() == 2).Count() == pairCount ? true : false;
        }

        private int GetRankPairHighestCard(int level)
        {
            if (level == 0)
                return (int)Cards.GroupBy(c => c.Value).Where(g => g.Count() == 2).First().Key;
            else
            {
                return (int)Cards.OrderByDescending(c => c.Value).GroupBy(c => c.Value).Where(g => g.Count() != 2).Skip(level - 1).Max(c => c.Key);
            }
        }

        private int GetRankTwoPairsHighestCard(int level)
        {
            if (level == 0)
                return (int)Cards.OrderByDescending(c => c.Value).GroupBy(c => c.Value).Where(g => g.Count() == 2).First().Key;
            else if (level == 1)
                return (int)Cards.OrderByDescending(c => c.Value).GroupBy(c => c.Value).Where(g => g.Count() == 2).Skip(1).First().Key;
            else
                return (int)Cards.OrderByDescending(c => c.Value).GroupBy(c => c.Value).Where(g => g.Count() != 2).Skip(level - 2).Max(c => c.Key);
        }

        private int GetThreeOfAKindHighestCard(int level)
        {
            if (level == 0)
                return (int)Cards.GroupBy(c => c.Value).Where(g => g.Count() == 3).First().Key;
            else
                return (int)Cards.OrderByDescending(c => c.Value).GroupBy(c => c.Value).Where(g => g.Count() != 3).Skip(level - 1).Max(c => c.Key);
        }

        private int GetRankFullHouseHighestCard(int level)
        {
            if (level == 0)
                return (int)Cards.GroupBy(c => c.Value).Where(g => g.Count() == 3).First().Key;
            else if (level == 1)
                return (int)Cards.GroupBy(c => c.Value).Where(g => g.Count() == 2).First().Key;
            else
                return (int)Cards.OrderByDescending(c => c.Value).GroupBy(c => c.Value).Where(g => g.Count() != 3 && g.Count() != 2).Skip(level - 2).Max(c => c.Key);
        }

        private int GetRankFourOfAkindHighestCard(int level)
        {
            if (level == 0)
                return (int)Cards.GroupBy(c => c.Value).Where(g => g.Count() == 4).First().Key;
            else
                return (int)Cards.GroupBy(c => c.Value).Where(g => g.Count() != 4).Max(c => c.Key);
        }
    }
}
