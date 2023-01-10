using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards
{
    public class EmptyCard : ICard
    {
        private static EmptyCard _instance;

        public readonly double damage;

        private EmptyCard() => this.damage = -1.0;

        public static EmptyCard Instance() => _instance ??= new();

        public double DamageModifier(ICard opponentCard) => -1;

        public double getDamage(ICard opponentCard) => -1;
    }
}
