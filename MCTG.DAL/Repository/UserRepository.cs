using System.Data.SqlTypes;
using System.Xml;
using Npgsql;
using MCTG.BL;
using MCTG.DAL.Repository;
using MCTG.Models;
using MCTG.Models.Cards;
using MCTG.Models.Exceptions;
using Npgsql.Replication;

namespace MCTG.DAL
{
    public class UserRepository : IRepository<User>
    {
        private readonly Database _db = Database.Instance() ?? throw new InvalidOperationException();

        private readonly CardRepository _cardRepository = new();
        private readonly StatsRepository _statsRepository = new();

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

        public IEnumerable<User> GetScoreboard()
        {
            var allUser = GetAll();
            return allUser.OrderByDescending(x => x.GameStats.elo);
        }

        public Guid GetIdFromUsername(string username)
        {
            if (!ExistsByName(username))
                throw new UserNotFoundException();
            const string sql = "SELECT u.guid FROM public.\"user\" u INNER JOIN public.\"login_credentials\" lc ON u.guid=lc.guid WHERE username = @name;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("name", username);

            using var reader = _db.ExecuteQuery(cmd);
            
            if (!reader.Read()) return Guid.Empty;
            
            var result = new Guid(reader.GetString(0));
            
            reader.Close();
            
            return result;
        }

        public User? LoginUser(LoginCredentials loginCredentials)
        {
            const string sql = "SELECT guid FROM public.\"login_credentials\" WHERE username=@uname AND password=@pw;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("uname", loginCredentials.Username);
            cmd.Parameters.AddWithValue("pw", loginCredentials.Password);

            var reader = _db.ExecuteQuery(cmd);
            if (!reader.Read()) { reader.Close(); return null;}
            var result = reader.GetString(0);
            reader.Close();
            return GetById(new Guid(result));

        }

        public IEnumerable<User> GetAll()
        {
            List<User> users = new();
            const string sql = "SELECT u.guid, username, password, name, description, coins, win, lose, draw, elo  FROM public.\"user\" u INNER JOIN public.\"login_credentials\" lu ON u.guid=lu.guid INNER JOIN public.\"userstats\" us ON u.guid=us.guid;";
            NpgsqlCommand cmd = new(sql);
            using var reader = _db.ExecuteQuery(cmd);  
            while (reader.Read())
            {
                users.Add(new User
                {
                    Guid = new Guid(reader.GetString(0)),
                    Credentials = new LoginCredentials(
                        reader.GetString(1),
                        reader.GetString(2)
                        ),
                    Name = reader.GetString(3),
                    Description = reader.GetString(4),
                    Coins = reader.GetInt32(5),
                    GameStats = new Stats()
                    {
                        wins = reader.GetInt32(6),
                        looeses = reader.GetInt32(7),
                        draws = reader.GetInt32(8),
                        elo = reader.GetInt32(9)
                    }
                });
            }
            reader.Close();

            return users;
        }

        public bool ExistsByName(string name)
        {
            var exists = false;
            const string sql = "SELECT COUNT(*) > 0 FROM public.\"login_credentials\" WHERE username=@uname;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("uname", name);
            using var reader = _db.ExecuteQuery(cmd);
            if (reader.Read())
            {
                exists = reader.GetBoolean(0);
            }
            reader.Close();
            return exists;
        }

        public User? GetById(Guid id)
        {
            const string sql = "SELECT u.guid, username, password, name, description, coins, win, lose, draw, elo  FROM public.\"user\" u INNER JOIN public.\"login_credentials\" lu ON u.guid=lu.guid INNER JOIN public.\"userstats\" us ON u.guid=us.guid WHERE u.guid = @id;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("id", id.ToString());
            using var reader = _db.ExecuteQuery(cmd);
            if (!reader.Read()) { reader.Close(); return null;}

            string username = reader.GetString(1);
            var Guid = new Guid(reader.GetString(0));
            var result = new User
            {
                Guid = Guid,
                Credentials = new LoginCredentials(
                    username,
                    reader.GetString(2)
                ),
                Name = reader.GetString(3),
                Description = reader.GetString(4),
                Coins = reader.GetInt32(5),
                GameStats = new Stats()
                {
                    username = username,
                    PlayerId = Guid,
                    wins = reader.GetInt32(6),
                    looeses = reader.GetInt32(7),
                    draws = reader.GetInt32(8),
                    elo = reader.GetInt32(9)
                }
            };
            reader.Close();
            result.Deck = GetDeck(result);
            return result;

        }

        public bool IsAdmin(Guid id)
        {
            var result = false;
            const string sql = "SELECT COUNT(*)>0 FROM public.\"admin\" WHERE guid = @id;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("id", id.ToString());
            _db.ExecuteNonQuery(cmd);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return result;
            
            result = reader.GetBoolean(0);
            reader.Close();

            return result;
        }

        public void AddDeck(User entity)
        {
            if (GetById(entity.Guid) == null)
                throw new UserNotFoundException();
            if (entity.Deck.Length != 4)
                throw new IndexOutOfRangeException();

            if (entity.Deck[0] != null)
            {
                foreach (var card in entity.Deck)
                {
                    if (!entity.Guid.Equals(card.GetOwner())) throw new CardBelongsToAnotherUserException();
                }
            }

            foreach (var card in entity.Deck)
            {
                string sql = "INSERT INTO deck (cardid, ownerid) VALUES (@cid, @oid);";
                NpgsqlCommand cmd = new(sql);
                cmd.Parameters.AddWithValue("oid", entity.Guid.ToString());
                cmd.Parameters.AddWithValue("cid", card.GetId().ToString());
                if (!_db.ExecuteNonQuery(cmd))
                {
                    throw new Exception("Failed to add/update deck");
                }
            }
        }

        public void DeleteDeck(User entity)
        {
            if (GetById(entity.Guid) == null)
                return;

            string sql = "DELETE FROM deck WHERE ownerid = @id;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("id", entity.Guid.ToString());

            if (!_db.ExecuteNonQuery(cmd))
            {
                throw new Exception("Failed to delete deck");
            }
        }

        public bool Exists(Guid guid)
        {
            string sql = "SELECT COUNT(*)>0 FROM public.\"user\" WHERE guid = @id;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("id", guid.ToString());
            using var reader = _db.ExecuteQuery(cmd);
            if (!reader.Read()) return false;
            bool result = reader.GetBoolean(0);
            reader.Close();
            return result;
        }

        public ICard[] GetDeck(User entity)
        {
            if (!Exists(entity.Guid))
                throw new UserNotFoundException();

            string sql = "SELECT COUNT(*) FROM deck WHERE ownerid = @id;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("id", entity.Guid.ToString());

            using var reader = _db.ExecuteQuery(cmd);
            if (!reader.Read()) return null;
            int max = reader.GetInt32(0);
            reader.Close();
            if (max != 4) return null;
            sql = "SELECT cardid FROM deck WHERE ownerid = @id;";
            cmd = new(sql);
            cmd.Parameters.AddWithValue("id", entity.Guid.ToString());

            var reader2 = _db.ExecuteQuery(cmd);
            if (!reader2.Read()) return null;
            List<Guid> cid = new();
            for (int i = 0; i < max; i++)
            {
                cid.Add(new Guid(reader2.GetString(0)));
            }
            reader2.Close();

            List<ICard> newDeck = new();
            foreach (var ids in cid)
            {
                newDeck.Add(_cardRepository.GetById(ids));
            }

            return newDeck.ToArray();
        }

        public void Add(User entity)
        {
            if (GetById(entity.Guid) != null || ExistsByName(entity.Credentials.Username))
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
            cmd.Parameters.AddWithValue("uname", entity.Credentials.Username);
            cmd.Parameters.AddWithValue("pw", entity.Credentials.Password);
            if (!_db.ExecuteNonQuery(cmd))
            {
                throw new Exception("Failed to register user: 2");
            }

            string sqlStats = "INSERT INTO public.\"userstats\" (guid) VALUES (@id);";
            cmd = new(sqlStats);
            cmd.Parameters.AddWithValue("id", id);
            if (!_db.ExecuteNonQuery(cmd))
            {
                throw new Exception("Failed to register user: 3");
            }
        }

        public void Update(User updatedUser)
        {
            if(GetById(updatedUser.Guid) == null)
                throw new Exception("Failed to update user: User does not exists.");
            var sql = "UPDATE public.\"user\" SET name=@name, description=@descr, coins=@coins WHERE guid = @id;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("id", updatedUser.Guid.ToString());
            cmd.Parameters.AddWithValue("coins", updatedUser.Coins);
            cmd.Parameters.AddWithValue("name", updatedUser.Name);
            cmd.Parameters.AddWithValue("descr", updatedUser.Description==null?"":updatedUser.Description);

            if (!_db.ExecuteNonQuery(cmd))
            {
                throw new Exception("Failed to update user: 1");
            }

            sql = "UPDATE public.\"login_credentials\" SET username=@uname, password=@passwd WHERE guid = @id;";
            cmd = new(sql);
            cmd.Parameters.AddWithValue("id", updatedUser.Guid.ToString());
            cmd.Parameters.AddWithValue("uname", updatedUser.Credentials.Username);
            cmd.Parameters.AddWithValue("passwd", updatedUser.Credentials.Password);

            if (!_db.ExecuteNonQuery(cmd))
            {
                throw new Exception("Failed to update user: 2");
            }

            _statsRepository.Update(updatedUser.GameStats);
            if (updatedUser.Deck != null && updatedUser.Deck[0] != null)
                UpdateDeck(updatedUser);
        }

        public void UpdateDeck(User user)
        {
            DeleteDeck(user);
            AddDeck(user);
        }

        public User Add(LoginCredentials userC)
        {
            var entity = new User(Guid.NewGuid(), userC.Username, userC.Password, userC.Username);
            if (GetById(entity.Guid) != null || ExistsByName(entity.Credentials.Username))
                throw new EntityAlreadyExistsException("User already Exists");

            const string sqlUser = "INSERT INTO public.\"user\" (guid, description, \"name\") VALUES (@id, @descr, @fname);";
            NpgsqlCommand cmd = new(sqlUser);
            cmd.Parameters.AddWithValue("id", entity.Guid.ToString());
            cmd.Parameters.AddWithValue("fname", entity.Name.ToString());
            cmd.Parameters.AddWithValue("descr", string.Empty);

            if (!_db.ExecuteNonQuery(cmd))
            {
                throw new Exception("Failed to register user: 1");
            }

            string sqlLogin = "INSERT INTO public.\"login_credentials\" (guid, username, password) VALUES (@id, @uname, @pw);";
            cmd = new(sqlLogin);
            cmd.Parameters.AddWithValue("id", entity.Guid.ToString());
            cmd.Parameters.AddWithValue("uname", entity.Credentials.Username);
            cmd.Parameters.AddWithValue("pw", entity.Credentials.Password);
            if (!_db.ExecuteNonQuery(cmd))
            {
                throw new Exception("Failed to register user: 2");
            }

            string sqlStats = "INSERT INTO public.\"userstats\" (guid) VALUES (@id);";
            cmd = new(sqlStats);
            cmd.Parameters.AddWithValue("id", entity.Guid.ToString());
            if (!_db.ExecuteNonQuery(cmd))
            {
                throw new Exception("Failed to register user: 3");
            }

            return GetById(entity.Guid);
        }
    }
}