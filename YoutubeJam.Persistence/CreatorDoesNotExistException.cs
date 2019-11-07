using System;
using System.Runtime.Serialization;

namespace YoutubeJam.Persistence
{
    [Serializable]
    public class CreatorDoesNotExistException : Exception
    {
        public CreatorDoesNotExistException()
        {
        }

        public CreatorDoesNotExistException(string message) : base(message)
        {
        }

        public CreatorDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CreatorDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}