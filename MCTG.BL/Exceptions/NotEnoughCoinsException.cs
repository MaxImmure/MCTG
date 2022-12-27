using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.BL.Exceptions
{
    public class NotEnoughCoinsException : ArgumentOutOfRangeException
    {
        public NotEnoughCoinsException() { }
        public NotEnoughCoinsException(string message) : base(message) { }
        public NotEnoughCoinsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
