using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.HTTP
{
    public class HttpRequest
    {
        private StreamReader reader;

        public HttpMethod HttpMethod { get; private set; }
        public string[] Path { get; private set; }

        public Dictionary<string, string> QueryParams = new();
        public string ProtocolVersion { get; set; }

        public Dictionary<string, string> headers = new();

        public string Content { get; private set; }

        public HttpRequest(StreamReader reader)
        {
            this.reader = reader;
        }

        public void Parse()
        {

        }
    }
}
