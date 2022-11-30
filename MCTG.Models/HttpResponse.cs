using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models
{
    public class HttpResponse
    {
        private TcpClient socket;
        public string StatusCode { get; private set; }
        public string HttpVersion { get; private set; }


        public HttpResponse(TcpClient socket)
        {
            this.socket = socket;
        }

    }
}
