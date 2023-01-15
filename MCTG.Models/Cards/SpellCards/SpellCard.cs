using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCTG.Models;

namespace MCTG.Models.Cards.SpellCards
{
    public abstract class SpellCard : ICard
    {
        private double BaseDamage;
        private Guid CardId;
        private Guid OwnerId;
        public virtual double DamageModifier(ICard opponentCard) { return 1; }
        public void SetOwner(Guid owner) => OwnerId = owner;

        protected SpellCard(Guid cardId, Guid ownerId, double baseDamage)
        {
            this.OwnerId = ownerId;
            this.CardId = cardId;
            this.BaseDamage = baseDamage;
        }

        public double GetDamage(ICard opponentCard)
        {
            return BaseDamage * DamageModifier(opponentCard);
        }

        public double GetBaseDamage() => BaseDamage;

        public Guid GetId() => CardId;
        public Guid GetOwner() => OwnerId;

        public abstract string ToSqlString();
    }
}
