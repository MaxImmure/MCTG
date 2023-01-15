using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.BL
{
    class Database
    {
        private static Database? _instance;
        private static readonly object SyncRoot = new object();

        private readonly NpgsqlConnection _connection; //IDbConnection _connection => CAN NULL

        private Database()
        {
            _connection = new NpgsqlConnection(
                "Host=localhost;Username=swe1user;Password=swe1pw;Database=mctg_db"
                ); // ToDo extract the String instead of hardcode, settings -> gitignore Environment value
            _connection.Open();
        }

        public static Database? Instance()
        {
            return _instance ??= new();//late-binding
        }

        public bool ExecuteNonQuery(NpgsqlCommand cmd)
        {
            lock (SyncRoot)
            {
                cmd.Connection = _connection;
                if (cmd.ExecuteNonQuery() == -1)
                    return false;

                return true;
            }
        }

        public NpgsqlDataReader ExecuteQuery(NpgsqlCommand cmd)
        {
            lock (SyncRoot)
            {
                cmd.Connection = _connection;
                return cmd.ExecuteReader();
            }
        }
    }
}
