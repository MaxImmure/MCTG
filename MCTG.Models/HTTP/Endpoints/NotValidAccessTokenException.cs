using System.Runtime.Serialization;

namespace MCTG.Models.HTTP.Endpoints
{
    [Serializable]
    internal class NotValidAccessTokenException : Exception
    {
        public NotValidAccessTokenException()
        {
        }

        public NotValidAccessTokenException(string? message) : base(message)
        {
        }

        public NotValidAccessTokenException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotValidAccessTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}