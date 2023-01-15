using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCTG.Models.Cards
{
    public interface ICard
    {
        public abstract double DamageModifier(ICard opponentCard);
        public abstract double GetDamage(ICard opponentCard);
        public abstract double GetBaseDamage();
        public Guid GetId();
        public Guid GetOwner();
        public void SetOwner(Guid owner);
        public String ToSqlString();
    }
}