using Common;
using Domain;
using FileManagementService.Interfaces;
using MazeGenerationService.Exceptions;
using MazeGenerationService.Interfaces;
using MazeSolvingApplication.Interfaces;
using System;
using System.Linq;

namespace MazeSolvingApplication
{
    static class Program
    {
        static void Main()
        {
            var diContainer = new DIContainer();

            try
            {
                var mazeSolver = diContainer.GetService<IMazeSolver>();
                var mazeGenerator = diContainer.GetService<IMazeGenerator>();
                var mazeValidator = diContainer.GetService<IMazeValidator>();
                var fileWriter = diContainer.GetService<IFileWriter>();

                var maze = mazeGenerator.GenerateMaze();
                Console.WriteLine("Maze:");
                Console.WriteLine(maze.ToString());

                var errors = mazeValidator.ValidateMaze(maze);
                if (errors.Any())
                {
                    errors.ForEach(x => Console.WriteLine(x));
                    EndMaze();
                }

                //Ability to define new starting position
                if (NewStartingPositionMenu(maze))
                {
                    SelectNewStartingPosition(maze);
                }

                var startingPosition = maze.StartingPosition;
                var pathToExit = mazeSolver.SolveMaze(maze, startingPosition);

                if (pathToExit is null)
                {
                    Console.WriteLine("There is no exit from current start position");
                    EndMaze();
                }

                fileWriter.CreateFileInCurrentDirectory(Constants.MazeSolutionFile, maze.ToString(pathToExit));
                Console.WriteLine("Success");
            }
            catch (MazeGenerationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong.");
            }
            finally
            {
                EndMaze();
            }
        }

        static void EndMaze()
        {
            Console.WriteLine(Environment.NewLine + "Press any key to continue.");
            Console.ReadLine();
            Environment.Exit(0);
        }

        static bool NewStartingPositionMenu(Maze maze)
        {
            var startingPosition = maze.StartingPosition;
            Console.WriteLine($"Current starting position coordinates: ({startingPosition.Coordinates.X}, {startingPosition.Coordinates.Y})");

            bool setNewStartingPosition;
            do
            {
                Console.Write("Would you like to set a new starting position? Y/N: ");
                var key = Console.ReadKey();
                Console.WriteLine();
                if (key.KeyChar == 'y' || key.KeyChar == 'Y')
                {
                    setNewStartingPosition = true;
                    break;
                }
                if (key.KeyChar == 'n' || key.KeyChar == 'N')
                {
                    setNewStartingPosition = false;
                    break;
                }
                Console.WriteLine("Invalid input, try again.");
            } while (true);

            return setNewStartingPosition;
        }

        static void SelectNewStartingPosition(Maze maze)
        {
            do
            {
                try
                {
                    Console.WriteLine($"X: [0; {maze.Width - 1}]; Y: [0; {maze.Height - 1}]");
                    Console.Write($"New X: ");
                    var x = Convert.ToInt32(Console.ReadLine());
                    Console.Write($"New Y: ");
                    var y = Convert.ToInt32(Console.ReadLine());

                    maze.SetNewStartingPosition(x, y);
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Wrong input, try again.");
                }
            } while (true);
        }
    }
}
