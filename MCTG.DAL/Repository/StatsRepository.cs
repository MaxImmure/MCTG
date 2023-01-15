using System.Xml;
using Npgsql;
using MCTG.BL;
using MCTG.DAL.Repository;
using MCTG.Models;
using MCTG.Models.Cards;
using MCTG.Models.Exceptions;

namespace MCTG.DAL
{
    public class StatsRepository : IRepository<Stats>
    {
        private readonly Database _db = Database.Instance() ?? throw new InvalidOperationException();

        private CardRepository _cardRepository = new();

        public void Delete(Stats stats)
        {
            if (GetById(stats.PlayerId) == null)
                return;
            string sql = "DELETE FROM public.\"userstats\" WHERE guid = @id;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("id", stats.PlayerId.ToString());

            if (!_db.ExecuteNonQuery(cmd))
            {
                throw new UserNotFoundException("Failed to Delete user! There is no User with that ID");
            }
        }

        public IEnumerable<Stats>? GetAll()
        {
            List<Stats> stats = new();
            const string sql = "SELECT u.guid, username, win, looses, draws, elo FROM public.\"userstats\" u INNER JOIN public.\"login_credentials\" lu ON u.guid=lu.guid;";
            NpgsqlCommand cmd = new(sql);
            using var reader = _db.ExecuteQuery(cmd);
            while (reader.Read())
            {
                stats.Add(new Stats
                {
                    PlayerId = new Guid(reader.GetString(0)),
                    username = reader.GetString(1),
                    wins = reader.GetInt32(2),
                    looeses = reader.GetInt32(3),
                    draws = reader.GetInt32(4),
                    elo = reader.GetInt32(5)
                });
            }
            reader.Close();
            return stats;
        }

        public Stats? GetById(Guid id)
        {
            const string sql = "SELECT u.guid, username, win, lose, draw, elo FROM public.\"userstats\" u INNER JOIN public.\"login_credentials\" lu ON u.guid=lu.guid WHERE u.guid = @id;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("id", id.ToString());
            using var reader = _db.ExecuteQuery(cmd);
            if (!reader.Read()) return null;
            var result = new Stats
            {
                PlayerId = new Guid(reader.GetString(0)),
                username = reader.GetString(1),
                wins = reader.GetInt32(2),
                looeses = reader.GetInt32(3),
                draws = reader.GetInt32(4),
                elo = reader.GetInt32(5)
            };
            reader.Close();
            return result;
        }

        public void Add(Stats entity)
        {
            if (GetById(entity.PlayerId) != null)
                throw new EntityAlreadyExistsException("Stats already Exists");

            const string sqlUser = "INSERT INTO public.\"userstats\" (guid) VALUES (@id);";
            var id = entity.PlayerId.ToString();
            NpgsqlCommand cmd = new(sqlUser);
            cmd.Parameters.AddWithValue("id", entity.PlayerId);

            if (!_db.ExecuteNonQuery(cmd))
            {
                throw new Exception("Failed to setup userstats");
            }
        }

        public void Update(Stats updatedUser)
        {
            if (GetById(updatedUser.PlayerId) == null)
                throw new Exception("Failed to update stats: User does not exists.");
            const string sql = "UPDATE public.\"userstats\" SET elo=@elo, win=@win, lose=@lose, draw=@draw WHERE guid = @id;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("elo", updatedUser.elo);
            cmd.Parameters.AddWithValue("lose", updatedUser.looeses);
            cmd.Parameters.AddWithValue("draw", updatedUser.draws);
            cmd.Parameters.AddWithValue("win", updatedUser.wins);
            cmd.Parameters.AddWithValue("id", updatedUser.PlayerId.ToString());


            if (!_db.ExecuteNonQuery(cmd))
            {
                throw new Exception("Failed to update user: 1");
            }
        }
    }
}