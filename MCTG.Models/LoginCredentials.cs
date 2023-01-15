using MCTG.Models;
using MCTG.Models.Cards;

namespace MCTG.Models
{

    public record LoginCredentials
    {
        public readonly string Username;
        public string Password;

        public LoginCredentials(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}