using MCTG.Exceptions;
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

        public Battle(User player1, User player2)
        {
            this.player1 = player1;
            this.player2 = player2;
        } 

        public string startBattle(User p1, User p2)
        {
            List<ICard> deckP1 = p1.Deck.ToList()
            , deckP2 = p2.Deck.ToList();
            //ToDo Logging
            for (int round = 1; round <= 100; round++)
            {
                
            }
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


        private Tuple<ICard,ICard> chooseRandomCards(ICard[] deck1, ICard deck2)
        {
            //ToDo
            return null;
        }
    }
}