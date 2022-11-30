using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models
{
    public class HttpRequest
    {
        private TcpClient socket;
        public string Url { get; }  
        public string HttpMethod { get; }
        public string[] UrlSegments { get { return Url.Split("/").Skip(1).ToArray(); } }

        public HttpRequest(TcpClient socket)
        {
            this.socket = socket;
            using var reader = new StreamReader(socket.GetStream());
            //Fehlerbehandlung
            var httpParts = reader.ReadLine().Split(" ");
            HttpMethod = httpParts[0];
            Url = httpParts[1];
            //Header & Request Content ?
        }
    }
}
