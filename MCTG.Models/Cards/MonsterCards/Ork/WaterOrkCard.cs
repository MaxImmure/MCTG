using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.MonsterCards
{
    public class WaterOrkCard : WaterMonsterCard
    {
        protected override double getDamageForSpecialities(ICard opponent)
        {
            if (opponent is FireWizzardCard
                || opponent is WaterWizzardCard
                || opponent is RegularWizzardCard)
                return 0;
            return 1;
        }
        public override double DamageModifier(ICard opponentCard)
        {
            return base.DamageModifier(opponentCard);
        }
    }
}
