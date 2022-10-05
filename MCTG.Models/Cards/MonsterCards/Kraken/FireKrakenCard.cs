using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.MonsterCards.Goblin
{
    public abstract class FireKrakenCard : FireMonsterCard
    {
        protected override double getDamageForSpecialities(ICard opponent)
        {
            return base.getDamageForSpecialities(opponent);
        }
        public override double DamageModifier(ICard opponentCard)
        {
            return base.DamageModifier(opponentCard);
        }
    }
}
