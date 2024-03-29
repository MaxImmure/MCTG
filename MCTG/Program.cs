﻿using System.Net;
using MCTG.BL.HTTP;
using MCTG.BL.HTTP.Endpoints;
using MCTG.DAL;

namespace MCTG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserRepository ur = new UserRepository();

            HttpServer server = new(IPAddress.Any, 10001);
            server.RegisterEndpoint("/users", new UsersEndpoint(server));
            server.RegisterEndpoint("/sessions", new SessionsEndpoint());
            //server.RegisterEndpoint("/tradings", new TradingsEndpoint());
            server.RegisterEndpoint("/packages", new PackagesEndpoint());
            server.RegisterEndpoint("/transactions/packages", new TransactionEndpoint());
            server.RegisterEndpoint("/cards", new CardEndpoint());
            server.RegisterEndpoint("/deck", new DeckEndpoint());
            server.RegisterEndpoint("/stats", new StatsEndpoint());
            server.RegisterEndpoint("/scoreboard", new ScoreboardEndpoint()); 
            server.RegisterEndpoint("/battles", new BattleEndpoint());

            var users = ur.GetAll();
            foreach (var user in users)
            {
                server.RegisterEndpoint("/users/" + user.Credentials.Username.ToLower(), new ProfileEndpoint(user));
            }
            //GetAllUsers

            //ToDo if there is no Endpoint Programm will Crash  
            server.Run();
        }
    }
}
