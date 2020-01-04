using System.Collections.Generic;

namespace FileManagementService.Interfaces
{
    public interface IFileReader
    {
        IEnumerable<string> ReadLineByLine(string filePath);
    }
}
