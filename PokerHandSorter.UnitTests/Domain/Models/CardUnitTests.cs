using PokerHandSorter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PokerHandSorter.UnitTests.Domain.Models
{
    public class CardUnitTests
    {
        [Fact]
        public void CreateNewCard_PassAceOfSpades_IsSuccess()
        {
            string card = "AS";

            var cardObject = new Card(card);

            Assert.Equal(CardValue.Ace, cardObject.Value);
            Assert.Equal(CardSuit.Spades, cardObject.Suit);
        }

        [Theory]
        [InlineData("4HC")]
        [InlineData("12R")]
        [InlineData("10D")]
        [InlineData("128FHFD")]
        [InlineData("2DC")]
        public void CreateNewCard_PassCardStringWhichIsNotTwoCharacters_ThrowArgumentException(string card)
        {
            var exception = Assert.Throws<ArgumentException>(() => new Card(card));
            Assert.Equal("Invalid Card!", exception.Message);
        }

        [Theory]
        [InlineData("1C")]
        [InlineData("1B")]
        [InlineData("0D")]
        public void CreateNewCard_PassInvalidCardValue_ThrowArgumentException(string card)
        {
            var exception = Assert.Throws<ArgumentException>(() => new Card(card));
            Assert.Equal("Invalid Card Value! Please provide valid content and try again.", exception.Message);
        }

        [Theory]
        [InlineData("3V")]
        [InlineData("TJ")]
        [InlineData("AX")]
        public void CreateNewCard_PassInvalidCardSuit_ThrowArgumentException(string card)
        {
            var exception = Assert.Throws<ArgumentException>(() => new Card(card));
            Assert.Equal("Invalid Card Suit! Please provide valid content and try again.", exception.Message);
        }
    }
}
