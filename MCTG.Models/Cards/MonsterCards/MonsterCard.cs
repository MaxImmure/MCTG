using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.MonsterCards
{
    public abstract class MonsterCard : ICard
    {
        public virtual double DamageModifier(ICard opponentCard) { return 1; }
        public void SetOwner(Guid owner) => OwnerId = owner;

        public virtual double GetDamage(ICard opponentCard) { return BaseDamage * DamageModifier(opponentCard) * getDamageForSpecialities(opponentCard); }
        public double GetBaseDamage() => BaseDamage;

        private double BaseDamage;
        protected Guid CardId;
        protected Guid OwnerId;

        public Guid GetId() => CardId;
        public Guid GetOwner() => OwnerId;

        public abstract string ToSqlString();

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
