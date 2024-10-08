using System;

namespace CopilotLab
{
    public class DuplicateAlbumException : Exception
    {
        public DuplicateAlbumException(string message) : base(message)
        {
        }
    }
}
