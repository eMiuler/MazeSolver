using FileManagementService.Interfaces;
using System.IO;

namespace FileManagementService
{
    public class FileWriter : IFileWriter
    {
        public void CreateFileInCurrentDirectory(string fileName, string fileContent)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = currentDirectory + @$"\{fileName}";

            File.WriteAllText(path, fileContent);
        }
    }
}
