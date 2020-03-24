using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHandSorter.Application.Validations
{
    public interface IPokerHandsValidator
    {
        bool ValidatePlayerHand(string playerHands);
    }
}
