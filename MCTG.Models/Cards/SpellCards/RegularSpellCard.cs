using MCTG.Models.Cards.MonsterCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.SpellCards
{
    public class RegularSpellCard : SpellCard
    {
        public override double DamageModifier(ICard opponentCard)
        {
            if (opponentCard == null) throw new ArgumentNullException("Card doesnt exists");
            if (opponentCard is KrakenCard) return 0;
            if (opponentCard is FireSpellCard) return 0.5;
            if (opponentCard is WaterSpellCard) return 2;
            return 1;
        }
    }
}
