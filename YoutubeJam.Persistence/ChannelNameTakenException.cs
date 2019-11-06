using System;
using System.Runtime.Serialization;

namespace YoutubeJam.Persistence
{
    [Serializable]
    public class ChannelNameTakenException : Exception
    {
        public ChannelNameTakenException()
        {
        }

        public ChannelNameTakenException(string message) : base(message)
        {
        }

        public ChannelNameTakenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ChannelNameTakenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}