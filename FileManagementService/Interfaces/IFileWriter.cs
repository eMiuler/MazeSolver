namespace FileManagementService.Interfaces
{
    public interface IFileWriter
    {
        void CreateFileInCurrentDirectory(string fileName, string fileContent);
    }
}
