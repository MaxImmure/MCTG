using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCTG.Models;

namespace MCTG.Models.Cards.SpellCards
{
    public abstract class SpellCard : ICard
    {
        public double BaseDamage { get; private set; }
        public virtual double DamageModifier(ICard opponentCard) { return 1; }

        public SpellCard(double baseDamage)
        {
            this.BaseDamage = baseDamage;
        }
        public double getDamage(ICard opponentCard)
        {
            return BaseDamage * DamageModifier(opponentCard);
        }
    }
}
