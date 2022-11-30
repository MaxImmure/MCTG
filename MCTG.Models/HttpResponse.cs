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
        private StreamWriter writer;

        public string ResponseString { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseCodeText { get; set; }

        public HttpResponse(TcpClient socket)
        {
            this.socket = socket;
            writer = new StreamWriter(socket.GetStream()) { AutoFlush = true };
        }

        public void Send()
        {
            writer.WriteLine("HTTP/1.1 " + ResponseCode + " " + ResponseCodeText);
            writer.WriteLine("Content-Length: " + ResponseString.Length); //Später Header per dict
            writer.WriteLine("Content-Type: text/plain");
            writer.WriteLine();
            writer.WriteLine(ResponseString);
            writer.Flush();
        }
    }
}
