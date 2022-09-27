using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.BL
{
    class Database
    {
        private static Database _instance = null;

        private Database()
        {

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
