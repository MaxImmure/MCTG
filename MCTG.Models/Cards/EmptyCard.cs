using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCTG.Models.Cards
{
    public class EmptyCard : ICard
    {
        private static EmptyCard _instance;
        private EmptyCard()
        {
            this.damage = -1.0;
        }

        public static EmptyCard getInstance()
        {
            if (EmptyCard._instance == null)
                _instance = new EmptyCard();
            return EmptyCard._instance;
        }

        public readonly double damage;

        public double DamageModifier(ICard opponentCard)
        {
            return -1;
        }

        public double getDamage(ICard opponentCard)
        {
            return -1;
        }
    }
}