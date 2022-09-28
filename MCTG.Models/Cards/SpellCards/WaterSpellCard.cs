using MCTG.Models.Cards.MonsterCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.SpellCards
{
    public class WaterSpellCard : SpellCard
    {
        public override double DamageModifier(ICard opponentCard)
        {
            if (opponentCard == null) throw new ArgumentNullException("Card doesnt exists");
            if (opponentCard is NormalSpellCard || opponentCard is NormalMonsterCard) return 0.5;
            if (opponentCard is FireSpellCard || opponentCard is FireMonsterCard) return 2;
            return 1;
        }
    }
}
