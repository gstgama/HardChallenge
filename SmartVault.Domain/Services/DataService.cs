using SmartVault.DataAccess.Interfaces;
using SmartVault.Domain.Interfaces;
using System.Collections.Generic;

namespace SmartVault.Domain.Services
{
    public class DataService : IDataService
    {
        private readonly IDatabaseService _databaseService;
        private readonly IFileService _fileService;

        public DataService(IDatabaseService databaseService, IFileService fileService)
        {
            _databaseService = databaseService;
            _fileService = fileService;
        }

        public long GetAllFileSizes()
        {
            const string query = "SELECT DISTINCT FilePath FROM Document";
            long totalFileSize = 0;

            foreach (var filePath in _databaseService.GetFilePaths(query))
            {
                totalFileSize += _fileService.GetFileSize(filePath);
            }

            return totalFileSize;
        }

        public void WriteEveryThirdFileToFile(string accountId)
        {
            const string query = "SELECT FilePath FROM Document WHERE AccountId = @accountId";
            var parameters = new Dictionary<string, object> { { "@accountId", accountId } };
            var filePaths = _databaseService.GetFilePaths(query, parameters);

            int count = 1;
            var output = string.Empty;

            foreach (var filePath in filePaths)
            {
                if (count % 3 == 0)
                {
                    var content = _fileService.ReadFileContent(filePath);
                    if (content.Contains("Smith Property"))
                    {
                        output += content;
                    }
                }

                count++;
            }

            if (!string.IsNullOrEmpty(output))
            {
                _fileService.WriteToFile("Output.txt", output);
            }
        }
    }
}