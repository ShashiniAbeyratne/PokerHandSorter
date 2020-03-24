using PokerHandSorter.Application.Validations;
using PokerHandSorter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHandSorter.Application.Evaluator
{
    public class PokerHandEvaluator : IPokerHandEvaluator
    {
        private readonly IPokerHandsValidator _validator;
        private Dictionary<int, int> _playerWins;

        public PokerHandEvaluator(IPokerHandsValidator validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _playerWins = new Dictionary<int, int>();
        }

        public Dictionary<int, int> Evaluate(List<string> playerHandsList)
        {
            _playerWins.Add(1, 0);
            _playerWins.Add(2, 0);
            _playerWins.Add(3, 0);

            foreach (string playerHand in playerHandsList)
            {
                if (_validator.ValidatePlayerHand(playerHand))
                {
                    playerHand.ToUpper();

                    Hand playerOne = new Hand(playerHand.Split(" ").Take(5).ToList());
                    Hand playerTwo = new Hand(playerHand.Split(" ").Skip(5).Take(5).ToList());

                    playerOne.EvaluateHandRank();
                    playerTwo.EvaluateHandRank();

                    if (GetWinner(playerOne, playerTwo) == 1)
                        _playerWins[1]++; 
                    else if (GetWinner(playerOne, playerTwo) == 2)
                        _playerWins[2]++;
                    else if (GetWinner(playerOne, playerTwo) == 0)
                        _playerWins[3]++;
                }
                else
                    throw new ArgumentException($"Player Hands {playerHand.ToString()} Invalid! Provide valid content and try again.");
            }

            return _playerWins;
        }

        private int GetWinner(Hand playerOne, Hand playerTwo)
        {
            if (playerOne.Rank.Id > playerTwo.Rank.Id)
                return 1;
            else if (playerOne.Rank.Id < playerTwo.Rank.Id)
                return 2;
            else 
            {
                int i = 0;
                int playerOneHighestCard, playerTwoHighestCard;

                do
                {
                    playerOneHighestCard = playerOne.GetHighestCard(i);
                    playerTwoHighestCard = playerTwo.GetHighestCard(i);

                    i++;

                } while (playerOneHighestCard == playerTwoHighestCard && i < playerOne.Rank.MaxComparisons);

                if (playerOneHighestCard > playerTwoHighestCard)
                    return 1;
                else if (playerOneHighestCard < playerTwoHighestCard)
                    return 2;
                else
                    return 0;
            }
        }
    }
}
