using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCTG.Models
{
    public class User
    {
        public string Name { get; }
        public double Coins { get; } = 20;
        public Stats GameStats { get; }
        public ICard[] Deck = new ICard[4];
        public List<ICard> Stack = new List<ICard>();
    }
}