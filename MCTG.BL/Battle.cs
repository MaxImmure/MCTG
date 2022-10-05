using MCTG.Exceptions;
using MCTG.Models;
using MCTG.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCTG.DAL
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
        

        private Tuple<ICard,ICard> chooseRandomCards(ICard[] deck1, ICard deck2)
        {
            //ToDo
            return null;
        }
    }
}