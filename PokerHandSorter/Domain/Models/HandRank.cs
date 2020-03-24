using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHandSorter.Domain.Models
{
    public class HandRank
    {
        public static HandRank HighCard = new HandRank(0, "HighCard", 5);
        public static HandRank Pair = new HandRank(1, "Pair", 4);
        public static HandRank TwoPairs = new HandRank(2, "TwoPairs", 3);
        public static HandRank ThreeOfAKind = new HandRank(3, "ThreeOfAKind", 3);
        public static HandRank Straight = new HandRank(4, "Straight", 5);
        public static HandRank Flush = new HandRank(5, "Flush", 5);
        public static HandRank FullHouse = new HandRank(6, "FullHouse", 2);
        public static HandRank FourOfAKind = new HandRank(7, "FourOfAKind", 2);
        public static HandRank StraightFlush = new HandRank(8, "StraightFlush", 5);
        public static HandRank RoyalFlush = new HandRank(9, "RoyalFlush", 1);

        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxComparisons { get; set; }

        protected HandRank() { }

        public HandRank(int id, string name, int maxCamparisons)
        {
            Id = id;
            Name = name;
            MaxComparisons = maxCamparisons;
        }
    }
}
