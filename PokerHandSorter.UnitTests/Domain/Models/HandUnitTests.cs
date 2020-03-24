using PokerHandSorter.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace PokerHandSorter.UnitTests.Domain.Models
{
    public class HandUnitTests
    {
        [Theory]
        [ClassData(typeof(TestHandRankEvaluateDataGenerator))]
        public void EvaluateHandRank_PassValidHand_GetCorrectRank(List<string> cards, HandRank expected)
        {
            var handObject = new Hand(cards);
            handObject.EvaluateHandRank();

            Assert.Equal(expected, handObject.Rank);
        }

        [Theory]
        [ClassData(typeof(TestHandDataGenerator))]
        public void EvaluateHandRank_PassHandWithInvalidNoOfCards_ThrowArgumentException(List<string> cards)
        {
            var exception = Assert.Throws<ArgumentException>(() => new Hand(cards));
            Assert.Equal("A hand should consist of 5 cards only! Please provide valid content and try again.", exception.Message);
        }

        [Fact]
        public void EvaluateHandRank_PassValidHand_IsSuccess()
        {
            var cards = new List<string> { "2C", "AD", "QS", "TS", "3D" };

            var handObject = new Hand(cards);

            Assert.Equal(5, handObject.Cards.Count);
        }

        [Theory]
        [ClassData(typeof(TestGetHighestCardDataGenerator))]
        public void GetHighestCard_PassValidHandAndLevel_IsSuccess(List<string> cards, int level, int expected)
        {
            var handObject = new Hand(cards);
  
            handObject.EvaluateHandRank();
            int highestCard = handObject.GetHighestCard(level);

            Assert.Equal(expected, highestCard);
        }

        [Fact]
        public void GetHighestCard_PassValidHandWithInvalidLevel_ThrowException()
        {
            List<string> cards = new List<string> { "8H", "8S", "8C", "8D", "3C" };
            var handObject = new Hand(cards);

            handObject.EvaluateHandRank();
            
            var exception = Assert.Throws<ArgumentException>(() => handObject.GetHighestCard(3));
            Assert.Equal("End of Hand Reached. No other high card! Please try again with correct level.", exception.Message);
        }
    }

    public class TestHandRankEvaluateDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] { new List<string> { "AC", "7H", "3D", "4D", "JH" }, HandRank.HighCard },
            new object[] { new List<string> { "9C", "6D", "9H", "2S", "QH" }, HandRank.Pair },
            new object[] { new List<string> { "6D", "KD", "6S", "AC", "KH" }, HandRank.TwoPairs },
            new object[] { new List<string> { "2C", "7H", "2D", "2S", "AD" }, HandRank.ThreeOfAKind },
            new object[] { new List<string> { "8H", "9D", "TD", "JC", "QS" }, HandRank.Straight },
            new object[] { new List<string> { "QH", "JD", "TD", "9C", "8S" }, HandRank.Straight },
            new object[] { new List<string> { "2S", "QS", "7S", "KS", "5S" }, HandRank.Flush },
            new object[] { new List<string> { "8C", "AC", "8H", "AS", "AD" }, HandRank.FullHouse },
            new object[] { new List<string> { "AC", "AD", "AH", "AS", "7H" }, HandRank.FourOfAKind },
            new object[] { new List<string> { "3C", "4C", "5C", "6C", "7C" }, HandRank.StraightFlush },
            new object[] { new List<string> { "7C", "6C", "5C", "4C", "3C" }, HandRank.StraightFlush },
            new object[] { new List<string> { "TD", "JD", "QD", "KD", "AD" }, HandRank.RoyalFlush }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class TestHandDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] { new List<string> { "2C", "3C", "4C", "5C", "5D", "6C" } },
            new object[] { new List<string> { "2C", "3C", "4C", "5C" } }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class TestGetHighestCardDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] { new List<string> { "2C", "AD", "QS", "TS", "3D" }, 0, (int) CardValue.Ace },
            new object[] { new List<string> { "2C", "AD", "QS", "TS", "3D" }, 1, (int) CardValue.Queen },
            new object[] { new List<string> { "2C", "AD", "QS", "TS", "3D" }, 2, (int) CardValue.Ten },
            new object[] { new List<string> { "2C", "AD", "QS", "TS", "3D" }, 3, (int) CardValue.Three },
            new object[] { new List<string> { "2C", "AD", "QS", "TS", "3D" }, 4, (int) CardValue.Two },

            new object[] { new List<string> { "2C", "4D", "2S", "KD", "AH" }, 0, (int) CardValue.Two },
            new object[] { new List<string> { "2C", "4D", "2S", "KD", "AH" }, 1, (int) CardValue.Ace },
            new object[] { new List<string> { "2C", "4D", "2S", "KD", "AH" }, 2, (int) CardValue.King },
            new object[] { new List<string> { "2C", "4D", "2S", "KD", "AH" }, 3, (int) CardValue.Four },

            new object[] { new List<string> { "2C", "4D", "2S", "7S", "4H" }, 0, (int) CardValue.Four },
            new object[] { new List<string> { "2C", "4D", "2S", "7S", "4H" }, 1, (int) CardValue.Two },
            new object[] { new List<string> { "2C", "4D", "2S", "7S", "4H" }, 2, (int) CardValue.Seven },

            new object[] { new List<string> { "KC", "KD", "KS", "7S", "4H" }, 0, (int) CardValue.King },
            new object[] { new List<string> { "KC", "KD", "KS", "7S", "4H" }, 1, (int) CardValue.Seven },
            new object[] { new List<string> { "KC", "KD", "KS", "7S", "4H" }, 2, (int) CardValue.Four },

            new object[] { new List<string> { "5H", "5S", "5C", "TH", "TC" }, 0, (int) CardValue.Five },
            new object[] { new List<string> { "5H", "5S", "5C", "TH", "TC" }, 1, (int) CardValue.Ten },

            new object[] { new List<string> { "8H", "8S", "8C", "8D", "3C" }, 0, (int) CardValue.Eight },
            new object[] { new List<string> { "8H", "8S", "8C", "8D", "3C" }, 1, (int) CardValue.Three }

        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
