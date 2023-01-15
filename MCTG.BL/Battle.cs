using MCTG.Models;
using MCTG.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCTG.DAL;
using MCTG.Models.Exceptions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Host;

namespace MCTG.BL
{
    public class Battle
    {
        private Random rnd;
        private Queue<User> lowEloQueue = new()
            , highEloQueue = new();

        private Dictionary<User, string> sendBattleLog = new();

        private UserRepository userRepository = new UserRepository();

        private static Battle? _instance;
        private static readonly object SyncRoot = new object();

        private Battle()
        {
            rnd = new Random();
        }

        internal static Battle Instance { get; } = new Battle();

        public string QueuePlayer(User p)
        {
            if (p.GameStats.elo < 135)
            {
                if (lowEloQueue.Count > 0)
                {
                    var p2 = lowEloQueue.Dequeue();
                    var log = StartBattleAsync(p, p2).Result;
                    sendBattleLog[p2] = log;
                    return log;
                }
                lowEloQueue.Enqueue(p);
                while (lowEloQueue.Contains(p))
                {
                    Thread.Sleep(100);
                    continue;
                }

                var result = sendBattleLog[p];
                sendBattleLog.Remove(p);
                return result;
            }
            else
            {
                if (highEloQueue.Count > 0)
                {
                    var p2 = highEloQueue.Dequeue();
                    var log = StartBattleAsync(p, p2).Result;
                    sendBattleLog[p2] = log;
                    return log;
                }
                highEloQueue.Enqueue(p);
                while (highEloQueue.Contains(p))
                {
                    Thread.Sleep(100);
                    continue;
                }

                var result = sendBattleLog[p];
                sendBattleLog.Remove(p);
                return result;
            }
        }

        public async Task<string> StartBattleAsync(User p1, User p2)
        {
            var log = new StringBuilder();
            List<ICard> deckP1 = p1.Deck.ToList()
            , deckP2 = p2.Deck.ToList();
            var rnd = new Random();
            ICard currentP1, currentP2;

            for (int round = 1; round <= 100; round++)
            {
                //"<Card Picking Phase>"
                log.AppendLine($"*** Starting Round {round} ***\n<Card Picking Phase>");
                var pickCards = ChooseRandomCards(deckP1, deckP2);
                currentP1 = pickCards.Item1;
                log.AppendLine($"Player1 chooses {currentP1.ToString()}");
                currentP2 = pickCards.Item2;
                log.AppendLine($"Player2 chooses {currentP2.ToString()}"); 

                log.AppendLine($"<Fighting Phase>\n\t{currentP1.ToString()} vs. {currentP2.ToString()}");
                
                var winnerCard = await SimulateCardFightAsync(currentP1, currentP2);
                if (winnerCard == EmptyCard.Instance())
                {
                    log.AppendLine("DRAW:\tNo Winner this Round!");
                    continue;
                }

                log.AppendLine($"WINNER:\t{winnerCard.ToString()}");

                if (winnerCard == currentP1)
                {
                    deckP2.Remove(currentP2);
                    deckP1.Add(currentP2);
                }
                else if(winnerCard == currentP2)
                {
                    deckP1.Remove(currentP1);
                    deckP2.Add(currentP1);
                }

                if (deckP1.Count == 0)
                {
                    log.AppendLine($"***{p2.Credentials.Username} won the game!");
                    p2.GameStats.wins++;
                    p2.GameStats.elo += 3;
                    p1.GameStats.elo -= 5;
                    p1.GameStats.looeses++;
                    userRepository.Update(p1);
                    userRepository.Update(p2);
                    return await Task.FromResult(log.ToString());
                }
                if (deckP2.Count == 0)
                {
                    log.AppendLine($"***{p1.Credentials.Username} won the game!");
                    p1.GameStats.wins++;
                    p1.GameStats.elo += 3;
                    p2.GameStats.elo -= 5;
                    p2.GameStats.looeses++;
                    userRepository.Update(p1);
                    userRepository.Update(p2);
                    return await Task.FromResult(log.ToString());
                }
            }
            p1.GameStats.draws++;
            p2.GameStats.draws++;
            userRepository.Update(p1);
            userRepository.Update(p2);
            return await Task.FromResult(log.ToString());   
        }

        public string StartBattle(ref User p1, ref User p2)
        {
            var log = new StringBuilder();
            List<ICard> deckP1 = p1.Deck.ToList()
            , deckP2 = p2.Deck.ToList();
            var rnd = new Random();
            ICard currentP1, currentP2;

            for (int round = 1; round <= 100; round++)
            {
                //"<Card Picking Phase>"
                log.AppendLine($"*** Starting Round {round} ***\n<Card Picking Phase>");
                var pickCards = ChooseRandomCards(deckP1, deckP2);
                currentP1 = pickCards.Item1;
                log.AppendLine($"Player1 chooses {currentP1.ToString()}");
                currentP2 = pickCards.Item2;
                log.AppendLine($"Player2 chooses {currentP2.ToString()}");

                log.AppendLine($"<Fighting Phase>\n\t{currentP1.ToString()} vs. {currentP2.ToString()}");

                var winnerCard = SimulateCardFight(currentP1, currentP2);
                if (winnerCard == EmptyCard.Instance())
                {
                    log.AppendLine("DRAW:\tNo Winner this Round!");
                    continue;
                }

                log.AppendLine($"WINNER:\t{winnerCard.ToString()}");

                if (winnerCard == currentP1)
                {
                    deckP2.Remove(currentP2);
                    deckP1.Add(currentP2);
                }
                else if (winnerCard == currentP2)
                {
                    deckP1.Remove(currentP1);
                    deckP2.Add(currentP1);
                }

                if (deckP1.Count == 0)
                {
                    log.AppendLine($"***{p2.Credentials.Username} won the game!");
                    p2.GameStats.wins++;
                    p2.GameStats.elo += 3;
                    p1.GameStats.elo -= 5;
                    p1.GameStats.looeses++;
                    userRepository.Update(p1);
                    userRepository.Update(p2);
                    return log.ToString();
                }
                if (deckP2.Count == 0)
                {
                    log.AppendLine($"***{p1.Credentials.Username} won the game!");
                    p1.GameStats.wins++;
                    p1.GameStats.elo += 3;
                    p2.GameStats.elo -= 5;
                    p2.GameStats.looeses++;
                    userRepository.Update(p1);
                    userRepository.Update(p2);
                    return log.ToString();
                }
                //End Condition if its not endless draw, deck.Count == 0
            }
            p1.GameStats.draws++;
            p2.GameStats.draws++;
            userRepository.Update(p1);
            userRepository.Update(p2);
            return log.ToString(); ;//live ping before lobby starts?
        }

        private Tuple<ICard, ICard> ChooseRandomCards(IList<ICard> deckP1, IList<ICard> deckP2)
        {
            return new(
                deckP1.ElementAt(rnd.Next(0, deckP1.Count))
                , deckP2.ElementAt(rnd.Next(0, deckP2.Count)));
        }

        public static async Task<ICard> SimulateCardFightAsync(ICard c1, ICard c2)
        {
            double dmg1 = c1.GetDamage(c2),
                dmg2 = c2.GetDamage(c1);

            if (dmg1 > dmg2) return c1;
            if (dmg2 > dmg1) return c2;

            return EmptyCard.Instance();
        }

        public static ICard SimulateCardFight(ICard c1, ICard c2)
        {
            double dmg1 = c1.GetDamage(c2),
                dmg2 = c2.GetDamage(c1);

            if (dmg1 > dmg2) return c1;
            if (dmg2 > dmg1) return c2;

            return EmptyCard.Instance();
        }

    }
}