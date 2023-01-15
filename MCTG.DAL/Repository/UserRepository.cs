using System.Xml;
using Npgsql;
using MCTG.BL;
using MCTG.DAL.Repository;
using MCTG.Models;
using MCTG.Models.Cards;
using MCTG.Models.Exceptions;

namespace MCTG.DAL
{
    public class UserRepository : IRepository<User>
    {
        private readonly Database _db = Database.Instance() ?? throw new InvalidOperationException();

        private CardRepository _cardRepository = new();

        public void Delete(User user)
        {
            if (GetById(user.Guid) == null)
                return;//ToDo delete other tables
            string sql = "DELETE FROM public.\"users\" WHERE guid = @id;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("id", user.Guid.ToString());

            if (!_db.ExecuteNonQuery(cmd))
            {
                throw new UserNotFoundException("Failed to Delete user! There is no User with that ID");
            }
        }

        public Guid GetIdFromUsername(string username)
        {
            if (!ExistsByName(username))
                throw new UserNotFoundException();
            string sql = "SELECT u.guid FROM public.\"user\" u INNER JOIN public.\"login_credentials\" lc ON u.guid=lc.guid WHERE username = @name;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("name", username);

            using var reader = _db.ExecuteQuery(cmd);
            if(reader.Read())
            {
                var result = new Guid(reader.GetString(0));
                reader.Close();
                return result;
            }
            return Guid.Empty;
        }

        public bool ValidLogin(User user)
        {
            bool result = false;
            string sql = "SELECT COUNT(*) > 0 FROM public.\"login_credentials\" WHERE username=@uname AND password=@pw;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("id", user.Guid);
            cmd.Parameters.AddWithValue("uname", user.Username);
            cmd.Parameters.AddWithValue("pw", user.Password);

            var reader = _db.ExecuteQuery(cmd);
            if (reader.Read())
            {
                result = reader.GetBoolean(0);
                reader.Close();
            }

            return result;
        }

        public IEnumerable<User> GetAll()
        {
            List<User> users = new();
            string sql = "SELECT guid, username, password, description, coins FROM public.\"user\" u INNER JOIN public.\"login_credentials\" lu ON u.guid=lu.guid;";
            NpgsqlCommand cmd = new(sql);
            using var reader = _db.ExecuteQuery(cmd);
            if (reader.Read())
            {
                users.Add(new User
                {
                    Guid = reader.GetGuid(0),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2),
                    Description = reader.GetString(3),
                    Coins = reader.GetInt32(4)
                });
            }
            reader.Close();

            return users;
        }

        public bool ExistsByName(String name)
        {
            bool result = false;
            string sql = "SELECT COUNT(*)>0 FROM public.\"login_credentials\" WHERE username=@name;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("name", name);
            var reader = _db.ExecuteQuery(cmd);
            if (reader.Read())
            {
                result = reader.GetBoolean(0);
                reader.Close();
            }

            return result;
        }

        public User? GetById(Guid id)
        {
            string sql = "SELECT u.guid, username, password, description, coins FROM public.\"user\" u INNER JOIN public.\"login_credentials\" lu ON u.guid=lu.guid WHERE u.guid = @id;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("id", id.ToString());
            using var reader = _db.ExecuteQuery(cmd);
            if (reader.Read())
            {
                var result = new User
                {
                    Guid = new Guid(reader.GetString(0)),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2),
                    Description = reader.GetString(3),
                    Coins = reader.GetInt32(4)
                };
                reader.Close();
                return result;
            }

            return null;
        }

        public bool IsAdmin(Guid id)
        {
            bool result = false;
            string sql = "SELECT COUNT(*)>0 FROM public.\"admin\" WHERE guid = @id;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("id", id.ToString());
            _db.ExecuteNonQuery(cmd);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                result = reader.GetBoolean(0);
                reader.Close();
            }

            return result;
        }

        public void AddDeck(User entity, ICard[] deck)
        {
            if (GetById(entity.Guid) == null)
                throw new UserNotFoundException();
            if (deck.Length != 4)
                throw new IndexOutOfRangeException();


        }

        public ICard[] GetDeckById(Guid userId)
        {
            if (GetById(userId) == null)
                throw new UserNotFoundException();
            string sqlUser = "SELECT cardid FROM deck WHEN ownerid=@id";
            NpgsqlCommand cmd = new(sqlUser);
            cmd.Parameters.AddWithValue("id", userId);
            
            using var reader = _db.ExecuteQuery(cmd);
            int i = 0;
            var cards = new ICard[4];
            while(reader.Read())
            {
                cards[i++] = _cardRepository.GetById(new Guid(reader.GetString(0)));
            }
            reader.Close(); 

            if (i >= 4) throw new IndexOutOfRangeException();

            return cards;
        }

        public void Add(User entity)
        {
            if (GetById(entity.Guid) != null || ExistsByName(entity.Username))
                throw new EntityAlreadyExistsException("User already Exists");

            string sqlUser = "INSERT INTO public.\"user\" (guid, description) VALUES (@id, @descr);"
                , id = entity.Guid.ToString();
            NpgsqlCommand cmd = new(sqlUser);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("descr", entity.Description);

            if (!_db.ExecuteNonQuery(cmd))
            {
                throw new Exception("Failed to register user: 1");
            }

            string sqlLogin = "INSERT INTO public.\"login_credentials\" (guid, username, password) VALUES (@id, @uname, @pw);";
            cmd = new(sqlLogin);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("uname", entity.Username);
            cmd.Parameters.AddWithValue("pw", entity.Password);
            if (!_db.ExecuteNonQuery(cmd))
            {
                throw new Exception("Failed to register user: 2");
            }

            //ToDo init other tables
        }

        public void Update(User updatedUser)
        {
            if(GetById(updatedUser.Guid) == null)
                throw new Exception("Failed to update user: User does not exists.");
            string sql = "UPDATE public.\"user\" SET description=@descr, coins=@coins WHERE guid = @id;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("id", updatedUser.Guid.ToString());
            cmd.Parameters.AddWithValue("coins", updatedUser.Coins);
            cmd.Parameters.AddWithValue("descr", updatedUser.Description);

            if (!_db.ExecuteNonQuery(cmd))
            {
                throw new Exception("Failed to update user: 1");
            }

            sql = "UPDATE public.\"login_credentials\" SET username=@uname, password=@passwd WHERE guid = @id;";
            cmd = new(sql);
            cmd.Parameters.AddWithValue("id", updatedUser.Guid.ToString());
            cmd.Parameters.AddWithValue("uname", updatedUser.Username);
            cmd.Parameters.AddWithValue("passwd", updatedUser.Password);

            if (!_db.ExecuteNonQuery(cmd))
            {
                throw new Exception("Failed to update user: 2");
            }
        }
    }
}