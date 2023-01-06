using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using MCTG.BL;
using MCTG.BL.HTTP;
using MCTG.Models.HTTP.Endpoints;

namespace MCTG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HttpServer server = new(IPAddress.Any, 10001);
            server.RegisterEndpoint("/users", new UsersEndpoint());
            //server.RegisterEndpoint("/users/MaxImmure", new UsersEndpoint());
            //ToDo if there is no Endpoint Programm will Crash
            server.Run();
        }
    }
}
