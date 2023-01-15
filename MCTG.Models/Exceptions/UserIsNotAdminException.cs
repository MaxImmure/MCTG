using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Exceptions
{
    public class UserIsNotAdminException : DuplicateNameException
    {
        public UserIsNotAdminException() { }
        public UserIsNotAdminException(string message) : base(message) { }
        public UserIsNotAdminException(string message, Exception innerException) : base(message, innerException) { }
    }
}
