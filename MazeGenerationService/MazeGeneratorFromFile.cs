using Domain;
using FileManagementService.Interfaces;
using MazeGenerationService.Exceptions;
using MazeGenerationService.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;

namespace MazeGenerationService
{
    public class MazeGeneratorFromFile : IMazeGenerator
    {
        private readonly MazeGenerationSettings _settings;
        private readonly IFileReader _fileReader;

        public MazeGeneratorFromFile(
            IFileReader fileReader,
            IOptions<MazeGenerationSettings> settings)
        {
            _fileReader = fileReader;
            _settings = settings.Value;
        }

        public Maze GenerateMaze()
        {
            try
            {
                var lines = _fileReader.ReadLineByLine(_settings.MazeFilePath);
                var mazeSize = lines.First().Split(' ').Select(x => Convert.ToInt32(x)).ToList();
                var maze = new Maze(mazeSize[0], mazeSize[1]);

                var row = 0;
                foreach (var line in lines.Skip(1))
                {
                    var tiles = line.Split(' ')
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .Select(x => Convert.ToInt32(x))
                        .ToList();
                    for (int col = 0; col < maze.Width; col++)
                    {
                        maze.AddTile(row, col, tiles[col]);
                    }

                    if (++row > maze.Height)
                    {
                        break;
                    }
                }

                return maze;
            }
            catch (FileNotFoundException)
            {
                throw new MazeGenerationException("Failed to find Maze.txt file. Check the appsettings.");
            }
            catch (Exception)
            {
                throw new MazeGenerationException("Something went wrong.");
            }
        }
    }
}
