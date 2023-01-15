using MCTG.Models.Cards;

namespace MCTG.Models
{
    public class User
    {
        public Guid Guid { get; set; } //= Guid.NewGuid();
        public string Username { get; set; }
        public string Password { get; set; }
        public double Coins { get; set; }
        public string Description { get; set; } = string.Empty;
        public Stats GameStats { get; set; }
        public ICard[] Deck = new ICard[4];
        public List<ICard> Stack = new List<ICard>();
        
        public User(Guid guid, string username, int coins = 20, string description = "")
        {
            this.Guid = guid;
            this.Username = username;
            this.Coins = coins;
            this.Description = description;
        }

        public User(Guid guid, string username, string password, int coins = 20, string description = "")
        {
            this.Guid = guid;
            this.Username = username;
            this.Password = password;
            this.Coins = coins;
            this.Description = description;
        }

        public User(string username, int coins = 20, string description = "")
        {
            this.Guid = Guid.NewGuid();
            //check Guid if exists
            this.Username = username;
            this.Coins = coins;
            this.Description = description;
        }

        public User()
        {
            Guid = Guid.NewGuid();
        }

        public bool Equals(User u)
        {
            return (Guid.Equals(u.Guid) && Username.Equals(u.Username) &&
                    Coins == u.Coins && Username.Equals(u.Password) && Description.Equals(u.Description));
        }
    }
}