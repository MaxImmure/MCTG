﻿using MCTG.Models.Cards.SpellCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.MonsterCards
{
    public class WaterMonsterCard : MonsterCard
    {
        public override double DamageModifier(ICard opponentCard)
        {
            if (opponentCard == null) throw new ArgumentNullException("Card doesnt exists");
            //if (opponentCard is MonsterCard || opponentCard.GetType() == typeof(SpellCard)) return 1.0; //no effectiveness on only Monster fights
            if (opponentCard is FireSpellCard) return 2;
            if (opponentCard is NormalSpellCard) return 0.5;
            return 1;
        }
    }
}
