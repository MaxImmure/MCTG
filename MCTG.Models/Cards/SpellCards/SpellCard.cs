using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCTG.Models;

namespace MCTG.Models.Cards.SpellCards
{
    public abstract class SpellCard : AbstractCard, ICard
    {

        public override double DamageModifier(ICard opponentCard) => 1;

        protected SpellCard(Guid cardId, Guid ownerId, double baseDamage)
        {
            OwnerId = ownerId;
            CardId = cardId;
            BaseDamage = baseDamage;
        }

        public override double GetDamage(ICard opponentCard)
        {
            return BaseDamage * DamageModifier(opponentCard);
        }

        public override double GetBaseDamage() => BaseDamage;

        public Guid GetId() => CardId;
        public Guid GetOwner() => OwnerId;

        public abstract override string ToSqlString();
    }
}
