using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Path : Tile
    {
        public IReadOnlyList<Neighbour> UnvisitedNeighbours => _neighbours
            .Where(x => !x.Tile.IsVisited).ToList();

        public IReadOnlyList<Neighbour> Neighbours => _neighbours;
        private List<Neighbour> _neighbours;

        public Path(int row, int column, bool isStartingPosition)
            : base(row, column, isStartingPosition)
        {
            _neighbours = new List<Neighbour>();
        }

        public bool IsVisited { get; private set; }

        public void AddNeighbour(Neighbour neighbour)
        {
            if (!_neighbours.Any(n => n.Tile.Coordinates == neighbour.Tile.Coordinates))
            {
                _neighbours.Add(neighbour);
            }
            //new starting position
            else if (neighbour.Tile.IsStartingPosition)
            {
                _neighbours[_neighbours.FindIndex(x => x.Tile.Coordinates == neighbour.Tile.Coordinates)] = neighbour;
            }
        }

        public void AddNeighbours(List<Neighbour> neighbours)
        {
            neighbours.ForEach(x => AddNeighbour(x));
        }

        public void Visit()
        {
            IsVisited = true;
        }
    }
}
