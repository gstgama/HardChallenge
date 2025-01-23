namespace SmartVault.Domain.Interfaces
{
    public interface IDataService
    {
        long GetAllFileSizes();
        void WriteEveryThirdFileToFile(string accountId);
    }
}