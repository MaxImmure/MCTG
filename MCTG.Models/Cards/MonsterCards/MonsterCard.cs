using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.MonsterCards
{
    public abstract class MonsterCard : ICard
    {
        public virtual double DamageModifier(ICard opponentCard) { return 1; }
        
        public virtual double getDamage(ICard opponentCard) { return BaseDamage * DamageModifier(opponentCard) * getDamageForSpecialities(opponentCard); }

        public double BaseDamage { get; private set; }

        public MonsterCard(double baseDamage)
        {
            this.BaseDamage = baseDamage;
        }

        protected virtual double getDamageForSpecialities(ICard opponentCard)
        {
            return 1;
        }
    }
}
