using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCTG.BL;
using MCTG.Models;
using Npgsql;

namespace MCTG.DAL
{
    public class DataLayerPostgres
    {

        public DataLayerPostgres()
        {
            initDatabase();
        }

        #region Init
        private void initDatabase()
        {
            string createuser = "CREATE TABLE IF NOT EXISTS users (guid char(36) PRIMARY KEY" +
                                ", username varchar(40) NOT NULL UNIQUE" +
                                ", u_password varchar(64) NOT NULL" +
                                ", coins integer NOT NULL" +
                                ", u_description varchar(128)" +
                                ", CHECK(coins >= 0));";

            Database.Instance().ExecuteNonQuery(new NpgsqlCommand(createuser));
        }
        #endregion

        //(private) User Repo Class -> Repository Pattern
        #region User
        
        public void CreateUser(User newUser)
        {
            //Delete - testCases
        }

        public void DeleteUser(User user)
        {
            //Delete - testCases
        }

        public User GetUser(String name)
        {
            //Delete - testCases
            return null;
        }


        public List<User> GetAllUsers()
        {
            List<User> res = new();
            string sql = "SELECT * FROM users";
            var cmd = new NpgsqlCommand(sql);

            using (NpgsqlDataReader reader = Database.Instance().ExecuteQuery(cmd))
            {
                while (reader.Read())
                {
                    res.Add(
                        new(
                            Guid.Parse(reader.GetValue(0).ToString()), 
                            reader.GetValue(1).ToString(), 
                            reader.GetValue(2).ToString(), 
                            Convert.ToInt32(reader.GetValue(3)), 
                            reader.GetValue(4).ToString()));
                }
                return res;
            }
        }

        public bool UpdateUser(User updatedUser)
        {
            string sql = "UPDATE users SET guid=@p1, username=@p2, u_password=@p3, coins=@p4 WHERE guid=@p5";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", updatedUser.Guid.ToString());
            cmd.Parameters.AddWithValue("p2", updatedUser.Username);
            cmd.Parameters.AddWithValue("p3", updatedUser.Password);
            cmd.Parameters.AddWithValue("p4", updatedUser.Coins);
            cmd.Parameters.AddWithValue("p5", updatedUser.Guid.ToString());

            if (Database.Instance().ExecuteNonQuery(cmd))
            {
                return true;
            }

            return false;
        }

        public User FindUserByName(string username)
        {
            string sql = "SELECT * FROM users WHERE username=@p1";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", username);

            using (NpgsqlDataReader reader = Database.Instance().ExecuteQuery(cmd))
            {

                if (reader.Read())
                {
                    return new User(Guid.Parse(reader.GetValue(0).ToString()), username, reader.GetValue(2).ToString(), Convert.ToInt32(reader.GetValue(3).ToString()), reader.GetValue(4).ToString());
                }
                return null;
            }
        }

        #endregion

    }
}
