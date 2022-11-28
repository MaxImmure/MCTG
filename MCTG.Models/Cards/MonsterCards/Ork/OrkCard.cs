using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.MonsterCards
{
<<<<<<<< HEAD:MCTG.Models/Cards/MonsterCards/Ork/RegularOrkCard.cs
    public class RegularOrk : RegularMonsterCard
========
    public class OrkCard : NormalMonsterCard
>>>>>>>> 8cc8aa25b7426be5c2a5baabebd373087698bb49:MCTG.Models/Cards/MonsterCards/Ork/OrkCard.cs
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
