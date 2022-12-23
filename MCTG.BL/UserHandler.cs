using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MCTG.DAL;
using MCTG.Models;

namespace MCTG.BL
{
    public class UserHandler
    {
        public void RegisterUser(User newUser)
        {
            var dal = new DataLayerPostgres();
            //dal.CreateUser(newUser);
        }

        public void UnregisterUser(User userToDelete)
        {
            var dal = new DataLayerPostgres();
            //dal.DeleteUser(userToDelete);
        }
    }
}
