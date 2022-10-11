using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.MonsterCards
{
    public class WaterDragonCard : WaterMonsterCard
    {
        protected override double getDamageForSpecialities(ICard opponent)
        {
            if (opponent is NormalElveCard) //ToDo can evade 
                return 0;
            return 1;
        }
        public override double DamageModifier(ICard opponentCard)
        {
            return base.DamageModifier(opponentCard);
        }
    }
}
