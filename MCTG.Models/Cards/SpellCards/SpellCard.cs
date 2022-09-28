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
        public abstract double damageModyfier(ICard opponentCard);
    }
}
