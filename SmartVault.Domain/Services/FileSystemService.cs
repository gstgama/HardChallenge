using SmartVault.Domain.Interfaces;
using System.IO;

namespace SmartVault.Domain.Services
{
    public class FileSystemService : IFileService
    {
        public long GetFileSize(string filePath)
        {
            return new FileInfo(filePath).Length;
        }

        public string ReadFileContent(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public void WriteToFile(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }
    }
}