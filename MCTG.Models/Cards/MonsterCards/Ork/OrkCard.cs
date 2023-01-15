using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.MonsterCards
{
    public class OrkCard : RegularMonsterCard
    {
    protected override double getDamageForSpecialities(ICard opponent)
        {
            if (opponent is FireWizardCard
                || opponent is WaterWizardCard
                || opponent is RegularWizardCard)
                return 0;
            return 1;
        }
        public override double DamageModifier(ICard opponentCard)
        {
            return base.DamageModifier(opponentCard);
        }

        public OrkCard(Guid cardId, Guid ownerId, double baseDamage) : base(cardId, ownerId, baseDamage)
        {
        }

        public override string ToSqlString()
        {
            return "NaE;Monster;Ork";
        }
    }
}
