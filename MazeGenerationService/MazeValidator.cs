using Domain;
using MazeGenerationService.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MazeGenerationService
{
    public class MazeValidator : IMazeValidator
    {
        public List<string> ValidateMaze(Maze maze)
        {
            var errors = new List<string>();

            var expectedTileCount = maze.Height * maze.Width;
            var expectedStartingPositionCount = 1;

            var tileCount = maze.Tiles.Count();
            var startingPositionCount = maze.Tiles.Count(x => x.IsStartingPosition);

            if (tileCount != expectedTileCount)
            {
                errors.Add($"Tile count should be {expectedTileCount} but was {tileCount}");
            }

            if (startingPositionCount != expectedStartingPositionCount)
            {
                errors.Add($"Starting position count should be {expectedStartingPositionCount} but was {startingPositionCount}");
            }

            return errors;
        }
    }
}
