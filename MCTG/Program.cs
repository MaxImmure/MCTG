using System.Net;
using MCTG.BL.HTTP;
using MCTG.BL.HTTP.Endpoints;

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
            server.RegisterEndpoint("/transactions/packages", new TransactionEndpoint());
            server.RegisterEndpoint("/cards", new CardEndpoint());
            server.RegisterEndpoint("/deck", new DeckEndpoint());
            //server.RegisterEndpoint("/stats", new Endpoint());
            //server.RegisterEndpoint("/scoreboard", new Endpoint());
            //server.RegisterEndpoint("/battles", new Endpoint());

            //ToDo if there is no Endpoint Programm will Crash  
            server.Run();
        }
    }
}
