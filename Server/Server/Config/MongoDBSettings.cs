using System.Collections.Generic;

namespace Server.Config
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public Dictionary<string, string> CollectionsNames { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoDBSettings
    {
        Dictionary<string, string> CollectionsNames { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
