﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.MonsterCards
{
    public class NormalTrollCard : NormalMonsterCard
    {
        public NormalTrollCard(double BaseDmg) : base(BaseDmg)
        { }
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
