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
        private Database _db = Database.Instance();

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

            _db.ExecuteNonQuery(new NpgsqlCommand(createuser));
        }
        #endregion

        #region User
        //ToDo Add Stats to User
        public bool CreateUser(User newUser)
        {
            string sql = "INSERT INTO users (guid, username, u_password, coins, u_description) VALUES (@p1, @p2, @p3, @p4, @p5)";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", newUser.Guid.ToString());
            cmd.Parameters.AddWithValue("p2", newUser.Username);
            cmd.Parameters.AddWithValue("p3", newUser.Password);
            cmd.Parameters.AddWithValue("p4", newUser.Coins);
            cmd.Parameters.AddWithValue("p5", newUser.Description);

            if (_db.ExecuteNonQuery(cmd))
            {
                return true;
            }

            return false;
        }

        public bool DeleteUser(string uuid)
        {
            string sql = "DELETE FROM users WHERE guid = @p1";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", uuid);

            if (_db.ExecuteNonQuery(cmd))
            {
                return true;
            }

            return false;
        }

        public bool DeleteUser(User userToDelete)
        {
            return DeleteUser(userToDelete.Guid.ToString());
        }

        public User GetUser(string uuid)
        {
            string sql = "SELECT username, u_password, coins, u_description FROM users WHERE guid=@p1";
            var cmd = new NpgsqlCommand(sql);
            cmd.Parameters.AddWithValue("p1", uuid);

            using (NpgsqlDataReader reader = _db.ExecuteQuery(cmd))
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

        public List<User> GetAllUsers()
        {
            List<User> res = new();
            string sql = "SELECT * FROM users";
            var cmd = new NpgsqlCommand(sql);

            using (NpgsqlDataReader reader = _db.ExecuteQuery(cmd))
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

            if (_db.ExecuteNonQuery(cmd))
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

            using (NpgsqlDataReader reader = _db.ExecuteQuery(cmd))
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
