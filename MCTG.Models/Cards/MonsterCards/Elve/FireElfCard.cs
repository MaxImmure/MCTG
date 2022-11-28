using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.MonsterCards
{
<<<<<<<< HEAD:MCTG.Models/Cards/MonsterCards/Troll/WaterTrollCard.cs
    public class WaterTrollCard : WaterMonsterCard
========
    public class FireElfCard : FireMonsterCard
>>>>>>>> 8cc8aa25b7426be5c2a5baabebd373087698bb49:MCTG.Models/Cards/MonsterCards/Elve/FireElfCard.cs
    {
        protected override double getDamageForSpecialities(ICard opponent)
        {
            return 1;
        }
        public override double DamageModifier(ICard opponentCard)
        {
            return base.DamageModifier(opponentCard);
        }
    }
}
