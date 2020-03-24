using Microsoft.Extensions.DependencyInjection;
using PokerHandSorter.Application.Evaluator;
using PokerHandSorter.Application.Validations;
using System;
using System.Collections.Generic;
using System.IO;

namespace PokerHandSorter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var serviceProvider = AddServices();

                IPokerHandEvaluator _evaluator = serviceProvider.GetService<IPokerHandEvaluator>();
                List<string> playerHandList = new List<string>();
                Dictionary<int, int> playerWins = new Dictionary<int, int>();

                if (args.Length == 0)
                    throw new ArgumentNullException("Please provide the file name which you want to evaluate.");

                using (StreamReader sr = new StreamReader(args[0]))
                {
                    while(!sr.EndOfStream)
                        playerHandList.Add(sr.ReadLine());
                }

                playerWins = _evaluator.Evaluate(playerHandList);

                Console.WriteLine("Player 1: {0}", playerWins[1]);
                Console.WriteLine("Player 2: {0}", playerWins[2]);

                if(playerWins[3] > 0)
                    Console.WriteLine("Ties: {0}", playerWins[3]);
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static ServiceProvider AddServices()
        {
            return new ServiceCollection()
                    .AddSingleton<IPokerHandEvaluator, PokerHandEvaluator>()
                    .AddTransient<IPokerHandsValidator, PokerHandsValidator>()
                    .BuildServiceProvider();
        }
    }
}
