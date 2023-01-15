using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards
{
    public class EmptyCard : AbstractCard, ICard
    {
        private static EmptyCard? _instance;

        public readonly double Damage;

        private Guid CardId = Guid.Empty;
        public Guid OwnerId = Guid.Empty;

        private EmptyCard() => this.Damage = -1.0;

        public static EmptyCard Instance() => _instance ??= new();

        public override double DamageModifier(ICard opponentCard) => -1;

        public override double GetDamage(ICard opponentCard) => -1;
        public override double GetBaseDamage() => Damage;

        public Guid GetId() => CardId;
        public Guid GetOwner() => OwnerId;
        public void SetOwner(Guid owner) => OwnerId = owner;

        public override string ToSqlString()
        {
            return $";Empty;";
        }
    }
}
