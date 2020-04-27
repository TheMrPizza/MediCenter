using System.Collections.Generic;

namespace Server.Config
{
    public interface IDBSettings
    {
        Dictionary<string, string> CollectionsNames { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
