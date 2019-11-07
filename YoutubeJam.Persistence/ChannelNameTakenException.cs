using System;

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
    }
}