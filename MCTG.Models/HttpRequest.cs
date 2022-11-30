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
        public string Url { get; private set; }
        public string HttpMethod { get; private set; }
        public string ContentType { get; private set; }

        public HttpRequest(TcpClient socket)
        {
            this.socket = socket;
        }

    }
}
