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
        
        public virtual double getDamage(ICard opponenCard) { return BaseDamage * DamageModifier(opponenCard) * getDamageForSpecialities(opponenCard); }

        public MonsterCard(double baseDmg)
        {
            this.BaseDamage = baseDmg;
        }

        public double BaseDamage { get; private set; }
        public string Name { get { return GetType().Name; } }

        protected virtual double getDamageForSpecialities(ICard opponentCard)
        {
            return 1;
        }
    }
}
