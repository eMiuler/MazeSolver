using Domain;
using System.Collections.Generic;

namespace MazeSolvingApplication.Interfaces
{
    public interface IMazeSolver
    {
        List<Path> SolveMaze(Maze maze, Path startingPosition);
    }
}
