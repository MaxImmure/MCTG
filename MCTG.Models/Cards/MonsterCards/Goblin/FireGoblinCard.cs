﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.MonsterCards.Goblin
{
    public class FireGoblinCard : FireMonsterCard
    {
        protected override double getDamageForSpecialities(ICard opponent)
        {
            if (opponent is FireDragonCard
                || opponent is WaterDragonCard
                || opponent is NormalDragonCard)
                return 0;
            return 1;
        }
        public override double DamageModifier(ICard opponentCard)
        {
            return base.DamageModifier(opponentCard);
        }
    }
}
