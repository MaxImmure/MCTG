using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.MonsterCards
{
<<<<<<<< HEAD:MCTG.Models/Cards/MonsterCards/Goblin/RegularGoblinCard.cs
    public class RegularGoblinCard : RegularMonsterCard
    {
        protected override double getDamageForSpecialities(ICard opponent)
        {
            if (opponent is DragonCard)
========
    public class DragonCard : NormalMonsterCard
    {
        protected override double getDamageForSpecialities(ICard opponent)
        {
            if (opponent is FireElfCard) //ToDo can evade 
>>>>>>>> 8cc8aa25b7426be5c2a5baabebd373087698bb49:MCTG.Models/Cards/MonsterCards/Dragon/DragonCard.cs
                return 0;
            return 1;
        }
        public override double DamageModifier(ICard opponentCard)
        {
            return base.DamageModifier(opponentCard);
        }
    }
}
