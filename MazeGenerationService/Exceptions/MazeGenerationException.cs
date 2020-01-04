using System;

namespace MazeGenerationService.Exceptions
{
    [Serializable]
    public class MazeGenerationException : Exception
    {
        public MazeGenerationException()
        {
        }

        public MazeGenerationException(string message)
            : base(message)
        {
        }

        public MazeGenerationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
