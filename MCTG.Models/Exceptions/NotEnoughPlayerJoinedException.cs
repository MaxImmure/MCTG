using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Exceptions
{
    public class NotEnoughPlayerJoinedException : ArgumentOutOfRangeException
    {
        public NotEnoughPlayerJoinedException() { }
        public NotEnoughPlayerJoinedException(string message) : base(message) { }
        public NotEnoughPlayerJoinedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
