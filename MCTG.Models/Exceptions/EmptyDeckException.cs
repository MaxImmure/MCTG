using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Exceptions
{
    public class EmptyDeckException : NullReferenceException
    {
        public EmptyDeckException() { }
        public EmptyDeckException(string message) : base(message) { }
        public EmptyDeckException(string message, Exception innerException) : base(message, innerException) { }
    }   
}
