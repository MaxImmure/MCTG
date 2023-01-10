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
            server.RegisterEndpoint("/sessions", new SessionsEndpoint());
            server.RegisterEndpoint("/tradings", new TradingsEndpoint());
            server.RegisterEndpoint("/packages", new PackagesEndpoint());
            //server.RegisterEndpoint("/transactions/packages", new Endpoint());
            //server.RegisterEndpoint("/cards", new Endpoint());
            //server.RegisterEndpoint("/deck", new Endpoint());
            //server.RegisterEndpoint("/stats", new Endpoint());
            //server.RegisterEndpoint("/scoreboard", new Endpoint());
            //server.RegisterEndpoint("/battles", new Endpoint());

            //ToDo if there is no Endpoint Programm will Crash  
            server.Run();
        }
    }
}
