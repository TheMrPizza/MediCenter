namespace Server.Models
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string DoctorsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoDBSettings
    {
        string DoctorsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
