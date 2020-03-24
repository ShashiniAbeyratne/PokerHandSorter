using PokerHandSorter.Application.Validations;
using Xunit;

namespace PokerHandSorter.UnitTests.Application.Validations
{
    public class PokerHandsValidatorTests
    {
        [Fact]
        public void ValidatePalyerHandSet_PassValidHand_ReturnTrue()
        {
            var playerHand = "2D 4C 5D 8C 8H TS JS KC 4H 2H";

            var result = new PokerHandsValidator().ValidatePlayerHand(playerHand);

            Assert.True(result);
        }

        [Theory]
        [InlineData("10D 4C 5D 8C 8H TS JS KC 4H 2H")]
        [InlineData("10D 4C 5D 8C 8H TS JS KC 4H 2H 7D")]
        [InlineData("BD 4C 5D 8C 8H TS JS XC 4H 2H")]
        [InlineData("4D 4C 5D 8C 8K TS JS KC 4N 2H")]
        [InlineData("10D 4XS 5D 8CD 8H TS JS KC 4H 2FD 5G")]
        public void ValidatePalyerHandSet_PassInValidHand_ReturnFalse(string playerHand)
        {
            var result = new PokerHandsValidator().ValidatePlayerHand(playerHand);

            Assert.False(result);
        }
    }
}
