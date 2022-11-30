using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using MCTG.BL;

namespace MCTG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = new();
            server.Run();
        }
    }
}
