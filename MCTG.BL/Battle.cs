using MCTG.Models;
using MCTG.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCTG.BL
{
    public class Battle
    {
        User player1, player2;
        Random rnd;

        public Battle(User player1, User player2)
        {
            this.player1 = player1;
            this.player2 = player2;
            rnd = new Random(); //generate new Random each lobby
        } 

        public string startBattle(User p1, User p2)
        {
            var log = new StringBuilder();
            List<ICard> deckP1 = p1.Deck.ToList()
            , deckP2 = p2.Deck.ToList();
            var rnd = new Random();
            //ToDo Logging and return log
            ICard currentP1, currentP2;

            for (int round = 1; round <= 100; round++)
            {
                //Choose random Card for each player
                //"<Card Picking Phase>"
                var pickCards = chooseRandomCards(deckP1, deckP2);
                currentP1 = pickCards.Item1;
                log.AppendLine($"Player1 chooses {currentP1.ToString()}"); //ToDo ToString
                currentP2 = pickCards.Item2;
                log.AppendLine($"Player2 chooses {currentP2.ToString()}"); //ToDo ToString

                log.AppendLine($"Fighting Phase\n\t{currentP1.ToString()} vs. {currentP2.ToString()}");
                
                var winnerCard = simulateCardFight(currentP1, currentP2);
                if (winnerCard == EmptyCard.Instance())
                {
                    log.AppendLine("DRAW:\tNo Winner this Round!");
                    continue;
                }

                log.AppendLine($"WINNER:\t{winnerCard.ToString()}");

                //ToDo Move looserCard to PlayerWinner
                //End Condition if its not endless draw, deck.Count == 0
            }
            return log.ToString();//live ping before lobby starts?
        }

        private Tuple<ICard, ICard> chooseRandomCards(IList<ICard> deckP1, IList<ICard> deckP2)
        {
            try
            {
                return new(
                        deckP1.ElementAt(rnd.Next(0, deckP1.Count))
                        , deckP2.ElementAt(rnd.Next(0, deckP2.Count)));
            } catch (Exception)
            {
                throw new NotImplementedException();//ToDo
            }
            //ToDo unable to Pick
            return null;
        }

        public static ICard simulateCardFight(ICard c1, ICard c2)
        {
            double dmg1 = c1.getDamage(c2),
                dmg2 = c2.getDamage(c1);

            if (dmg1 > dmg2) return c1;
            if (dmg2 > dmg1) return c2;

            return EmptyCard.Instance();
        }

    }
}