using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Cards.MonsterCards
{
    public abstract class MonsterCard : ICard
    {
        public abstract double DamageModifier(ICard opponentCard);
    }
}
