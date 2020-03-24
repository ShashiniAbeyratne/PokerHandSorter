using Moq;
using PokerHandSorter.Application.Evaluator;
using PokerHandSorter.Application.Validations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PokerHandSorter.UnitTests.Application.Evaluator
{
    public class PokerHandEvaluatorTests
    {
        private readonly Mock<IPokerHandsValidator> _pokerHandsValidator;

        public PokerHandEvaluatorTests()
        {
            _pokerHandsValidator = new Mock<IPokerHandsValidator>();
        }

        [Fact]
        public void EvaluatePlayerHands_WhenValid_GeneratesValidResultDictionary()
        {
            _pokerHandsValidator.Setup(x => x.ValidatePlayerHand(It.IsAny<string>())).Returns(true);

            var evaluator = new PokerHandEvaluator(_pokerHandsValidator.Object);
            var result = evaluator.Evaluate(new List<string> { });

            Assert.Contains(1, result.Keys);
            Assert.Contains(2, result.Keys);
            Assert.Contains(3, result.Keys);
            Assert.Equal(3, result.Keys.Count);
        }

        [Theory]
        [ClassData(typeof(TestEvaluatePlayerOneDataGenerator))]
        public void EvaluatePlayerHands_WhenValid_PlayerOneWins(List<string> playerHandsList)
        {
            _pokerHandsValidator.Setup(x => x.ValidatePlayerHand(It.IsAny<string>())).Returns(true);

            var evaluator = new PokerHandEvaluator(_pokerHandsValidator.Object);
            var result = evaluator.Evaluate(playerHandsList);

            Assert.Equal(1, result[1]);
        }

        [Theory]
        [ClassData(typeof(TestEvaluatePlayerTwoDataGenerator))]
        public void EvaluatePlayerHands_WhenValid_PlayerTwoWins(List<string> playerHandsList)
        {
            _pokerHandsValidator.Setup(x => x.ValidatePlayerHand(It.IsAny<string>())).Returns(true);

            var evaluator = new PokerHandEvaluator(_pokerHandsValidator.Object);
            var result = evaluator.Evaluate(playerHandsList);

            Assert.Equal(1, result[2]);
        }

        [Theory]
        [ClassData(typeof(TestEvaluateTiesDataGenerator))]
        public void EvaluatePlayerHands_WhenValid_Tie(List<string> playerHandsList)
        {
            _pokerHandsValidator.Setup(x => x.ValidatePlayerHand(It.IsAny<string>())).Returns(true);

            var evaluator = new PokerHandEvaluator(_pokerHandsValidator.Object);
            var result = evaluator.Evaluate(playerHandsList);

            Assert.Equal(1, result[3]);
        }

        [Fact]
        public void EvaluatePlayerHands_WhenInValid_ThrowArgumentException()
        {
            var playerHandsList = new List<string> { "JS 9S 6S JD 5H 9D KS 8H 5S 4D" };
            _pokerHandsValidator.Setup(x => x.ValidatePlayerHand(It.IsAny<string>())).Returns(false);

            var evaluator = new PokerHandEvaluator(_pokerHandsValidator.Object);

            var exception = Assert.Throws<ArgumentException>(() => evaluator.Evaluate(playerHandsList));
            Assert.Equal($"Player Hands {playerHandsList.First().ToString()} Invalid! Provide valid content and try again.", exception.Message);
        }
    }

    public class TestEvaluatePlayerOneDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] { new List<string> { "JS 9S 6S JD 5H 9D KS 8H 5S 4D" } },
            new object[] { new List<string> { "5H 5S JD 3H 2H 4C JC 3C 4H AS" } },
            new object[] { new List<string> { "QH AS QS 9S AH 2S 7S 4S 2C TH" } },
            new object[] { new List<string> { "3D 8H 8D KH QS AD TC 7D 2S 5H" } },
            new object[] { new List<string> { "KH KD KS 2H 2D QC QS QH 7D 7C" } },
            new object[] { new List<string> { "KH KD 6S TH TD KC KS QC 5D 5C" } },
            new object[] { new List<string> { "4S 4D 4C 4H 5H TD QH AC 9S 3H" } },
            new object[] { new List<string> { "JS 9S 6S JD 5H 9D KS 8H 5S 4D" } },
            new object[] { new List<string> { "9S TS JS QS KS 8D 9D TD JD QD" } },
            new object[] { new List<string> { "TC JC QC KC AC 4H 5H 6H 7H 9H" } },
            new object[] { new List<string> { "TS JS QS KS AS 9D KS 8H 5S 4D" } }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class TestEvaluatePlayerTwoDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] { new List<string> { "6S 7D 5D TH KH TC 5S KD QD 4S" } },
            new object[] { new List<string> { "8D 6S 9D AH 2H 8S JS JH 4S 3S" } },
            new object[] { new List<string> { "KS 8H AS 3H 6D 4C 7H 9C 7C KH" } },
            new object[] { new List<string> { "TH JC 7H 5D 4D 7S JH TC 7D 7C" } },
            new object[] { new List<string> { "4C 6S QS 9D JD AH 2D 5S 4S KH" } },
            new object[] { new List<string> { "5S 8H QS 2H 4S 9C TH 7C AH 2D" } },
            new object[] { new List<string> { "6D TH 8C 3C KC 7H QD 8S 2D QH" } },
            new object[] { new List<string> { "AS 9S TH 8S 4S JD 4C AD 6H 2D" } },
            new object[] { new List<string> { "KH 2S 5H TH JS QH AS TC TD 3D" } },
            new object[] { new List<string> { "4H 2C KH 9C 8H QD QH QC 4S 9S" } },
            new object[] { new List<string> { "QS KH 7D 4C TD TC 2S 7S 5D 5C" } }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class TestEvaluateTiesDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] { new List<string> { "2D 8S QC AC TD 2D 8S QC AC TD" } },
            new object[] { new List<string> { "9H 9D KD QD AS 9H 9D KD QD AS" } },
            new object[] { new List<string> { "2D 2S QC QS AS 2D 2S QC QS AS" } },
            new object[] { new List<string> { "TH TC TD 5D 4D TH TC TD 5D 4D" } },
            new object[] { new List<string> { "4C 5S 6S 7D 8D 4C 5S 6S 7D 8D" } },
            new object[] { new List<string> { "QS 8S 2S KS TS QS 8S 2S KS TS" } },
            new object[] { new List<string> { "8D 8S 8H AC AS 8D 8S 8H AC AS" } },
            new object[] { new List<string> { "AS AC AH AD 4S AS AC AH AD 4S" } },
            new object[] { new List<string> { "5H 6H 7H 8H 9H 5H 6H 7H 8H 9H" } },
            new object[] { new List<string> { "TD JD QD KD AD TC JC QC KC AC" } }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
