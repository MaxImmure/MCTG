using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Exceptions
{
    public class CardBelongsToAnotherUserException : Exception
    {
        public CardBelongsToAnotherUserException() { }
        public CardBelongsToAnotherUserException(string message) : base(message) { }
        public CardBelongsToAnotherUserException(string message, Exception innerException) : base(message, innerException) { }
    }   
}
