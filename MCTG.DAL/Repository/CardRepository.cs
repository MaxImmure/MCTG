using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCTG.BL;
using MCTG.Models;
using MCTG.Models.Cards;
using Npgsql;

namespace MCTG.DAL.Repository
{
    public class CardRepository : IRepository<ICard>
    {
        private readonly Database _db = Database.Instance() ?? throw new InvalidOperationException();

        public CardRepository()
        { }

        public IEnumerable<ICard>? GetAll()
        {
            List<ICard> result = new();
            string sql = "SELECT element, type, cardname, damage, cardid, ownerid FROM public.\"card\";";
            NpgsqlCommand cmd = new(sql);
            var reader = _db.ExecuteQuery(cmd);
            if (reader.Read())
            {
                ElementType etype;
                CardType ctype;
                Enum.TryParse(reader.GetString(0), out etype);
                Enum.TryParse(reader.GetString(1), out ctype);
                string cardname = reader.GetString(2);
                double dmg = reader.GetDouble(3);
                Guid cardId = new Guid(reader.GetString(4))
                    , ownerId = new Guid(reader.GetString(5));
                var card = CardFactory.GetCard(ctype, etype, cardname, dmg, cardId, ownerId);
                if(card != null) result.Add(card);
            }
            reader.Close();
            return result;
        }

        public IEnumerable<ICard>? GetAllFromUserId(Guid id)
        {
            List<ICard> result = new();
            string sql = "SELECT cardid, element, type, cardname, damage FROM public.\"card\" WHERE ownerid = @id;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("id", id.ToString());
            var reader = _db.ExecuteQuery(cmd);
            while (reader.Read())
            {
                ElementType etype;
                CardType ctype;
                Guid cardId = new Guid(reader.GetString(0));
                Enum.TryParse(reader.GetString(1), out etype);
                Enum.TryParse(reader.GetString(2), out ctype);
                string cardname = reader.GetString(3);
                double dmg = reader.GetDouble(4);
                Guid ownerId = id;
                var card = CardFactory.GetCard(ctype, etype, cardname, dmg, cardId, id);
                if (card != null) result.Add(card);
            }
            reader.Close();
            return result;
        }

        public ICard? GetById(Guid id)
        {
            string sql = "SELECT element, type, cardname, damage, ownerid FROM public.\"card\" where cardid = @id;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("id", id.ToString());
            var reader = _db.ExecuteQuery(cmd);
            if (reader.Read())
            {
                ElementType etype;
                CardType ctype;
                Enum.TryParse(reader.GetString(0), out etype);
                Enum.TryParse(reader.GetString(1), out ctype);
                string cardname = reader.GetString(2);
                double dmg = reader.GetDouble(3);
                Guid ownerId = new Guid(reader.GetString(4));

                reader.Close();
                return CardFactory.GetCard(ctype, etype, cardname, dmg, id, ownerId);
            }
            reader.Close();
            return null;
        }

        public void Add(ICard entity)
        {
            if (GetById(entity.GetId()) != null)
                throw new Exception(); //ToDo //EntityAlreadyExistsException();
            string sql = "INSERT INTO card (cardid, ownerid, element, type, cardname, damage) VALUES (@id, @owner, @element, @type, @cardname, @dmg);";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("id", entity.GetId().ToString());
            cmd.Parameters.AddWithValue("owner", entity.GetOwner().ToString());
            cmd.Parameters.AddWithValue("dmg", entity.GetBaseDamage());
            var split = entity.ToSqlString().Split(";");
            cmd.Parameters.AddWithValue("element", split[0]);
            cmd.Parameters.AddWithValue("type", split[1]);
            cmd.Parameters.AddWithValue("cardname", split[2]);

            if (!_db.ExecuteNonQuery(cmd))
            {
                throw new ("Adding new Card to Database failed.");
            }
        }

        public void Update(ICard entity)
        {
            if (GetById(entity.GetId()) == null)
                throw new Exception("Card not exists."); //ToDo //EntityAlreadyExistsException();
            string sql = "UPDATE public.\"card\" SET ownerid=@owid WHERE cardid = @id;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("owid", entity.GetOwner().ToString());
            cmd.Parameters.AddWithValue("id", entity.GetId().ToString());
            if (!_db.ExecuteNonQuery(cmd))
            {
                throw new("Updating Card to Database failed.");
            }
        }

        public void Delete(ICard entity)
        {
            if (GetById(entity.GetId()) == null)
                return;
            string sql = "DELETE FROM public.\"card\" WHERE cardid = @id;";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("id", entity.GetId().ToString());
            if (!_db.ExecuteNonQuery(cmd))
            {
                throw new("Deleting Card to Database failed.");
            }
            //Check if is in Deck table
        }
    }
}
