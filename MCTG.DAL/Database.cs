using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Npgsql;
using Npgsql.Replication.PgOutput.Messages;
using NpgsqlTypes;

namespace MCTG.BL
{
    class Database
    {
        private static Database _instance = null;
        private IDbConnection connection;

        private Database()
        {
            //ToDo extract connection String    
            connection = new NpgsqlConnection($"Host=localhost;Username={"swe1user"};Password={"swe1pw"};Database={"mctg_game"}");
        }

        public static Database getInstance()
        {
            if (_instance == null)
                _instance = new Database();
            return _instance;
        }

        public void query(string query)
        {
            //ToDo
        }
    }
}
