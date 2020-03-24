using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHandSorter.Application.Evaluator
{
    public interface IPokerHandEvaluator
    {
        public Dictionary<int, int> Evaluate(List<string> playerHandsList);
    }
}
