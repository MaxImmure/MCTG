using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.MonsterCards
{
    public class WaterGoblinCard : WaterMonsterCard
    {
        protected override double getDamageForSpecialities(ICard opponent)
        {
            if (opponent is DragonCard)
                return 0;
            return 1;
        }
        public override double DamageModifier(ICard opponentCard)
        {
            return base.DamageModifier(opponentCard);
        }

        public WaterGoblinCard(double baseDamage) : base(baseDamage)
        {
        }
    }
}
