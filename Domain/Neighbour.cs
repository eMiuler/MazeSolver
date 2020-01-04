using Common;

namespace Domain
{
    public class Neighbour
    {
        public Path Tile { get; }
        public Directions Direction { get; }

        public Neighbour(Path path, Directions direction)
        {
            Tile = path;
            Direction = direction;
        }
    }
}
