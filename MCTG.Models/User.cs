using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCTG.Models.Cards;

namespace MCTG.Models
{
    public class User
    {
        public Guid Guid { get; } //= Guid.NewGuid();
        public string Username { get; }
        public string Password { get; } 
        public int Coins { get; } = 20;
        public string Description { get; }
        public Stats GameStats { get; }
        public ICard[] Deck = new ICard[4];
        public List<ICard> Stack = new List<ICard>();

        public User(Guid guid, string username, string password, int coins, string description)
        {
            throw new NotImplementedException();
        }
    }
}