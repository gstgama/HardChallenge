using SmartVault.DataAccess.Interfaces;
using SmartVault.DataAccess.Services;
using SmartVault.Domain.Interfaces;
using SmartVault.Domain.Services;
using System;

namespace SmartVault.Program
{
    partial class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                return;
            }

            var connectionString = GetConnectionString();

            IDatabaseService databaseService = new SQLiteDatabaseService(connectionString);
            IFileService fileService = new FileSystemService();
            IDataService dataService = new DataService(databaseService, fileService);

            WriteEveryThirdFileToFile(dataService, args[0]);
            GetAllFileSizes(dataService);
        }

        private static void GetAllFileSizes(IDataService dataService)
        {
            long size = dataService.GetAllFileSizes();
            Console.WriteLine($"Total size of files: {size}");
        }

        private static void WriteEveryThirdFileToFile(IDataService dataService, string accountId)
        {
            dataService.WriteEveryThirdFileToFile(accountId);
        }

        private static string GetConnectionString()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var dataDirectory = currentDirectory.Replace(".Program", ".DataGeneration");
            var databasePath = System.IO.Path.Combine(dataDirectory, "testdb.sqlite");
            return $@"Data Source={databasePath};Version=3;";
        }
    }
}