using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.MonsterCards
{
    public abstract class MonsterCard : AbstractCard, ICard
    {
        public override double DamageModifier(ICard opponentCard) { return 1; }
        public void SetOwner(Guid owner) => OwnerId = owner;

        public override double GetDamage(ICard opponentCard) { return BaseDamage * DamageModifier(opponentCard) * getDamageForSpecialities(opponentCard); }
        public override double GetBaseDamage() => BaseDamage;

        public Guid GetId() => CardId;
        public Guid GetOwner() => OwnerId;

        public abstract override string ToSqlString();

        protected MonsterCard(Guid cardId, Guid ownerId,double baseDamage)
        {
            this.CardId = cardId;
            this.OwnerId = ownerId;
            this.BaseDamage = baseDamage;
        }

        protected virtual double getDamageForSpecialities(ICard opponentCard)
        {
            return 1;
        }
    }
}
