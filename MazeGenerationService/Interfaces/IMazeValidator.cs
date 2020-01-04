using Domain;
using System.Collections.Generic;

namespace MazeGenerationService.Interfaces
{
    public interface IMazeValidator
    {
        List<string> ValidateMaze(Maze maze);
    }
}
