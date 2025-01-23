using System.Collections.Generic;

namespace SmartVault.DataAccess.Interfaces
{
    public interface IDatabaseService
    {
        IEnumerable<string> GetFilePaths(string query, Dictionary<string, object> parameters = null);
    }
}