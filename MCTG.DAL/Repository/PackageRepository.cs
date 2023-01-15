using MCTG.BL;
using MCTG.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCTG.Models.Cards;
using System.Xml.Linq;

namespace MCTG.DAL.Repository
{
    public class PackageRepository : IRepository<Package>
    {
        private readonly Database _db = Database.Instance() ?? throw new InvalidOperationException();
        private CardRepository _cardRepository = new CardRepository();

        public void Add(Package entity)
        {
            foreach (var card in entity.Cards)
            {
                string sql = "INSERT INTO package (p_id, cardid) VALUES (@id, @card);";
                NpgsqlCommand cmd = new(sql);
                cmd.Parameters.AddWithValue("id", entity.p_id);
                cmd.Parameters.AddWithValue("card", card.GetId());
                _db.ExecuteNonQuery(cmd);
            }
        }

        public void Delete(Package entity)
        {
            string sql = "DELETE FROM package WHERE p_id = @pid;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("pid", entity.p_id.ToString());
            _db.ExecuteNonQuery(cmd);
        }

        public Package? GetNext()
        {
            string sql = "SELECT p_id FROM package LIMIT 1;";
            NpgsqlCommand cmd = new(sql);
            using var reader = _db.ExecuteQuery(cmd);
            if (reader.Read())
            {
                var id = new Guid(reader.GetString(0));
                reader.Close();
                return GetById(id);
            }

            return null;
        }

        public IEnumerable<Package>? GetAll()
        {
            throw new NotImplementedException();
        }

        public Package? GetById(Guid id)
        {
            int i = 0;
            string sql = "SELECT cardid FROM package WHERE p_id=@pid;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("pid", id.ToString());
            using var reader = _db.ExecuteQuery(cmd);

            var result = new Package() {p_id = id};
            List<Guid> cards = new();
            while (reader.Read())
            {
                cards.Add(new Guid(reader.GetString(0)));
            }
            reader.Close();

            foreach (var card in cards)
            {
                result.Cards[i++] = _cardRepository.GetById(card);
            }
             //ToDo all reader outside
            return result;
        }

        public void Update(Package entity)
        {
            throw new NotImplementedException();
        }
    }
}
