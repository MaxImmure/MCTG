using MCTG.Models.Cards.SpellCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.MonsterCards
{
    public class WaterKrakenCard : WaterMonsterCard
    {
        public WaterKrakenCard(double BaseDmg) : base(BaseDmg)
        { }
        protected override double getDamageForSpecialities(ICard opponentCard)
        {
            return base.getDamageForSpecialities(opponentCard);
        }
        public override double DamageModifier(ICard opponentCard)
        {
            return base.DamageModifier(opponentCard);
        }
    }
}
