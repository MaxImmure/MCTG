using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using MCTG.Models.Cards;

namespace MCTG.Models
{
    public class User
    {
        public Guid Guid { get; } //= Guid.NewGuid();
        public string Username { get; set; }
        public string Password { get; set; } 
        public int Coins { get; }
        public string Description { get; } = string.Empty;
        public Stats GameStats { get; }
        public ICard[] Deck = new ICard[4];
        public List<ICard> Stack = new List<ICard>();

        public User(Guid guid, string username, string password, int coins = 20, string description = "")
        {
            this.Guid = guid;
            this.Username = username;
            this.Password = password;
            this.Coins = coins;
            this.Description = description;
        }

        public User(string username, string password, int coins = 20, string description = "")
        {
            this.Guid = Guid.NewGuid();
            //check Guid if exists
            this.Username = username;
            this.Password = password;
            this.Coins = coins;
            this.Description = description;
        }

        public User()
        {
            Guid = Guid.Empty;
        }

        public bool Equals(User u)
        {
            return (Guid.Equals(u.Guid) && Username.Equals(u.Username) && Password.Equals(u.Password) &&
                    Coins == u.Coins && Description.Equals(u.Description));
            //ToDo update
        }
    }
}