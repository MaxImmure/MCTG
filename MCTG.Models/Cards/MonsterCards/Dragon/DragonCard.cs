using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.MonsterCards
{
<<<<<<<< HEAD:MCTG.Models/Cards/MonsterCards/Dragon/DragonCard.cs
    public class DragonCard : FireMonsterCard
========
    public class NormalElfCard : NormalMonsterCard
>>>>>>>> 8cc8aa25b7426be5c2a5baabebd373087698bb49:MCTG.Models/Cards/MonsterCards/Elve/NormalElfCard.cs
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
