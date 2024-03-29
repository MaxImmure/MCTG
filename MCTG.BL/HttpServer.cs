﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MCTG.Models.HTTP;

namespace MCTG.BL.HTTP
{
    public class HttpServer
    {
        public IPAddress host { get; }
        public int port { get; }
        private TcpListener listener;
        public Dictionary<string, IHttpEndpoint> Endpoints { get; private set; } = new();

        public HttpServer(IPAddress host, int port = 10001)
        {
            this.port = port;
            this.host = host;
            listener = new TcpListener(host, port);
        }

        public HttpServer(int port = 10001) : this(IPAddress.Any, port)
        {}

        public void RegisterEndpoint(string path, IHttpEndpoint endpoint) => Endpoints.Add(path, endpoint);

        public void Run()
        {
            listener.Start();

            for (;;)
            {
                Console.WriteLine("Waiting for new client request...");
                var clientSocket = listener.AcceptTcpClient();
                var httpProcessor = new HttpProcessor(this, clientSocket);
                Task.Factory.StartNew(() => httpProcessor.Run());
                /*
                Task.Run(() =>
                {
                    //NonFunctional Sockets
                    HttpRequest request = new(socket.GetStream().);
                    HttpResponse response = new(socket.GetStream());
                    string responseString = "Unknown request";

                    string[] strParams = request.UrlSegments;

                    if (request.HttpMethod == "GET")
                    {
                        Console.WriteLine("Received a GET request");

                        if (strParams[0] == "users")
                        {
                            if (strParams.Length > 1)
                            {
                                responseString = $"Retrieves the user data for the given username ({strParams[1]})";
                            }
                        }
                        else if (strParams[0] == "cards")
                        {
                            responseString = "Shows a user's cards";
                        }
                        else if (strParams[0] == "deck")
                        {
                            responseString = "Shows the user's currently configured deck";
                        }
                        else if (strParams[0] == "stats")
                        {
                            responseString = "Retrieves the stats for an individual user";
                        }
                        else if (strParams[0] == "scoreboard")
                        {
                            responseString = "Retrieves the user scoreboard ordered by the user's ELO.";
                        }
                        else if (strParams[0] == "tradings")
                        {
                            responseString = "Retrieves the currently available trading deals.";
                        }
                    }

                    if (request.HttpMethod == "POST")
                    {
                        Console.WriteLine("Received a POST request");

                        if (strParams[0] == "users")
                        {
                            responseString = "Register a new user";
                        }
                        else if (strParams[0] == "sessions")
                        {
                            responseString = "Login with existing user";
                        }
                        else if (strParams[0] == "packages")
                        {
                            responseString = "Create new card packages (requires admin)";
                        }
                        else if (strParams[0] == "transactions")
                        {
                            if (strParams[1] == "packages")
                            {
                                responseString = "Acquire a package";
                            }
                        }
                        else if (strParams[0] == "battles")
                        {
                            responseString = "Enters the lobby to start a battle";
                        }
                        else if (strParams[0] == "tradings")
                        {
                            if (strParams.Length > 1)
                            {
                                responseString = $"Carry out a trade for the deal with the provided card. ID ({strParams[1]})";
                            }
                            else
                            {
                                responseString = "Creates a new trading deal.";
                            }
                        }
                    }

                    if (request.HttpMethod == "PUT")
                    {
                        Console.WriteLine("Received a PUT request");

                        if (strParams[0] == "users")
                        {
                            responseString = "Updates the user data for the given username.";
                        }
                        else if (strParams[0] == "deck")
                        {
                            responseString = "Configures the deck with four provided cards";
                        }
                    }

                    if (request.HttpMethod == "DELETE")
                    {
                        if (strParams[0] == "tradings")
                        {
                            if (strParams.Length > 1)
                            {
                                responseString = $"Deletes an existing trading deal. ID: {strParams[1]}";
                            }
                        }
                    }

                });*/

            }

        }
    }
}
