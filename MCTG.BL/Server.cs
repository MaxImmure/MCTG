using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace MCTG.BL
{
    class Server
    {
        public IPAddress host { get; }
        public int port { get; }

        public Server(IPAddress host, int port = 80)
        {
            this.host = host;
            this.port = port;
        }

        public void Run()
        {
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(host, port);

            TcpListener listener = new TcpListener(IPAddress.Any, port);

            //ToDo Serverhandling
            listener.Start();

            for (; ; )
            {
                Socket client = null;

                try
                {
                    client = listener.AcceptSocket();

                    Console.Write("Handling client at " + client.RemoteEndPoint + " - ");

                    //ToDo Magic

                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    client.Close();
                }
            }

            tcpClient.Close();
        }

    }

}
