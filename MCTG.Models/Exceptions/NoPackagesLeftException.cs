using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Exceptions
{
    public class NoPackagesLeftException : DuplicateNameException
    {
        public NoPackagesLeftException() { }
        public NoPackagesLeftException(string message) : base(message) { }
        public NoPackagesLeftException(string message, Exception innerException) : base(message, innerException) { }
    }
}
