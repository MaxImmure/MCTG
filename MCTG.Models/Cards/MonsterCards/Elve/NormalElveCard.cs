using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.MonsterCards
{
    public class NormalElveCard : NormalMonsterCard
    {
        public NormalElveCard(double BaseDmg) : base(BaseDmg)
        { }
        protected override double getDamageForSpecialities(ICard opponent)
        {
            return 1;
        }
        public override double DamageModifier(ICard opponentCard)
        {
            return base.DamageModifier(opponentCard);
        }
    }
}
