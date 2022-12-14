using MCTG.BL;
using MCTG.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.DAL
{
    public class UserController
    {
        UserRepository userRepository = new();

        public void AddUser(User newUser)
        {
            if(GetUser(newUser) == null)
                userRepository.Insert(newUser); //ToDo Else
        }

        public void DeleteUser(User userToDelete)
        {
            if (GetUser(userToDelete) == null)
                userRepository.Delete(userToDelete);
        }   

        public User GetUser(User u)
        {
            return GetUser(u.Guid.ToString());
        }

        public User GetUser(string uuid)
        {
            string sql = "SELECT username, u_password, coins, u_description FROM users WHERE guid=@p1";
            var cmd = new NpgsqlCommand(sql);
            cmd.Parameters.AddWithValue("p1", uuid);

            using (NpgsqlDataReader reader = Database.Instance().ExecuteQuery(cmd))
            {

                if (reader.Read())
                {
                    return new User(Guid.Parse(uuid),
                        reader.GetValue(0).ToString(),
                        reader.GetValue(1).ToString(),
                        Convert.ToInt32(reader.GetValue(2)),
                        reader.GetValue(3).ToString());
                }
                return null;
            }
        }
    }
}
