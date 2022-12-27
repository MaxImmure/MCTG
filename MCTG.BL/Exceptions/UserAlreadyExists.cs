using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.BL.Exceptions
{
    public class UserAlreadyExists : DuplicateNameException
    {
        public UserAlreadyExists() { }
        public UserAlreadyExists(string message) : base(message) { }
        public UserAlreadyExists(string message, Exception innerException) : base(message, innerException) { }
    }
}
