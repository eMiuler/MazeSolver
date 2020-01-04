using Common;
using Domain;
using MazeSolvingApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeSolvingApplication
{
    public class DfsMazeSolver : IMazeSolver
    {
        public DfsMazeSolver()
        {
        }

        public List<Path> SolveMaze(Maze maze, Path startingPosition)
        {
            var pathToExit = new List<Path> { startingPosition };

            var success = SolveMaze(maze, pathToExit);

            return success ? pathToExit : null;
        }

        private bool SolveMaze(Maze maze, List<Path> pathToExit)
        {
            var startingPosition = pathToExit.Last();
            startingPosition.Visit();
            if (maze.IsTileExit(startingPosition))
            {
                return true;
            }

            if (!startingPosition.UnvisitedNeighbours.Any())
            {
                return false;
            }

            foreach (var neighbour in startingPosition.UnvisitedNeighbours)
            {
                pathToExit.Add(neighbour.Tile);
                Console.WriteLine(neighbour.Direction.ToString());
                if (SolveMaze(maze, pathToExit))
                {
                    return true;
                }
                // Going back because no luck here.
                pathToExit.Remove(neighbour.Tile);
                Console.WriteLine(neighbour.Direction.Opposite().ToString());
            }

            return false;
        }
    }
}
