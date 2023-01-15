using System.Runtime.Serialization;

namespace MCTG.Models.Exceptions
{
    public class InvalidLoginCredentialsException : Exception
    {
        public InvalidLoginCredentialsException()
        {
        }

        public InvalidLoginCredentialsException(string? message) : base(message)
        {
        }

        public InvalidLoginCredentialsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidLoginCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}