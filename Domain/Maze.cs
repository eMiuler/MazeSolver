using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Maze
    {
        private readonly List<Tile> _tiles;

        public Maze(int height, int width)
        {
            Height = height;
            Width = width;
            _tiles = new List<Tile>();
        }

        public int Width { get; }
        public int Height { get; }
        public IReadOnlyList<Tile> Tiles => _tiles;
        public Path StartingPosition => _tiles.FirstOrDefault(x => x.IsStartingPosition) as Path;

        public bool IsTileExit(Tile tile)
        {
            return tile.Coordinates.X == 0 || tile.Coordinates.Y == 0 ||
                tile.Coordinates.X == Width - 1 || tile.Coordinates.Y == Height - 1;
        }

        public void AddTile(int row, int column, int tileId)
        {
            switch (tileId)
            {
                case Constants.StartPositionId:
                case Constants.PathId:
                    var isStart = tileId == Constants.StartPositionId;
                    var tile = new Path(row, column, isStart);
                    var neighbours = FindNeighbours(tile);
                    ConnectTiles(tile, neighbours);
                    tile.AddNeighbours(neighbours);
                    _tiles.Add(tile);
                    break;
                case Constants.WallId:
                    _tiles.Add(new Wall(row, column, false));
                    break;
                default:
                    throw new ArgumentException($"Invalid tileId: {tileId}.");
            }
        }

        public void SetNewStartingPosition(int x, int y)
        {
            if (x < 0 || y < 0 || x > Width - 1 || y > Height - 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            StartingPosition.SetStartingPosition(false);

            var newStartingPosition = new Path(y, x, true);
            // Add neighbours to the new starting position.
            var neighbours = FindNeighbours(newStartingPosition);
            ConnectTiles(newStartingPosition, neighbours);
            // Instead of an old tile set the new starting position.
            var indexOfOldStartingPosition = _tiles.IndexOf(_tiles.First(t => t.Coordinates == new Coordinates(x, y)));
            _tiles[indexOfOldStartingPosition] = newStartingPosition;
        }

        private void ConnectTiles(Path path, List<Neighbour> neighbours)
        {
            path.AddNeighbours(neighbours);

            neighbours.ForEach(n =>
            {
                var direction = n.Direction.Opposite();
                n.Tile.AddNeighbour(new Neighbour(path, direction));
            });
        }

        private List<Neighbour> FindNeighbours(Path path)
        {
            var neighbours = new List<Neighbour>();
            var tileX = path.Coordinates.X;
            var tileY = path.Coordinates.Y;

            if (tileY > 0 && FindTileByCoordinates(tileX, tileY - 1) is Path upNeighbour)
            {
                neighbours.Add(new Neighbour(upNeighbour, Directions.Up)); // up
            }

            if (tileY < Height - 1 && FindTileByCoordinates(tileX, tileY + 1) is Path downNeighbour)
            {
                neighbours.Add(new Neighbour(downNeighbour, Directions.Down)); // down
            }

            if (tileX < Width - 1 && FindTileByCoordinates(tileX + 1, tileY) is Path rightNeighbour)
            {
                neighbours.Add(new Neighbour(rightNeighbour, Directions.Right)); // right
            }

            if (tileX > 0 && FindTileByCoordinates(tileX - 1, tileY) is Path leftNeighbour)
            {
                neighbours.Add(new Neighbour(leftNeighbour, Directions.Left)); // left
            }

            return neighbours;
        }

        private Tile FindTileByCoordinates(int x, int y)
        {
            var coordinates = new Coordinates(x, y);

            return _tiles.FirstOrDefault(t => t.Coordinates == coordinates);
        }

        /// <summary>
        /// Returns maze representation.
        /// </summary>
        public override string ToString()
        {
            int counter = 0;
            string maze = string.Empty;

            Tiles.ToList().ForEach(x =>
            {
                if (x is Path path)
                {
                    maze += path.IsStartingPosition ? Constants.StartPositionId : Constants.PathId;
                }
                if (x is Wall)
                {
                    maze += Constants.WallId;
                }
                maze += ' ';
                if (++counter == Width)
                {
                    maze += Environment.NewLine;
                    counter = 0;
                }
            });

            return maze;
        }

        /// <summary>
        /// Returns Maze representation with a path from starting position to the exit.
        /// </summary>
        public string ToString(List<Path> pathToExit)
        {
            int counter = 0;
            string maze = string.Empty;

            Tiles.ToList().ForEach(x =>
            {
                if (pathToExit.Any(tile => tile.Coordinates == x.Coordinates))
                {
                    maze += 'X';
                }
                else if (x is Path path)
                {
                    maze += path.IsStartingPosition ? Constants.StartPositionId : Constants.PathId;
                }
                else if (x is Wall)
                {
                    maze += Constants.WallId;
                }
                maze += ' ';
                if (++counter == Width)
                {
                    maze += Environment.NewLine;
                    counter = 0;
                }
            });

            return maze;
        }
    }
}
