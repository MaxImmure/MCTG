using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards
{
    public class EmptyCard : ICard
    {
        private static EmptyCard? _instance;

        public readonly double Damage;

        private Guid CardId = Guid.Empty;
        public Guid OwnerId = Guid.Empty;

        private EmptyCard() => this.Damage = -1.0;

        public static EmptyCard Instance() => _instance ??= new();

        public double DamageModifier(ICard opponentCard) => -1;

        public double GetDamage(ICard opponentCard) => -1;
        public double GetBaseDamage() => Damage;

        public Guid GetId() => CardId;
        public Guid GetOwner() => OwnerId;
        public void SetOwner(Guid owner) => OwnerId = owner;

        public string ToSqlString()
        {
            return $";Empty;";
        }
    }
}
