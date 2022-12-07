using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.HTTP
{
    public class HttpResponse
    {
        private StreamWriter writer;

        public int ResponseCode { get; set; }
        public string ResponseText { get; set; }

        public Dictionary<string, string> Headers { get; set; }
        public string Content { get; set; }

        public HttpResponse(StreamWriter writer)
        {
            this.writer = writer;
            Headers = new();
        }

        public void Process()
        {
            writer.WriteLine($"HTTP/1.1 {ResponseCode} {ResponseText}");
            
            if (Content != null && Content.Length > 0)
                Headers.Add("Content-Length", Content.Length.ToString());

            Headers.ToList().ForEach(kvp => writer.WriteLine($"{kvp.Key}: {kvp.Value}"));                
            
            if (Content != null && Content.Length > 0)
                writer.WriteLine(Content);
            
            writer.Flush();
            writer.Close();
        }
    }
}
