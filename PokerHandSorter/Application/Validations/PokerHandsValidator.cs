using System.Text.RegularExpressions;

namespace PokerHandSorter.Application.Validations
{
    public class PokerHandsValidator : IPokerHandsValidator
    {
        public bool ValidatePlayerHand(string playerHands)
        {
            return isValidFormatAndLength(playerHands);
        }

        private bool isValidFormatAndLength(string playerHands)
        {
            return Regex.IsMatch(playerHands, @"^\s*([2-9,T,t,A,a,J,j,K,k,Q,q][D,d,C,c,S,s,H,h]\s+){9}([2-9,T,t,A,a,J,j,K,k,Q,q][D,d,C,c,S,s,H,h]\s*)$");
        }
    }
}
