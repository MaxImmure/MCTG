using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCTG.Models
{
    public interface ICard
    {
        abstract double DamageModifier(ICard opponentCard);
    }
}