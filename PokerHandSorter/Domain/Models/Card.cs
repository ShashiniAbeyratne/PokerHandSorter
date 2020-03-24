using System;

namespace PokerHandSorter.Domain.Models
{
    public class Card
    {
        public CardValue Value { get; set; }
        public CardSuit Suit { get; set; }

        public Card(string card)
        {
            if (card.Length != 2)
                throw new ArgumentException("Invalid Card!");

            Value = getCardValue(card.ToCharArray()[0]);
            Suit = getCardSuit(card.ToCharArray()[1]);
        }

        private CardValue getCardValue(char value)
        {
            return _ = (value) switch
            {
                '2' => CardValue.Two,
                '3' => CardValue.Three,
                '4' => CardValue.Four,
                '5' => CardValue.Five,
                '6' => CardValue.Six,
                '7' => CardValue.Seven,
                '8' => CardValue.Eight,
                '9' => CardValue.Nine,
                'T' => CardValue.Ten,
                'J' => CardValue.Jack,
                'K' => CardValue.King,
                'Q' => CardValue.Queen,
                'A' => CardValue.Ace,
                _ => throw new ArgumentException("Invalid Card Value! Please provide valid content and try again.")
            };
        }

        private CardSuit getCardSuit(char suit)
        {
            return _ = (suit) switch
            {
                'D' => CardSuit.Diamonds,
                'H' => CardSuit.Hearts,
                'S' => CardSuit.Spades,
                'C' => CardSuit.Clubs,
                _ => throw new ArgumentException("Invalid Card Suit! Please provide valid content and try again.")
            };
        }
    }
}
