﻿using MCTG.Models.Cards.SpellCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.MonsterCards
{
    public abstract class WaterMonsterCard : MonsterCard
    {
        public override double DamageModifier(ICard opponentCard)
        {
            if (opponentCard == null) throw new ArgumentNullException("Card doesnt exists");
            //if (opponentCard is MonsterCard || opponentCard.GetType() == typeof(SpellCard)) return 1.0; //no effectiveness on only Monster fights
            if (opponentCard is FireSpellCard) return 2;
            if (opponentCard is RegularSpellCard) return 0.5;
            return 1;
        }

        protected WaterMonsterCard(Guid cardId, Guid ownerId, double baseDamage) : base(cardId, ownerId, baseDamage)
        {
        }

        public override string ToSqlString()
        {
            return "Water;Monster;";
        }
    }
}
