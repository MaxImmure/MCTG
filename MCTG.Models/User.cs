using MCTG.DAL;
using MCTG.Models.Cards;

namespace MCTG.Models
{
    public class User
    {
        public Guid Guid { get; set; } //= Guid.NewGuid();
        public string Name { get; set; }
        public LoginCredentials Credentials { get; set; }
        public double Coins { get; set; }
        public string Description { get; set; } = string.Empty;
        public Stats GameStats { get; set; }
        public ICard[] Deck = new ICard[4];
        
        public User(Guid guid, string username, int coins = 20, string description = "")
        {
            this.Guid = guid;
            Credentials = new LoginCredentials(username, string.Empty);
            this.Coins = coins;
            this.Description = description;
            GameStats = new Stats()
            {
                username = Credentials.Username,
                PlayerId = Guid
            };
        }

        public User(Guid guid, string username, string password, string name = "", int coins = 20, string description = "")
        {
            this.Guid = guid;
            this.Name = name;
            Credentials = new LoginCredentials(username, password);
            GameStats = new Stats()
            {
                username = Credentials.Username, PlayerId = Guid
            };
            this.Coins = coins;
            this.Description = description;
        }

        public User()
        {
            Guid = Guid.NewGuid();
        }

        public string ProfileString()
        {
            return $"<Id: {Guid.ToString()}\nUsername: {Credentials.Username}\nName: {Name}\nDescription: {Description}\nCoins: {Coins}";
        }

        public override string ToString()
        {
            return $"{Credentials.Username}: {GameStats.elo}";
        }

        public bool Equals(User u)
        {
            return (Guid.Equals(u.Guid));
        }
    }
}