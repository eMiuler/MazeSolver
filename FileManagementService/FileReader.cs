using FileManagementService.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace FileManagementService
{
    public class FileReader : IFileReader
    {
        public IEnumerable<string> ReadLineByLine(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }

            string line = string.Empty;
            using (var file = new StreamReader(filePath))
            {
                while ((line = file.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}
