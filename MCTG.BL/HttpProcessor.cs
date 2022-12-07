﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MCTG.Models.HTTP;

namespace MCTG.BL
{
    public class HttpProcessor
    {
        private TcpClient client;
        private HttpServer httpServer;

        public HttpProcessor(HttpServer httpServer, TcpClient client)
        {
            this.client = client;
            this.httpServer = httpServer;
        }

        public void run()
        {
            var reader = new StreamReader(client.GetStream());
            var request = new HttpRequest(reader);
            request.Parse();
            var writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
            var response = new HttpResponse(writer);

            var endpoint = httpServer.Endpoints[request.Path[1]];
            if(endpoint != null)
                endpoint.HandleRequest(request, response);
            else
            {
                response.ResponseCode = 404;
                response.ResponseText = "Not Found";
                response.Content = "<html><body>Not Found!</body></html>";
                response.Headers.Add("Content-Length", response.Content.Length.ToString());
                response.Headers.Add("Content-Type", "text/plain");
            }
            response.Process();
        }
    }
}
