using MCTG.Models.Cards.SpellCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCTG.Models.Cards.MonsterCards
{
    public abstract class RegularMonsterCard : MonsterCard
    {
        public override double DamageModifier(ICard opponentCard)
        {
            if (opponentCard == null) throw new ArgumentNullException("Card doesnt exists");
            //if (opponentCard is MonsterCard || opponentCard.GetType() == typeof(SpellCard)) return 1.0; //no effectiveness on only Monster fights
            if (opponentCard is WaterSpellCard) return 2;
            if (opponentCard is FireSpellCard) return 0.5;
            return 1;
        }
    }
}