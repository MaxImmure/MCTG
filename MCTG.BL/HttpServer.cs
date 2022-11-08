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
        private TcpListener listener;

        //DAtabase

        public Server(int port = 10001)
        {
            this.port = port;
            host = IPAddress.Any;
            listener = new TcpListener(IPAddress.Any, port);
        }

        public async void Run()
        {
            //db.connect();
            listener.Start();

            for (; ; )
            {
                try
                {
                    var client = await listener.AcceptTcpClientAsync();


                    Console.Write("Handling client at " + client.Client.RemoteEndPoint + " - ");

                    Task.Run(() => {
                        //ToDo Magic
                    });

                    //client.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

    }

}
