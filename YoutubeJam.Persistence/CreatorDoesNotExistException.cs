using System;

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
    }
}