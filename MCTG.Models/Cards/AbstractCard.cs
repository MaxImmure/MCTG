using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards
{
    public abstract class AbstractCard : ICard
    {
        public double BaseDamage;

        public Guid CardId;
        public Guid OwnerId;

        public abstract double DamageModifier(ICard opponentCard);

        public abstract double GetDamage(ICard opponentCard);

        public abstract double GetBaseDamage();

        public Guid GetId() => CardId;
        public Guid GetOwner() => OwnerId;

        public void SetOwner(Guid owner) => OwnerId = owner;

        public abstract string ToSqlString();
    }
}
