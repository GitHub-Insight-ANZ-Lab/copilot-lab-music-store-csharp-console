using System;

namespace MusicStore
{
    public class DuplicateAlbumException : Exception
    {
        public DuplicateAlbumException(string message) : base(message)
        {
        }
    }
}
