﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.MonsterCards
{
    public class DragonCard : FireMonsterCard
    {
        protected override double getDamageForSpecialities(ICard opponent)
        {
            if (opponent is FireElfCard) //ToDo evade attack
                return 0;
            return 1;
        }
        public override double DamageModifier(ICard opponentCard)
        {
            return base.DamageModifier(opponentCard);
        }

        public DragonCard(Guid cardId, Guid ownerId, double baseDamage) : base(cardId, ownerId, baseDamage)
        {
        }

        public override string ToSqlString()
        {
            return "NaE;Monster;Dragon";
        }
    }
}
