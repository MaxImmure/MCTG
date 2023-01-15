using System.Runtime.Serialization;

namespace MCTG.Models.Exceptions
{
    public class UserHasNoCardsException : Exception
    {
        public UserHasNoCardsException()
        {
        }

        public UserHasNoCardsException(string? message) : base(message)
        {
        }

        public UserHasNoCardsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserHasNoCardsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}