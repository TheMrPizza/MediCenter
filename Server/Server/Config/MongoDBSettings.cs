using System.Collections.Generic;

namespace Server.Config
{
    public class MongoDBSettings : IDBSettings
    {
        public Dictionary<string, string> CollectionsNames { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
