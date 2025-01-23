using System.Collections.Generic;

namespace SmartVault.Domain.Interfaces
{
    public interface IFileService
    {
        long GetFileSize(string filePath);
        string ReadFileContent(string filePath);
        void WriteToFile(string filePath, string content);
    }
}