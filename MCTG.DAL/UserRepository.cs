using MCTG.BL;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCTG.Models;

namespace MCTG.DAL
{
    public class UserRepository
    {
        //ToDo Add Stats to User
        public void Insert(User newUser)
        {
            string sql = "INSERT INTO users (guid, username, u_password, coins, u_description) VALUES (@p1, @p2, @p3, @p4, @p5)";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", newUser.Guid.ToString());
            cmd.Parameters.AddWithValue("p2", newUser.Username);
            cmd.Parameters.AddWithValue("p3", newUser.Password);
            cmd.Parameters.AddWithValue("p4", newUser.Coins);
            cmd.Parameters.AddWithValue("p5", newUser.Description);

            if (!Database.Instance().ExecuteNonQuery(cmd))
                throw new Exception(); //ToDo 
        }

        public void Delete(User user)
        {
            string sql = "DELETE FROM users WHERE guid = @p1";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", user.Guid.ToString());

            if (!Database.Instance().ExecuteNonQuery(cmd))
            {
                throw new Exception();
                //ToDo
            }
        }

        public void Update(User updatedUser)
        {
            string sql = "UPDATE users SET, username=@p1, u_password=@p2, coins=@p3, u_description=@p4 WHERE guid=@p5";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", updatedUser.Username);
            cmd.Parameters.AddWithValue("p2", updatedUser.Password);
            cmd.Parameters.AddWithValue("p3", updatedUser.Coins);
            cmd.Parameters.AddWithValue("p4", updatedUser.Description);
            cmd.Parameters.AddWithValue("p5", updatedUser.Guid.ToString());

            if (!Database.Instance().ExecuteNonQuery(cmd))
            {
                throw new Exception();
                //ToDo
            }
        }
    }
}
